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
    class BerekenModuleViewModel : BeheerViewModel, ISelectedKlant
    {
        public Prestatie AddPrestatie { get; private set; }
        public Parameters CurrentParameters { get; private set; }
        public BerekenCommand BerekenCommandEvent { get; set; }
        public SearchBoxKlantViewModel SearchBoxViewModel { get; set; }

        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

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

        public ICollectionView KlantenView
        {
            get
            {
                ctx.Klanten.Load();
                ViewSource.Source = ctx.Klanten.Local;
                return ViewSource.View;
            }
        }

        public BerekenModuleViewModel()
        {
            Header = "Berekenmodule";
            LoadData();
            AddPrestatie = new Prestatie();
            CurrentParameters = new Parameters();
            ViewSource = new CollectionViewSource();
            BerekenCommandEvent = new BerekenCommand(this);
            SearchBoxViewModel = new SearchBoxKlantViewModel(this);
            AddPrestatie.Datum = DateTime.Now;
        }

        private void LoadData()
        {
            ctx = new ISIS_DataEntities();
            ctx.Prestaties.Load();
            ctx.Klanten.Load();
        }

        public void Bereken()
        {
            if (SelectedKlant == null)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            ////Load parameters from settings! And add them into the Prestatie
            CurrentParameters.LoadParameters();

            //ChangeDataContextColumn(3, _parameters);
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

            //_ableToSave = true;
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
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            //The second time you want to add a "prestatie" EF is following the first object
            //If you change the object EF will track the edits
            //This will cause errors because the ID wil change (is normally not possible)
            //But in our case it's a new "prestatie" so the ID should change
            //To solve this we have to detach the object if EF is tracking it
            Prestatie attachedPrestatie = ctx.Prestaties.Find(AddPrestatie.Id);
            if (attachedPrestatie != null)
                ctx.Entry(attachedPrestatie).State = EntityState.Detached;


            //if (ButtonToevoegen.Content.ToString() == "Toevoegen")
            //{
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
            //}
            //else
            //{
            //SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.NieuwTegoed);

            //var klant = ctx.Klanten.Find(SelectedKlant.ID);

            ////We have it in the entity, need to update.
            //if (klant != null)
            //{
            //    ctx.Entry(klant).CurrentValues.SetValues(SelectedKlant);
            //}

            //var prestatie = ctx.Prestaties.Find(AddPrestatie.Id);

            ////We have it in the entity, need to update.
            //if (prestatie != null)
            //{
            //    ctx.Entry(prestatie).CurrentValues.SetValues(AddPrestatie);
            //}

            //ctx.SaveChanges();
            //ButtonBereken.Content = "Bereken";
            //ButtonToevoegen.Content = "Toevoegen";
            //ButtonChange.Content = "Laatste prestatie aanpassen";
            //}
        }
    }
}
