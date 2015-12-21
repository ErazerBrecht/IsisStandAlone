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

        #region Strings that will overwrite text in searchbox for strijksters, will use these to clear them after adding a data
        public string ClearAutoCompleteBox1 { get; set; }
        public string ClearAutoCompleteBox2 { get; set; }
        public string ClearAutoCompleteBox3 { get; set; }
        public string ClearAutoCompleteBox4 { get; set; }
        public string ClearAutoCompleteBox5 { get; set; }
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
            AddDatum.PropertyChanged += AddDatum_PropertyChanged;
        }

        private void AddDatum_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Strijker1")
            {
                if (AddDatum.Strijker1 == null)
                {
                    if (AddDatum.Strijker2 != null)
                    {
                        var temp = AddDatum.Strijker2;
                        AddDatum.Strijker2 = null;
                        AddDatum.Strijker1 = temp;
                        //AddDatum.Strijker2 = null;
                    }
                }
            }

            else if (e.PropertyName == "Strijker2")
            {
                if (AddDatum.Strijker2?.Naam == null)
                {
                    if (AddDatum.Strijker3 != null)
                    {
                        var temp = AddDatum.Strijker3;
                        AddDatum.Strijker3 = null;
                        AddDatum.Strijker2 = temp;
                        //AddDatum.Strijker3 = null;
                    }
                }
            }

            else if (e.PropertyName == "Strijker3")
            {
                if (AddDatum.Strijker3?.Naam == null)
                {
                    if (AddDatum.Strijker4 != null)
                    {
                        var temp = AddDatum.Strijker4;
                        AddDatum.Strijker4 = null;
                        AddDatum.Strijker3 = temp;
                       // AddDatum.Strijker4 = null;
                    }
                }
            }

            else if (e.PropertyName == "Strijker4")
            {
                if (AddDatum.Strijker4?.Naam == null)
                {
                    if (AddDatum.Strijker5 != null)
                    {
                        var temp = AddDatum.Strijker5;
                        AddDatum.Strijker5 = null;
                        AddDatum.Strijker4 = temp;
                       // AddDatum.Strijker5 = null;
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

        public AutoCompleteFilterPredicate<object> StrijkerFilter
        {
            get
            {
                return (searchText, obj) =>
                    Convert.ToString((obj as Strijker).ID).Contains(searchText) || (obj as Strijker).Naam.Contains(searchText);
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
            AddDatum.PropertyChanged += AddDatum_PropertyChanged;

        }

        public override void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }

    public class NullToBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Strijker && values[1] is Datum)
            {
                if (((Strijker) values[0]) != null && ((Datum) values[1]).CanSave)
                    return true;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StrijkerStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            else
                return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
