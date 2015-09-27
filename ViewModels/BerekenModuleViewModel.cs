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
        protected ISIS_DataEntities ctx;
        public WorkspaceViewModel CurrentView { get; set; }

        public SearchBoxKlantViewModel SearchBoxViewModel { get; set; }

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
                NoticeMe("SelectedKlant");
            }
        }

        public ICollectionView KlantenView
        {
            get
            {
                ctx.Klanten.Load();
                return CollectionViewSource.GetDefaultView(ctx.Klanten.Local);
            }
        }

        public BerekenModuleViewModel()
        {
            Header = "Berekenmodule";
            SearchBoxViewModel = new SearchBoxKlantViewModel(this);
            CurrentView = new StukBerekenModuleViewModel();
            ctx = new ISIS_DataEntities();
        }
    }
}
