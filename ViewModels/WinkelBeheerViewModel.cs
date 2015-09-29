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
        private SoortKlant _addWinkel;
        public SoortKlant AddWinkel
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
        private SoortKlant _selectedSoort;
        public SoortKlant SelectedSoort {
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
            AddWinkel = new SoortKlant();
            AddWinkel.Type = "Winkel";
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
            ViewSource.Source = ctx.SoortKlant.Local.Where(s => s.Type == "Winkel");
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.SoortKlant.Add(AddWinkel);
            ctx.SaveChanges();
            AddWinkel = new SoortKlant();
            AddWinkel.Type = "Winkel";
        }
    }
}
