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
        private Bedrijf _addBedrijf;
        public Bedrijf AddBedrijf
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
        private Bedrijf _selectedBedrijf;
        public Bedrijf SelectedBedrijf
        {
            get
            {
                return _selectedBedrijf;
            }
            set
            {
                _selectedBedrijf = value;
                NoticeMe("SelectedBedrijf");
                if (value != null)
                    _selectedBedrijf.PropertyChanged += _selectedBedrijf_PropertyChanged;
            }
        }

        private void _selectedBedrijf_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
            AddBedrijf = new Bedrijf();
        }

        public void LoadData()
        {
            Refresh();
        }

        public override void Delete()
        {
            ctx.Bedrijven.Remove(SelectedBedrijf);
            ctx.SaveChanges();
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Bedrijven.Load();
            ViewSource.Source = ctx.Bedrijven.Local;
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.Bedrijven.Add(AddBedrijf);
            ctx.SaveChanges();
            AddBedrijf = new Bedrijf();
        }
    }
}
