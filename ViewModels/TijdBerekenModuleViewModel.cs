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
    class TijdBerekenModuleViewModel : BeheerViewModel, IBereken, ISelectedKlant
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

        #region CurrentParameter full property
        private Parameters _currentParameters;
        public Parameters CurrentParameters
        {
            get
            {
                return _currentParameters;
            }
            private set
            {
                _currentParameters = value;
                NoticeMe("CurrentParameters");
            }
        }
        #endregion

        #region Selected klant full property
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

        public TijdBerekenModuleViewModel(ISIS_DataEntities context)
        {
            AddPrestatie = new Prestatie();
            AddPrestatie.TijdPrestaties = new TijdPrestatie();
            CurrentParameters = new Parameters();
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

            //Load parameters from settings!
            CurrentParameters.LoadParameters();

            //Data is just loaded so first have to recalculate before we can save!!
            SetIsValid(false);         
        }

        public void Bereken()
        {
            //Check if there is a klant selected
            if (SelectedKlant == null)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            //Add current parameter values into the prestatie!
            AddPrestatie.TijdPrestaties.AddParameters(CurrentParameters);

            //Calculate how long every part takes
            CalculateStrijk();

            //Calculate the "administratie" time
            if (AddPrestatie.TijdPrestaties.TotaalStrijk < 20)
                AddPrestatie.TijdPrestaties.TijdAdministratie = 5;
            else if (AddPrestatie.TijdPrestaties.TotaalStrijk < 40)
                AddPrestatie.TijdPrestaties.TijdAdministratie = 10;
            else if (AddPrestatie.TijdPrestaties.TotaalStrijk < 80)
                AddPrestatie.TijdPrestaties.TijdAdministratie = 15;
            else
                AddPrestatie.TijdPrestaties.TijdAdministratie = 20;

            AddPrestatie.TotaalMinuten = AddPrestatie.TijdPrestaties.TotaalHemden + AddPrestatie.TijdPrestaties.TotaalLakens1 + AddPrestatie.TijdPrestaties.TotaalLakens2 + AddPrestatie.TijdPrestaties.TotaalAndereStrijk + AddPrestatie.TijdPrestaties.TijdAdministratie;

            AddPrestatie.TotaalBetalen = AddPrestatie.TotaalMinuten - SelectedKlant.Tegoed;
            AddPrestatie.TotaalDienstenChecks = Convert.ToByte(Math.Ceiling(AddPrestatie.TotaalBetalen / 60.0));
            if (AddPrestatie.TotaalDienstenChecks == 0)
                AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed - AddPrestatie.TotaalMinuten;
            else
                AddPrestatie.NieuwTegoed = (AddPrestatie.TotaalDienstenChecks * 60) - AddPrestatie.TotaalBetalen;

            //The "prestatie" is calculated you're know able to save it!
            SetIsValid(true);
        }

        private void CalculateStrijk()
        {
            AddPrestatie.TijdPrestaties.TotaalHemden = (int)Math.Ceiling(AddPrestatie.TijdPrestaties.AantalHemden * AddPrestatie.TijdPrestaties.ParameterHemden);
            AddPrestatie.TijdPrestaties.TotaalLakens1 = (int)Math.Ceiling(AddPrestatie.TijdPrestaties.AantalLakens1 * AddPrestatie.TijdPrestaties.ParameterLakens1);
            AddPrestatie.TijdPrestaties.TotaalLakens2 = (int)Math.Ceiling(AddPrestatie.TijdPrestaties.AantalLakens2 * AddPrestatie.TijdPrestaties.ParameterLakens2);
            AddPrestatie.TijdPrestaties.TotaalAndereStrijk = (int)Math.Ceiling(AddPrestatie.TijdPrestaties.TijdAndereStrijk * AddPrestatie.TijdPrestaties.ParameterAndereStrijk);
            AddPrestatie.TijdPrestaties.TotaalStrijk = (AddPrestatie.TijdPrestaties.AantalHemden + AddPrestatie.TijdPrestaties.AantalLakens1 + AddPrestatie.TijdPrestaties.AantalLakens2 + AddPrestatie.TijdPrestaties.AantalAndereStrijk);
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

                if (ctx.Prestaties.Any())
                {
                    //Get all previous TijdPrestaties of the selected klant
                    var previousPrestaties = ctx.TijdPrestaties.Where(p => p.Prestaties.KlantenNummer == SelectedKlant.ID);

                    if (previousPrestaties.Any())
                    {
                        //Get the id of the latest tijdprestatie
                        var tempId = previousPrestaties.OrderByDescending(p => p.Id).First().Id;
                        //Get the prestatie with the same id of the latest tijdprestatie
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

                //Change the current parameters to the parameters that where active at the time the parameter was made!
                CurrentParameters.ParameterHemden = AddPrestatie.TijdPrestaties.ParameterHemden;
                CurrentParameters.ParameterLakens1 = AddPrestatie.TijdPrestaties.ParameterLakens1;
                CurrentParameters.ParameterLakens2 = AddPrestatie.TijdPrestaties.ParameterLakens2;
                CurrentParameters.ParameterAndereStrijk = AddPrestatie.TijdPrestaties.ParameterAndereStrijk;

                //Thinking reverse => The current Tegoed of the Klant was the NiewTegoed of the last prestatie
                AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed;

                CalculateStrijk();

                AddPrestatie.TotaalMinuten = AddPrestatie.TijdPrestaties.TotaalHemden + AddPrestatie.TijdPrestaties.TotaalLakens1 + AddPrestatie.TijdPrestaties.TotaalLakens2 + AddPrestatie.TijdPrestaties.TotaalAndereStrijk + AddPrestatie.TijdPrestaties.TijdAdministratie;

                //Recalculate the previous Tegoed of the klant
                if (AddPrestatie.TotaalDienstenChecks > 0)
                {
                    AddPrestatie.TotaalBetalen = (AddPrestatie.TotaalDienstenChecks * 60) - AddPrestatie.NieuwTegoed;
                    SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.TotaalMinuten - AddPrestatie.TotaalBetalen);
                }
                else
                {
                    AddPrestatie.TotaalBetalen = 0;
                    SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.TotaalMinuten + AddPrestatie.NieuwTegoed);
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

                CurrentParameters = new Parameters();
                AddPrestatie = new Prestatie();
                AddPrestatie.TijdPrestaties = new TijdPrestatie();
                AddPrestatie.TijdPrestaties.AddParameters(CurrentParameters);
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
                //SelectedKlant.LaatsteActiviteit = AddPrestatie.Datum;


                //We query local context first to see if it's there.
                var klant = ctx.Klanten.Find(SelectedKlant.ID);

                //We have it in the entity, need to update the "klant".
                if (klant != null)
                {
                    ctx.Entry(klant).CurrentValues.SetValues(SelectedKlant);        //Update "LaatsteActiviteit" in the correct "klant"!
                }

                ctx.Prestaties.Add(AddPrestatie);
                ctx.SaveChanges();
            }
            //Updating last prestatie
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
