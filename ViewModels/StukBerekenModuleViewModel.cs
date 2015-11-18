using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;
using ISIS.Commands;
using System.ComponentModel;
using System.Windows.Data;
using ISIS.Services;

namespace ISIS.ViewModels
{
    class StukBerekenModuleViewModel : BeheerViewModel, IBereken, ISelectedKlant
    {
        #region AddPrestatie full property
        private Prestatie _addPresatie;
        public Prestatie AddPrestatie
        {
            get
            {
                return _addPresatie;
            }
            private set
            {
                _addPresatie = value;
                NoticeMe("AddPrestatie");
            }
        }
        #endregion

        #region SelectedKlant full property
        private Klant _selectedKlant;
        public Klant SelectedKlant
        {
            get
            {
                return _selectedKlant;
            }

            set
            {
                _selectedKlant = value;
                NoticeMe("SelectedKlant");
            }
        }
        #endregion

        public BerekenCommand BerekenCommandEvent { get; set; }
        public SearchBoxKlantViewModel SearchBoxViewModel { get; set; }

        #region ButtonBerekenContent full property
        private string _buttonBerekenContent;

        public string ButtonBerekenContent
        {
            get { return _buttonBerekenContent; }
            set { _buttonBerekenContent = value; NoticeMe("ButtonBerekenContent"); }
        }
        #endregion

        #region ButtonToevoegenContent full property
        private string _buttonToevoegenContent;

        public string ButtonToevoegenContent
        {
            get { return _buttonToevoegenContent; }
            set { _buttonToevoegenContent = value; NoticeMe("ButtonToevoegenContent"); }
        }

        #endregion

        #region ButtonChangeContent full property
        private string _buttonChangeContent;

        public string ButtonChangeContent
        {
            get { return _buttonChangeContent; }
            set { _buttonChangeContent = value; NoticeMe("ButtonChangeContent"); }
        }

        #endregion


        private bool _isValid;
        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                // if (isValid == true) => check for validation errors
                return _isValid;
            }
        }

        //Gonna make use of a seperate set method because this is the only screen that needs a setter
        private void SetIsValid(bool value)
        {
            _isValid = value;
        }

        public StukBerekenModuleViewModel(ISIS_DataEntities context)
        {
            AddPrestatie = new Prestatie();
            AddPrestatie.StukPrestaties = new StukPrestatie();
            ViewSource = new CollectionViewSource();
            BerekenCommandEvent = new BerekenCommand(this);
            //In the near future this will not work, so disabled it already
            //Adding dates will change!
            //AddPrestatie.Datum = DateTime.Now;

            ButtonBerekenContent = "Bereken";
            ButtonToevoegenContent = "Toevoegen";
            ButtonChangeContent = "Laatste prestatie aanpassen";

            ctx = context;
            ctx.Prestaties.Load();

            //Data is just loaded so first have to recalculate before we can save!!
            SetIsValid(false);         
        }

        public void Bereken()
        {
            if (SelectedKlant == null)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            //Totaal needs to be saved in StukPrestaties otherwise it won't be placed in the DB
            AddPrestatie.TotaalMinuten = AddPrestatie.StukPrestaties.Totaal;

            //Calculate...
            AddPrestatie.TotaalBetalen = AddPrestatie.TotaalMinuten - SelectedKlant.Tegoed;
            AddPrestatie.TotaalDienstenChecks = Convert.ToByte(Math.Ceiling(AddPrestatie.TotaalBetalen / 60.0));
            if (AddPrestatie.TotaalDienstenChecks == 0)
                AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed - AddPrestatie.TotaalMinuten;
            else
                AddPrestatie.NieuwTegoed = (AddPrestatie.TotaalDienstenChecks * 60) - AddPrestatie.TotaalBetalen;

            SetIsValid(true);
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            SetIsValid(false);

            if (SelectedKlant == null)
            {
                MessageBoxService messageBoxService = new MessageBoxService();
                messageBoxService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            if (ButtonChangeContent == "Laatste prestatie aanpassen")
            {

                if (ctx.StukPrestaties.Any())
                {
                    var previousPrestaties = ctx.StukPrestaties.Where(p => p.Prestaties.KlantenNummer == SelectedKlant.ID);

                    if (previousPrestaties.Any())
                    {
                        //Get the id of the latest stukprestatie
                        var tempId = previousPrestaties.OrderByDescending(p => p.Id).First().Id;
                        //Get the prestatie with the same id of the latest stukprestatie
                        AddPrestatie = ctx.Prestaties.First(p => p.Id == tempId);
                    }
                    else
                    {
                        MessageBoxService messageBoxService = new MessageBoxService();
                        messageBoxService.ShowMessageBox(
                            "Deze klant heeft geen vorige prestaties, u kunt niets wijzigen");
                        return;
                    }
                }
                else
                {
                    MessageBoxService messageBoxService = new MessageBoxService();
                    messageBoxService.ShowMessageBox("Er bevinden zich nog geen prestaties in de databank, u kunt niets wijzigen");
                    return;
                }

                //Thinking reverse => The current Tegoed of the Klant was the NiewTegoed of the last prestatie
                AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed;

                if (AddPrestatie.TotaalDienstenChecks > 0)
                {
                    AddPrestatie.TotaalBetalen = (AddPrestatie.TotaalDienstenChecks * 60) - AddPrestatie.NieuwTegoed;
                    SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.StukPrestaties.Totaal - AddPrestatie.TotaalBetalen);
                }
                else
                {
                    AddPrestatie.TotaalBetalen = 0;
                    SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.StukPrestaties.Totaal + AddPrestatie.NieuwTegoed);
                }

                ButtonBerekenContent = "Herbereken";
                ButtonToevoegenContent = "Aanpassen";
                ButtonChangeContent = "Annuleren";
            }
            else
            {
                ButtonBerekenContent = "Bereken";
                ButtonToevoegenContent = "Toevoegen";
                ButtonChangeContent = "Laatste prestatie aanpassen";

                ctx.Entry(AddPrestatie).Reload();
                ctx.Entry(SelectedKlant).Reload();

                AddPrestatie = new Prestatie();
                AddPrestatie.StukPrestaties = new StukPrestatie();
            }
        }

        public override void SaveChanges()
        {
            //The "prestatie" will be saved, the next "prestatie" has to be calculated first!
            //Setting this one false, disbales the save button
            SetIsValid(false);

            //The second time you want to add a "prestatie" EF is following the first object
            //If you change the object EF will track the edits
            //This will cause errors because the ID wil change (is normally not possible)
            //But in our case it's a new "prestatie" so the ID should change
            //To solve this we have to detach the object if EF is tracking it
            Prestatie attachedPrestatie = ctx.Prestaties.Find(AddPrestatie.Id);
            if (attachedPrestatie != null)
                ctx.Entry(attachedPrestatie).State = EntityState.Detached;


            //Adding new prestatie
            if (ButtonToevoegenContent == "Toevoegen")
            {
                int tempId = 1;

                //Search for first valid ID
                while (ctx.Prestaties.Any(p => p.Id == tempId))
                {
                    tempId++;
                }

                AddPrestatie.Id = tempId;
                AddPrestatie.KlantenNummer = SelectedKlant.ID;

                SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.NieuwTegoed);
                
                //TODO: Reanable this!!!!
                //TODO: After the new data adding functionality is done
                //SelectedKlant.LaatsteActiviteit = AddPrestatie.Datum;


                //We query local context first to see if it's there.
                var klant = ctx.Klanten.Find(SelectedKlant.ID);

                //We have it in the entity, need to update.
                if (klant != null)
                {
                    ctx.Entry(klant).CurrentValues.SetValues(SelectedKlant);
                }

                ctx.Prestaties.Add(AddPrestatie);
                ctx.SaveChanges();
            }
            else
            {
                SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.NieuwTegoed);

                var klant = ctx.Klanten.Find(SelectedKlant.ID);

                //We have it in the entity, need to update.
                if (klant != null)
                {
                    ctx.Entry(klant).CurrentValues.SetValues(SelectedKlant);
                }

                var prestatie = ctx.Prestaties.Find(AddPrestatie.Id);

                //We have it in the entity, need to update.
                if (prestatie != null)
                {
                    ctx.Entry(prestatie).CurrentValues.SetValues(AddPrestatie);
                }

                ctx.SaveChanges();
                ButtonBerekenContent = "Bereken";
                ButtonToevoegenContent = "Toevoegen";
                ButtonChangeContent = "Laatste prestatie aanpassen";
            }
        }
    }
}
