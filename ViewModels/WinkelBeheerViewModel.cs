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
    class WinkelBeheerViewModel : BeheerViewModel
    {
        #region AddWinkel fullproperty
        private Winkel _addWinkel;
        public Winkel AddWinkel
        {
            get
            {
                return _addWinkel;
            }
            private set
            {
                _addWinkel = value;
                NoticeMe("AddWinkel");
            }
        }
        #endregion

        #region SelectedWinkel fullproperty + propertychanged event
        private Winkel _selectedWinkel;
        public Winkel SelectedWinkel {
            get
            {
                return _selectedWinkel;
            }
            set
            {
                _selectedWinkel = value;
                NoticeMe("SelectedWinkel");
                if (value != null)
                    _selectedWinkel.PropertyChanged += _selectedWinkel_PropertyChanged;
            }
        }

        private void _selectedWinkel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //ADD Datavalidation
            //SelectedWinkel.Check();

            ctx.SaveChanges();
        }

        #endregion
        public override bool IsValid        //Used to disable or Enable Toevoegen button
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public WinkelBeheerViewModel()
        {
            LoadData();
            AddWinkel = new Winkel();
        }

        public void LoadData()
        {
            Refresh();
        }

        public override void Delete()
        {
            ctx.Winkels.Remove(SelectedWinkel);
            ctx.SaveChanges();
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Winkels.Load();
            ViewSource.Source = ctx.Winkels.Local;
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.Winkels.Add(AddWinkel);
            ctx.SaveChanges();
            AddWinkel = new Winkel();
        }
    }
}
