using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class BerekenModuleViewModel : BeheerViewModel
    {
        public Prestatie AddPrestatie { get; private set; }
        public Parameters CurrentParameters { get; private set; }

        public BerekenCommand BerekenCommandEvent { get; set; }
        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public BerekenModuleViewModel()
        {
            Header = "Berekenmodule";
            LoadData();
            AddPrestatie = new Prestatie();
            CurrentParameters = new Parameters();
            BerekenCommandEvent = new BerekenCommand(this);
        }

        private void LoadData()
        {
            ctx = new ISIS_DataEntities();
            ctx.Prestaties.Load();
            ctx.Klanten.Load();
        }

        public void Bereken()
        {
            //if (_tempKlant == null)
            //{
            //    MessageBox.Show("Je hebt nog geen klant gekozen!");
            //    return;
            //}

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

            //AddPrestatie.TotaalBetalen = AddPrestatie.TotaalMinuten - _tempKlant.Tegoed;
            //AddPrestatie.TotaalDienstenChecks = Convert.ToByte(Math.Ceiling(AddPrestatie.TotaalBetalen / 60.0));
            //if (AddPrestatie.TotaalDienstenChecks == 0)
            //    AddPrestatie.NieuwTegoed = _tempKlant.Tegoed - AddPrestatie.TotaalMinuten;
            //else
            //    AddPrestatie.NieuwTegoed = (AddPrestatie.TotaalDienstenChecks * 60) - AddPrestatie.TotaalBetalen;

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
            throw new NotImplementedException();
        }
    }
}
