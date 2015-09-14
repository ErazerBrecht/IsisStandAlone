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
    class PrestatieBeheerViewModel : WorkspaceViewModel, ISelectedKlant
    {
        ISIS_DataEntities ctx;
        public SearchBoxKlantViewModel SearchBoxViewModel { get; set; }
        public List<Prestatie> Prestaties { get; private set; }

        private bool _enableSearch;
        public bool EnableSearch
        {
            get
            {
                return _enableSearch;
            }
            private set
            {
                _enableSearch = value;
                NoticeMe("EnableSearch");
            }
        }

        private bool _boolIedereen;
        public bool BoolIedereen
        {
            get
            {
                return _boolIedereen;
            }
            set
            {
                _boolIedereen = value;
                NoticeMe("BoolIedereen");

                if (value == true)
                {
                    LoadData();
                    EnableSearch = false;
                }
                else
                {
                    EnableSearch = true;
                }
            }
        }

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
                Prestaties = Prestaties.Where(p => p.KlantenNummer == _selectedKlant.ID).ToList();
                NoticeMe("Prestaties");
            }
        }

        public ICollectionView KlantenView
        {
            get
            {
                CollectionViewSource viewSource = new CollectionViewSource();
                viewSource.Source = ctx.Klanten.ToList();
                return viewSource.View;
            }
        }

        public PrestatieBeheerViewModel()
        {
            Header = "PrestatieBeheer";
            LoadData();
            BoolIedereen = true;
            SearchBoxViewModel = new SearchBoxKlantViewModel(this);
        }

        private void LoadData()
        {
            ctx = new ISIS_DataEntities();
            Prestaties = ctx.Prestaties.ToList();
            NoticeMe("Prestaties");
        }
    }
}
