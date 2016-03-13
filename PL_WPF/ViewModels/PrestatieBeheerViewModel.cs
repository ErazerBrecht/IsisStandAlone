using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DAL_Repository;
using EF_Model;
using GalaSoft.MvvmLight.Command;
using PL_WPF.Services;

namespace PL_WPF.ViewModels
{
    class PrestatieBeheerViewModel : WorkspaceViewModel
    {
        public CollectionViewSource ViewSource { get; private set; }

        public ICommand PrintCommandEvent { get; private set; }

        #region SelectedPrestatie full - property

        private Prestatie _selectedPrestatie;
        public Prestatie SelectedPrestatie {
            get { return _selectedPrestatie; }
            set
            {
                _selectedPrestatie = value;
                NoticeMe("SelectedPrestatie");
            }
        }
        #endregion

        #region Klanten Searchbox
        #region SelectedKlant full property
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
                ViewSource.View.Refresh();

            }
        }
        #endregion

        #region Klanten list for searchbox full property
        private IEnumerable<Klant> _klanten;
        public IEnumerable<Klant> Klanten
        {
            get { return _klanten; }
            set
            {
                _klanten = value;
                NoticeMe("Klanten");
            }
        }
        #endregion
        public AutoCompleteFilterPredicate<object> KlantenFilter
        {
            get
            {
                return (searchText, obj) =>
                {
                    var klant = obj as Klant;
                    return klant != null && (Convert.ToString(klant.Id).Contains(searchText) || klant.Naam.ToUpper().Contains(searchText.ToUpper()) || (!string.IsNullOrEmpty(klant.Voornaam) && klant.Voornaam.ToUpper().Contains(searchText.ToUpper())));
                };
            }
        }
        #endregion

        #region FilterOnName
        //Bool to make Searbox Enabled or Disabled
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

        //Bool to ignore SelectedKlant and show every prestatie
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

                EnableSearch = !value;

                ViewSource.View.Refresh();
            }
        }
        #endregion

        #region FilterOnDate
        //Bool to indicate you want to filter on dates
        private bool _filterOpDag;

        public bool FilterOpDag
        {
            get { return _filterOpDag; }
            set
            {
                _filterOpDag = value;
                NoticeMe("FilterOpDag");
                ViewSource.View.Refresh();
            }
        }

        //Bool to indicate if you want to filter on a single day or between a range of dates
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
                NoticeMe("BoolDag");
                ViewSource.View.Refresh();
            }
        }

        //Date that will be used when we filter on a single day
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
                ViewSource.View.Refresh();
            }
        }

        //Date we use as first date in our range of dates
        private DateTime _dateFirst;

        public DateTime DateFirst
        {
            get { return _dateFirst; }
            set
            {
                _dateFirst = value;
                NoticeMe("DateFirst");
                ViewSource.View.Refresh();
            }
        }

        //Date we use as last date in our range of dates
        private DateTime _dateSecond;

        public DateTime DateSecond
        {
            get { return _dateSecond; }
            set
            {
                _dateSecond = value;
                NoticeMe("DateSecond");
                ViewSource.View.Refresh();
            }
        }

        #endregion

        public PrestatieBeheerViewModel(UnitOfWork ctx) : base(ctx)
        {
            Header = "Prestatiebeheer";

            ViewSource = new CollectionViewSource { Source = Ctx.Prestaties.GetAll() };
            ViewSource.View.Filter = Filter;
            Klanten = Ctx.Klanten.GetAll();

            PrintCommandEvent = new RelayCommand(Print);
            BoolIedereen = true;
            BoolDag = true;
        }

        public bool Filter(object item)
        {
            Prestatie prestatie = item as Prestatie;

            if (!BoolIedereen)
            {
                if (SelectedKlant != null)
                    return prestatie.Klant.Id == SelectedKlant.Id;
            }

            if (FilterOpDag)
            {
                if (BoolDag)
                {
                    return prestatie.Datum.Any(p => p.Date.Date == DateSingle);
                }

                return prestatie.Datum[0].Date.Date >= DateFirst && prestatie.Datum[prestatie.Datum.Count - 1].Date.Date <= DateSecond;
            }

            return true;

        }

        public void Print()
        {
            PrintWindow print = new PrintWindow();
            print.ShowPrintPreview(SelectedPrestatie);
        }
    }
}
