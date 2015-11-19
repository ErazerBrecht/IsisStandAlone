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
    abstract class SoortKlantBeheerViewModel : BeheerViewModel
    {
        #region AddSoort fullproperty
        private SoortKlant _addSoort;
        public SoortKlant AddSoort
        {
            get
            {
                return _addSoort;
            }
            private set
            {
                _addSoort = value;
                NoticeMe("AddSoort");
            }
        }
        #endregion

        #region SelectedSoort fullproperty + propertychanged event
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
            //SelectedOphaling.Check();

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

        public SoortKlantBeheerViewModel()
        {
            LoadData();
            AddSoort = new SoortKlant();
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
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.SoortKlant.Add(AddSoort);
            ctx.SaveChanges();
            Refresh();

            AddSoort = new SoortKlant();
        }
    }
}
