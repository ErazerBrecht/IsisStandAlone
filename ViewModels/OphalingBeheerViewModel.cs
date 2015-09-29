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
    class OphalingBeheerViewModel : BeheerViewModel
    {
        #region AddOphaling fullproperty
        private SoortKlant _addOphaling;
        public SoortKlant AddOphaling
        {
            get
            {
                return _addOphaling;
            }
            private set
            {
                _addOphaling = value;
                NoticeMe("AddOphaling");
            }
        }
        #endregion

        #region SelectedOphaling fullproperty + propertychanged event
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

        public OphalingBeheerViewModel()
        {
            LoadData();
            AddOphaling = new SoortKlant();
            AddOphaling.Type = "Ophaling";
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
            ViewSource.Source = ctx.SoortKlant.Local.Where(s => s.Type == "Ophaling");
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.SoortKlant.Add(AddOphaling);
            ctx.SaveChanges();
            AddOphaling = new SoortKlant();
            AddOphaling.Type = "Ophaling";
        }
    }
}
