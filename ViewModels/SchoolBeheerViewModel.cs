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
        private School _addSchool;
        public School AddSchool
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
        private School _selectedSchool;
        public School SelectedSchool
        {
            get
            {
                return _selectedSchool;
            }
            set
            {
                _selectedSchool = value;
                NoticeMe("SelectedSchool");
                if (value != null)
                    _selectedSchool.PropertyChanged += _selectedSchool_PropertyChanged;
            }
        }

        private void _selectedSchool_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
            AddSchool = new School();
        }

        public void LoadData()
        {
            Refresh();
        }

        public override void Delete()
        {
            ctx.Scholen.Remove(SelectedSchool);
            ctx.SaveChanges();
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Scholen.Load();
            ViewSource.Source = ctx.Scholen.Local;
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.Scholen.Add(AddSchool);
            ctx.SaveChanges();
            AddSchool = new School();
        }
    }
}
