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

        private List<TijdPrestatie> _originalPrestaties;

        private List<TijdPrestatie> _prestaties;
        public List<TijdPrestatie> Prestaties
        {
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

        //#region FilterOnName
        //private bool _enableSearch;
        //public bool EnableSearch
        //{
        //    get
        //    {
        //        return _enableSearch;
        //    }
        //    private set
        //    {
        //        _enableSearch = value;
        //        NoticeMe("EnableSearch");
        //    }
        //}

        //private bool _boolIedereen;
        //public bool BoolIedereen
        //{
        //    get
        //    {
        //        return _boolIedereen;
        //    }
        //    set
        //    {
        //        _boolIedereen = value;
        //        NoticeMe("BoolIedereen");

        //        if (value == true)
        //        {
        //            EnableSearch = false;
        //        }
        //        else
        //        {
        //            EnableSearch = true;
        //        }

        //        Filter();
        //    }
        //}
        //#endregion

        //#region FilterOnDate

        //private bool _filterOpDag;

        //public bool FilterOpDag
        //{
        //    get { return _filterOpDag; }
        //    set
        //    {
        //        _filterOpDag = value;
        //        Filter();
        //        NoticeMe("FilterOpDag");
        //    }
        //}


        //private bool _boolDag;

        //public bool BoolDag
        //{
        //    get
        //    {
        //        return _boolDag;
        //    }
        //    set
        //    {
        //        _boolDag = value;
        //        Filter();
        //        NoticeMe ("BoolDag");
        //    }
        //}

        //private DateTime _dateSingle;
        //public DateTime DateSingle
        //{
        //    get
        //    {
        //        return _dateSingle;
        //    }
        //    set
        //    {
        //        _dateSingle = value;
        //        Filter();
        //    }
        //}

        //private DateTime? _dateFirst;

        //public DateTime? DateFirst
        //{
        //    get { return _dateFirst; }
        //    set
        //    {
        //        _dateFirst = value;
        //        NoticeMe("DateFirst");

        //        Filter();    
        //    }
        //}


        //private DateTime? _dateSecond;

        //public DateTime? DateSecond
        //{
        //    get { return _dateSecond; }
        //    set { _dateSecond = value; Filter(); NoticeMe("DateSecond"); }
        //}

        //#endregion

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

        public PrestatieBeheerViewModel()
        {
            Header = "PrestatieBeheer";
            LoadData();
            //BoolIedereen = true;
            SearchBoxViewModel = new SearchBoxKlantViewModel(this);
            //BoolDag = true;
        }

        public override void LoadData()
        {
            ctx = new ISIS_DataEntities();
            _originalPrestaties = ctx.TijdPrestaties.ToList();
            Prestaties = _originalPrestaties;
        }

        public void Filter()
        {
            //if (BoolIedereen)
            //    Prestaties = _originalPrestaties;
            //else
            //{
            //    if (SelectedKlant != null)
            //        Prestaties = _originalPrestaties.Where(p => p.KlantenNummer == _selectedKlant.ID).ToList();
            //}

            //if(FilterOpDag)
            //{
            //    if (BoolDag)
            //    {
            //        if (DateSingle != null)
            //            Prestaties = Prestaties.Where(p => p.Datum.Date == DateSingle.Date).ToList();
            //    }
            //    else
            //    {
            //        if (DateSecond != null && DateFirst != null)
            //            Prestaties = Prestaties.Where(p => p.Datum.Date >= DateFirst.GetValueOrDefault() && p.Datum.Date <= DateSecond.GetValueOrDefault()).ToList();
            //    }

            //}

        }
    }
}
