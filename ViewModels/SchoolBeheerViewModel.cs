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
    class SchoolBeheerViewModel : BeheerViewModel
    {
        #region AddSchool fullproperty
        private SoortKlant _addSchool;
        public SoortKlant AddSchool
        {
            get
            {
                return _addSchool;
            }
            private set
            {
                _addSchool = value;
                NoticeMe("AddSchool");
            }
        }
        #endregion

        #region SelectedSchool fullproperty + propertychanged event
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
            //SelectedSchool.Check();

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

        public SchoolBeheerViewModel()
        {
            LoadData();
            AddSchool = new SoortKlant();
            AddSchool.Type = "School";
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
            ViewSource.Source = ctx.SoortKlant.Local.Where(s => s.Type == "School");
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.SoortKlant.Add(AddSchool);
            ctx.SaveChanges();
            AddSchool = new SoortKlant();
            AddSchool.Type = "School";
        }
    }
}
