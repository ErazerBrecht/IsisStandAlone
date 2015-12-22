using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using ISIS.Models;
using ISIS.Commands;
using PropertyChanged;

namespace ISIS.ViewModels
{
    [ImplementPropertyChanged]
    class DatumBeheerViewModel : BeheerViewModel
    {

        public Datum AddDatum { get; set; }
        public ObservableCollection<Datum> CurrentDates { get; set; }
        public IEnumerable<Strijker> Strijkers { get; set; }

        #region SelectedDatum full prop
        private Datum _selectedDatum;
        public Datum SelectedDatum
        {
            get { return _selectedDatum; }
            set
            {
                _selectedDatum = value;
                if (_selectedDatum != null)
                {
                    _selectedDatum.PropertyChanged += Datum_PropertyChanged;
                }
            }
        }
        #endregion

        //Used to disable or Enable Toevoegen button
        public override bool IsValid
        {
            get { return AddDatum.CanSave; }
        }

        public DatumBeheerViewModel()
        {
            CurrentDates = new ObservableCollection<Datum>();
            AddDatum = new Datum { Date = DateTime.Now };
            AddDatum.PropertyChanged += Datum_PropertyChanged;
        }

        private void Datum_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //I only check for properties that start with a 'S', so if it doesn't start with a 'S' => immediately stop with checking!
            if (e.PropertyName[0] != 'S')
                return;

            var date = (Datum)sender;
            if (e.PropertyName == "Strijker1")
            {
                if (date.Strijker1 == null)
                {
                    if (date.Strijker2 != null)
                    {
                        //Why this works I don't f*cking know
                        //First I did this:
                        //date.Strijker1 = date.Strijker2;
                        //date.Strijker2 = null;
                        //This didn't work...
                        //This works, I don't know why...
                        var temp = date.Strijker2;
                        date.Strijker2 = null;
                        date.Strijker1 = temp;
                    }
                }
            }

            else if (e.PropertyName == "Strijker2")
            {
                if (date.Strijker2 == null)
                {
                    if (date.Strijker3 != null)
                    {
                        var temp = date.Strijker3;
                        date.Strijker3 = null;
                        date.Strijker2 = temp;
                    }
                }
            }

            else if (e.PropertyName == "Strijker3")
            {
                if (date.Strijker3 == null)
                {
                    if (date.Strijker4 != null)
                    {
                        var temp = date.Strijker4;
                        date.Strijker4 = null;
                        date.Strijker3 = temp;
                    }
                }
            }

            else if (e.PropertyName == "Strijker4")
            {
                if (date.Strijker4 == null)
                {
                    if (date.Strijker5 != null)
                    {
                        var temp = date.Strijker5;
                        date.Strijker5 = null;
                        date.Strijker4 = temp;
                    }
                }
            }
        }

        //Gets called by MainvViewModel
        public override void LoadData()
        {
            using (var context = new ISIS_DataEntities())
            {
                Strijkers = context.Strijkers.ToList();
            }
        }

        public override void Delete()
        {
            //TODO
        }

        public override void Refresh()
        {
            CurrentDates = new ObservableCollection<Datum>();
        }

        public override void Add()
        {
            CurrentDates.Add(AddDatum);
            AddDatum = new Datum { Date = DateTime.Now };
            AddDatum.PropertyChanged += Datum_PropertyChanged;
        }

        public override void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }

    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tempStrijker = value as Strijker;

            if (tempStrijker != null)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
