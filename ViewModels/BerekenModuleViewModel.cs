using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ISIS.ViewModels
{
    class BerekenModuleViewModel: WorkspaceViewModel, ISelectedKlant
    {
        public DatumBeheerViewModel DatumViewModel { get; set; }
        public SearchBoxKlantViewModel SearchBoxViewModel { get; set; }

        #region CurrentView full property
        private ISelectedKlant _currentView;

        public ISelectedKlant CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                NoticeMe("CurrentView");
            }
        }
        #endregion

        private Klant _selectedKlant;
        public Klant SelectedKlant
        {
            get
            {
                return _selectedKlant;
            }

            set
            {
                _selectedKlant = value;
                if (_selectedKlant.SoortKlant.StukTarief)
                    CurrentView = new StukBerekenModuleViewModel(ctx);
                else
                    CurrentView = new TijdBerekenModuleViewModel(ctx);
                CurrentView.SelectedKlant = value;
                NoticeMe("SelectedKlant");
            }
        }

        public BerekenModuleViewModel()
        {
            Header = "Berekenmodule";
            ctx = new ISIS_DataEntities();
            DatumViewModel = new DatumBeheerViewModel();
            SearchBoxViewModel = new SearchBoxKlantViewModel(this);
            //CurrentView = new StukBerekenModuleViewModel(ctx);
        }
    }
}
