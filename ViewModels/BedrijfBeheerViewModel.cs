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
    class BedrijfBeheerViewModel : BeheerViewModel
    {
        #region AddBedrijf fullproperty
        private SoortKlant _addBedrijf;
        public SoortKlant AddBedrijf
        {
            get
            {
                return _addBedrijf;
            }
            private set
            {
                _addBedrijf = value;
                NoticeMe("AddBedrijf");
            }
        }
        #endregion

        #region SelectedBedrijf fullproperty + propertychanged event
        private SoortKlant _selectedSoort;
        public SoortKlant SelectedSoort
        {
            get
            {
                return _selectedSoort;
            }
            set
            {
                _selectedSoort = value;
                NoticeMe("SelectedSoort");
                if (value != null)
                    _selectedSoort.PropertyChanged += _selectedSoort_PropertyChanged;
            }
        }

        private void _selectedSoort_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //ADD Datavalidation
            //SelectedBedrijf.Check();

            ctx.SaveChanges();
        }

        #endregion

        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public BedrijfBeheerViewModel()
        {
            LoadData();
            AddBedrijf = new SoortKlant();
            AddBedrijf.Type = "Bedrijf";
        }

        public void LoadData()
        {
            Refresh();
        }

        public override void Delete()
        {
            ctx.SoortKlant.Remove(SelectedSoort);
            ctx.SaveChanges();
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.SoortKlant.Load();
            ViewSource.Source = ctx.SoortKlant.Local.Where(s => s.Type == "Bedrijf");
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.SoortKlant.Add(AddBedrijf);
            ctx.SaveChanges();
            AddBedrijf = new SoortKlant();
            AddBedrijf.Type = "Bedrijf";
        }
    }
}
