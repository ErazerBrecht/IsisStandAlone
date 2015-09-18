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

        private List<Prestatie> _originalPrestaties;

        private List<Prestatie> _prestaties;
        public List<Prestatie> Prestaties {
            get
            {
                return _prestaties;
            }
            private set
            {
                _prestaties = value;
                NoticeMe("Prestaties");
            }
        }

        #region FilterOnName
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
                    EnableSearch = false;
                }
                else
                {
                    EnableSearch = true;
                }

                Filter();
            }
        }
        #endregion

        #region FilterOnDate

        private bool _filterOpDag;

        public bool FilterOpDag
        {
            get { return _filterOpDag; }
            set
            {
                _filterOpDag = value;

                if(value == false)          //TODO: Set radiobuttons to false and reset text datepickers
                {
                    EnableDateSecond = false;
                    Filter();
                }
                NoticeMe("FilterOpDag");
            }
        }


        private bool _boolDag;

        public bool BoolDag
        {
            get
            {
                return _boolDag;
            }
            set
            {
                _boolDag = value;
                Filter();
            }
        }

        private DateTime _dateSingle;
        public DateTime DateSingle
        {
            get
            {
                return _dateSingle;
            }
            set
            {
                _dateSingle = value;
                Filter();
            }
        }

        private DateTime _dateFirst;

        public DateTime DateFirst
        {
            get { return _dateFirst; }
            set
            {
                _dateFirst = value;

                Filter();

                if (_dateFirst != null)
                    EnableDateSecond = true;

                NoticeMe("DateFirst");
            }
        }


        private bool _enableDateSecond;

        public bool EnableDateSecond
        {
            get { return _enableDateSecond; }
            set { _enableDateSecond = value; NoticeMe("EnableDateSecond"); }
        }


        private DateTime _dateSecond;

        public DateTime DateSecond
        {
            get { return _dateSecond; }
            set { _dateSecond = value; Filter(); NoticeMe("DateSecond"); }
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
                Filter();
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

        public void LoadData()
        {
            ctx = new ISIS_DataEntities();
            _originalPrestaties = ctx.Prestaties.ToList();
            Prestaties = _originalPrestaties;
        }

        public void Filter()
        {
            if (BoolIedereen)
                Prestaties = _originalPrestaties;
            else
            {
                if (SelectedKlant != null)
                    Prestaties = _originalPrestaties.Where(p => p.KlantenNummer == _selectedKlant.ID).ToList();
            }

            if(FilterOpDag)
            {
                if (BoolDag)
                {
                    if (DateSingle != null)
                        Prestaties = Prestaties.Where(p => p.Datum.Date == DateSingle.Date).ToList();
                }
                else
                {
                    if (DateSecond != null && EnableDateSecond)
                        Prestaties = Prestaties.Where(p => p.Datum.Date >= DateFirst.Date && p.Datum.Date <= DateSecond.Date).ToList();
                }

            }

        }
    }
}
