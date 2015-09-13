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
        private Ophaling _addOphaling;
        public Ophaling AddOphaling
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
        private Ophaling _selectedOphaling;
        public Ophaling SelectedOphaling
        {
            get
            {
                return _selectedOphaling;
            }
            set
            {
                _selectedOphaling = value;
                NoticeMe("SelectedOphaling");
                if (value != null)
                    _selectedOphaling.PropertyChanged += _selectedOphaling_PropertyChanged;
            }
        }

        private void _selectedOphaling_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
            AddOphaling = new Ophaling();
        }

        public void LoadData()
        {
            Refresh();
        }

        public override void Delete()
        {
            ctx.Ophalingen.Remove(SelectedOphaling);
            ctx.SaveChanges();
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Ophalingen.Load();
            ViewSource.Source = ctx.Ophalingen.Local;
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.Ophalingen.Add(AddOphaling);
            ctx.SaveChanges();
            AddOphaling = new Ophaling();
        }
    }
}
