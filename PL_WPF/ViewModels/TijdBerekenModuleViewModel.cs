using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using DAL_Repository;
using EF_Model;
using PL_WPF.Services;

namespace PL_WPF.ViewModels
{
    class TijdBerekenModuleViewModel : PrestatieBerekenModuleViewModel
    {
        #region AddTijdPrestatie full property
        private TijdPrestatie _addTijdPrestatie;

        public TijdPrestatie AddTijdPrestatie
        {
            get { return _addTijdPrestatie; }
            set
            {
                _addTijdPrestatie = value;
                NoticeMe("AddTijdPrestatie");
                _addTijdPrestatie.PropertyChanged += _addTijdPrestatie_PropertyChanged;
            }
        }

        private void _addTijdPrestatie_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsValid = false;
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

        #region  CurrentStrings full property
        private Strings _currentStrings;
        public Strings CurrentStrings
        {
            get
            {
                return _currentStrings;
            }
            private set
            {
                _currentStrings = value;
                NoticeMe("CurrentStrings");
            }
        }
        #endregion


        public TijdBerekenModuleViewModel(UnitOfWork ctx) : base(ctx)
        {
            Init();
            CurrentParameters = new Parameters();
            CurrentStrings = new Strings();
            LoadData();
        }

        public override void LoadData()
        {
            //Load parameters from settings!
            CurrentParameters.LoadParameters();
            CurrentStrings.LoadStrings();

            if (SelectedKlant != null)
            {
                CurrentParameters.ParameterAndereStrijk = Ctx.KlantTypes.GetSnelheidsCoëfficiënt(SelectedKlant.TypeNaam, SelectedKlant.TypePlaats);
            }

        }

        public override void Init()
        {
            AddTijdPrestatie = new TijdPrestatie
            {
                AantalHemden = 0,
                AantalLakens1 = 0,
                AantalLakens2 = 0,
                AantalAndereStrijk = 0,
                TijdAndereStrijk = 0
            };
        }

        public override void Bereken()
        {
            //Add current parameter values into the prestatie!
            AddTijdPrestatie.AddParameters(CurrentParameters);
            //Tegoed of current prestatie is equal to the current 'tegoed' of the client
            AddTijdPrestatie.Tegoed = SelectedKlant.Tegoed;

            //Calculate how long every part takes
            AddTijdPrestatie.CalculatePrestatie();

            //The "prestatie" is calculated you're know able to save it!
            //But first check if there where validation errors
            if (AddTijdPrestatie.CanSave)
                IsValid = true;
        }

        public override void EditLast(DatumBeheerViewModel vm)
        {
            IsValid = false;

            if (Ctx.TijdPrestaties.Any())
            {
                //Get all previous TijdPrestaties of the selected klant
                var tempPrestatie = Ctx.TijdPrestaties.GetLatestPrestatie(SelectedKlant);
                if (tempPrestatie != null)
                {
                    AddTijdPrestatie = tempPrestatie;
                    vm.EditLast(tempPrestatie.Id);
                }
                else
                {
                    throw new Exception("Deze klant heeft geen vorige prestaties, u kunt niets wijzigen");
                }
            }
            else
            {
                throw new Exception("Er bevinden zich nog geen prestaties in de databank, u kunt niets wijzigen");
            }

            //Change the current parameters to the parameters that where active at the time the parameter was made!
            CurrentParameters.ParameterHemden = AddTijdPrestatie.ParameterHemden;
            CurrentParameters.ParameterLakens1 = AddTijdPrestatie.ParameterLakens1;
            CurrentParameters.ParameterLakens2 = AddTijdPrestatie.ParameterLakens2;
            CurrentParameters.ParameterAndereStrijk = AddTijdPrestatie.ParameterAndereStrijk;

            //Thinking reverse => The current Tegoed of the Klant was the NiewTegoed of the last prestatie
            SelectedKlant.Tegoed = AddTijdPrestatie.RecalculatePrestatie(SelectedKlant.Tegoed);
        }

        public override void Cancel()
        {
            //Reset the changes we made
            Ctx.TijdPrestaties.Refresh();
            Ctx.Klanten.Refresh();

            ////Legacy code (before Repository pattern)
            ////Maybe I need it back one day
            //using (var context = new ISIS_DataEntities())
            //{
            //    context.Entry(AddPrestatie).Reload();
            //    context.Entry(SelectedKlant).Reload();
            //}

            CurrentParameters = new Parameters();
            Init();
        }

        //Adding new prestatie
        public override void Save(DateTime lastdate)
        {
            //The "prestatie" will be saved, the next "prestatie" has to be calculated first!
            //Setting this one false, disbales the save button
            IsValid = false;

            AddTijdPrestatie.Klant = SelectedKlant;

            //We query local context first to see if it's there.
            var klant = Ctx.Klanten.Get(SelectedKlant.Id);

            //We have it in the entity, need to update.
            if (klant != null)
            {
                klant.Tegoed = Convert.ToByte(AddTijdPrestatie.NieuwTegoed);
                klant.LaatsteActiviteit = lastdate;
            }

            Ctx.TijdPrestaties.Add(AddTijdPrestatie);
        }

        public override void UpdateLast()
        {
            //The "prestatie" will be saved, the next "prestatie" has to be calculated first!
            //Setting this one false, disbales the save button
            IsValid = false;

            SelectedKlant.Tegoed = Convert.ToByte(AddTijdPrestatie.NieuwTegoed);

            ////Legacy code
            //var klant = context.Klanten.Find(SelectedKlant.ID);

            ////We have it in the entity, need to update.
            //if (klant != null)
            //{
            //    context.Entry(klant).CurrentValues.SetValues(SelectedKlant);
            //}

            //var prestatie = context.Prestaties.Find(AddPrestatie.Id);

            ////We have it in the entity, need to update.
            //if (prestatie != null)
            //{
            //    context.Entry(prestatie).CurrentValues.SetValues(AddPrestatie);
            //}
        }
    }
}
