using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
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
using PropertyChanged;

namespace PL_WPF.ViewModels
{
    class DatumBeheerViewModel : INotifyPropertyChanged
    {
        private UnitOfWork _ctx;
        private int _id;

        #region AddDatum full prop
        public Datum AddDatum
        {
            get { return _addDatum; }
            set
            {
                _addDatum = value;
                if (_addDatum != null)
                    _addDatum.PropertyChanged += _addDatum_PropertyChanged;
            }
        }

        private void _addDatum_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NoticeMe("IsValid");
        }
        #endregion

        #region SelectedDatum full prop
        private Datum _selectedDatum;
        private Datum _addDatum;

        public Datum SelectedDatum
        {
            get { return _selectedDatum; }
            set
            {
                _selectedDatum = value;
                if (_selectedDatum != null)
                {
                    //_selectedDatum.PropertyChanged += Datum_PropertyChanged;
                }
            }
        }
        #endregion

        public  CollectionViewSource ViewSource {get; private set;}

        public ObservableCollection<Datum> NewDates { get; set; }

        public IEnumerable<Strijker> Strijkers => _ctx.Strijkers.GetAll();

        #region Commands
        public ICommand AddCommandEvent { get; private set; }
        public ICommand DeleteCommandEvent { get; private set; }
        #endregion

        //Used to disable or Enable Toevoegen button
        public bool IsValid => AddDatum.CanSave;

        public bool HasDates
        {
            get
            {
                return ViewSource.View.Cast<Datum>().Any();
            }
        }

        public DatumBeheerViewModel(UnitOfWork ctx)
        {
            _ctx = ctx;
            Init();

            AddCommandEvent = new RelayCommand(Add);
            DeleteCommandEvent = new RelayCommand(Delete);
        }

        public void Init()
        {
            //_id = 0 => Add datum(s) to a new prestatie
            //_id != 0 => Add datum(s) to an existing prestatie. The id of this prestatie will be saved in this variable
            _id = 0;
            NewDates = new ObservableCollection<Datum>();
            AddDatum = new Datum {Date = DateTime.Now};

            if (ViewSource != null)
                ViewSource.Source = NewDates;
            else
            {
                ViewSource = new CollectionViewSource {Source = NewDates};
                //DataGrid ordenen op datum (oudste vanboven)
                ViewSource.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));        
            }
        }

        public void Delete()
        {
            if (_id == 0)
                NewDates.Remove(SelectedDatum);
            else
                _ctx.Datums.Remove(SelectedDatum);
        }

        public void Add()
        {
            var duplicate = ViewSource.View.Cast<Datum>().FirstOrDefault(d => d.Date.Date == AddDatum.Date.Date);
            if (duplicate == null)
            {
                //Add new date to a new prestatie
                if (_id == 0)
                    NewDates.Add(AddDatum);
                //Add new date to an existing prestatie
                else
                {
                    AddDatum.PrestatieId = _id;
                    _ctx.Datums.Add(AddDatum);
                }

                AddDatum = new Datum { Date = DateTime.Now };
            }
            else
            {
                MessageBoxService messageBoxService = new MessageBoxService();
                messageBoxService.ShowMessageBox("Deze datum is al toegevoegd!");
            }
        }

        public void EditLast(int id)
        {
            _id = id;
            ViewSource.Source = _ctx.Datums.GetAll();
            ViewSource.Filter += ViewSource_Filter;
        }

        private void ViewSource_Filter(object sender, FilterEventArgs e)
        {
            Datum datum = e.Item as Datum;
            if (datum.PrestatieId == _id)
                e.Accepted = true;
            else
                e.Accepted = false;
        }

        #region INotifyPropertyChanged
        protected void NoticeMe(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tempStrijker = value as Strijker;

            if (tempStrijker?.Id != null)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
