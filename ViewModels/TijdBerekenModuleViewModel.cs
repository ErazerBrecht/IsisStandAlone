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

        public TijdBerekenModuleViewModel()
        {
            AddPrestatie = new Prestatie();
            CurrentParameters = new Parameters();
            ViewSource = new CollectionViewSource();
            BerekenCommandEvent = new BerekenCommand(this);
            AddPrestatie.Datum = DateTime.Now;

            ButtonBerekenContent = "Bereken";
            ButtonToevoegenContent = "Toevoegen";
            ButtonChangeContent = "Laatste prestatie aanpassen";

            LoadData();
        }

        public void LoadData()
        {
            ctx = new ISIS_DataEntities();
            ctx.Prestaties.Load();
            ctx.Klanten.Load();

            //Load parameters from settings!
            CurrentParameters.LoadParameters();

            SetIsValid(false);          //New data loaded so first have to recalculate!!
        }

        public void Bereken()
        {
            if (SelectedKlant == null)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            //Add current parameter values into the prestatie!
            AddPrestatie.AddParameters(CurrentParameters);

            CalculateStrijk();

            if (AddPrestatie.TotaalStrijk < 20)
                AddPrestatie.TijdAdministratie = 5;
            else if (AddPrestatie.TotaalStrijk < 40)
                AddPrestatie.TijdAdministratie = 10;
            else if (AddPrestatie.TotaalStrijk < 80)
                AddPrestatie.TijdAdministratie = 15;
            else
                AddPrestatie.TijdAdministratie = 20;

            AddPrestatie.TotaalMinuten = AddPrestatie.TotaalHemden + AddPrestatie.TotaalLakens1 + AddPrestatie.TotaalLakens2 + AddPrestatie.TotaalAndereStrijk + AddPrestatie.TijdAdministratie;

            AddPrestatie.TotaalBetalen = AddPrestatie.TotaalMinuten - SelectedKlant.Tegoed;
            AddPrestatie.TotaalDienstenChecks = Convert.ToByte(Math.Ceiling(AddPrestatie.TotaalBetalen / 60.0));
            if (AddPrestatie.TotaalDienstenChecks == 0)
                AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed - AddPrestatie.TotaalMinuten;
            else
                AddPrestatie.NieuwTegoed = (AddPrestatie.TotaalDienstenChecks * 60) - AddPrestatie.TotaalBetalen;

            SetIsValid(true);
        }

        private void CalculateStrijk()
        {
            AddPrestatie.TotaalHemden = (int)Math.Ceiling(AddPrestatie.AantalHemden * AddPrestatie.ParameterHemden);
            AddPrestatie.TotaalLakens1 = (int)Math.Ceiling(AddPrestatie.AantalLakens1 * AddPrestatie.ParameterLakens1);
            AddPrestatie.TotaalLakens2 = (int)Math.Ceiling(AddPrestatie.AantalLakens2 * AddPrestatie.ParameterLakens2);
            AddPrestatie.TotaalAndereStrijk = (int)Math.Ceiling(AddPrestatie.TijdAndereStrijk * AddPrestatie.ParameterAndereStrijk);
            AddPrestatie.TotaalStrijk = (AddPrestatie.AantalHemden + AddPrestatie.AantalLakens1 + AddPrestatie.AantalLakens2 + AddPrestatie.AantalAndereStrijk);
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

                if (ctx.Prestaties.Count() > 0)
                {
                    var previousPrestaties = ctx.Prestaties.Where(p => p.KlantenNummer == SelectedKlant.ID);

                    if (previousPrestaties.Count() > 0)
                        AddPrestatie = previousPrestaties.OrderByDescending(p => p.Id).First();
                    else
                    {
                            MessageBoxService messageBoxService = new MessageBoxService();
                            messageBoxService.ShowMessageBox("Deze klant heeft geen vorige prestaties, u kunt niets wijzigen");
                            return;
                    }
                }
                else
                {
                    MessageBoxService messageBoxService = new MessageBoxService();
                    messageBoxService.ShowMessageBox("Er bevinden zich nog geen prestaties in de databank, u kunt niets wijzigen");
                    return;
                }

                CurrentParameters.ParameterHemden = AddPrestatie.ParameterHemden;
                CurrentParameters.ParameterLakens1 = AddPrestatie.ParameterLakens1;
                CurrentParameters.ParameterLakens2 = AddPrestatie.ParameterLakens2;
                CurrentParameters.ParameterAndereStrijk = AddPrestatie.ParameterAndereStrijk;

                AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed;

                CalculateStrijk();

                AddPrestatie.TotaalMinuten = AddPrestatie.TotaalHemden + AddPrestatie.TotaalLakens1 + AddPrestatie.TotaalLakens2 + AddPrestatie.TotaalAndereStrijk + AddPrestatie.TijdAdministratie;
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
                AddPrestatie.AddParameters(CurrentParameters);
            }
        }

        public override void SaveChanges()
        {
            SetIsValid(false);

            //The second time you want to add a "prestatie" EF is following the first object
            //If you change the object EF will track the edits
            //This will cause errors because the ID wil change (is normally not possible)
            //But in our case it's a new "prestatie" so the ID should change
            //To solve this we have to detach the object if EF is tracking it
            Prestatie attachedPrestatie = ctx.Prestaties.Find(AddPrestatie.Id);
            if (attachedPrestatie != null)
                ctx.Entry(attachedPrestatie).State = EntityState.Detached;


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
            SelectedKlant.LaatsteActiviteit = AddPrestatie.Datum;


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
