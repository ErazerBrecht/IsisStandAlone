using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows.Controls.Primitives;

namespace ISIS.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class KlantenBeheer : System.Windows.Controls.UserControl
    {
        //ISIS_DataEntities _entities;
        //CollectionViewSource _klantenViewSource;
        //Klant _addClient;
        //bool _unsavedChanges;
        //int _oldLengthSearchBox = 0;
        //int _numberofErrors = 0;

        public KlantenBeheer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        //    _klantenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("klantenViewSource")));
        //    Refresh();
        //    SwitchToEditMode();
        }

        //private void Refresh()
        //{
        //    _unsavedChanges = false;

        //    if (_entities != null)
        //        _entities.Dispose();

        //    _entities = new ISIS_DataEntities();
        //    _entities.Klanten.Local.CollectionChanged += Local_CollectionChanged;
        //    _entities.Klanten.Load();
        //    _entities.Winkels.Local.CollectionChanged += SoortKlant_CollectionChanged;
        //    _entities.Winkels.Load();
        //    _klantenViewSource.Source = _entities.Klanten.Local;
        //    TextBoxSearch.ItemsSource = _entities.Klanten.Local;
        //    ButtonAdd.Content = "Toevoegen";
        //    GridInformation.DataContext = _klantenViewSource;
        //}

        //private void SoortKlant_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    AddItemmSourceComboBoxSoortKlant();
        //}

        //void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == NotifyCollectionChangedAction.Remove)
        //    {
        //        foreach (Klant item in e.OldItems)
        //        {
        //            //Removed items => Delete event
        //            item.PropertyChanged -= EntityViewModelPropertyChanged;
        //        }
        //    }
        //    else if (e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        foreach (Klant item in e.NewItems)
        //        {
        //            //Added items => Add event
        //            item.PropertyChanged += EntityViewModelPropertyChanged;
        //        }
        //    }
        //}

        //public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    //This will get called when the property of an object inside the collection changes
        //    if (e.PropertyName != "CanValidateID")     //There isn't actual data changed in that property
        //        _unsavedChanges = true;
        //}

        //public bool CheckChanges()
        //{
        //    if (ButtonAdd.Content.ToString() == "Annuleren")
        //    {
        //        MessageBoxResult result = MessageBox.Show("U bent nog een nieuwe klant aan het aanmaken! Deze is nog niet opgeslagen!\nWilt u zeker verder gaan?", "Klantenbeheer", MessageBoxButton.YesNo);
        //        if (result == MessageBoxResult.No)
        //        {
        //            return true;
        //        }
        //    }

        //    if (_unsavedChanges == true)
        //    {
        //        MessageBoxResult result = MessageBox.Show("Er zijn nog onopgeslagen wijzigingen.\nWilt u deze wijzingen nog opslaan?", "KlantenBeheer", MessageBoxButton.YesNoCancel);
        //        if (result == MessageBoxResult.Yes)
        //        {
        //            Save();
        //        }
        //        else if (result == MessageBoxResult.No)
        //        {
        //            Refresh();
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        //private void Save()
        //{
        //    try
        //    {
        //        _entities.SaveChanges();
        //        _unsavedChanges = false;
        //    }
        //    catch (Exception e)
        //    {
        //        var error = _entities.GetValidationErrors();
        //        if (error != null)
        //            MessageBox.Show("Opslagen is niet gelukt!\n\nWegens volgende reden:\n" + error.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
        //        else
        //            MessageBox.Show(e.ToString());
        //    }

        //}

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
        //    CheckChanges();
        }
    }

    public class SearchComboBox : ComboBox
    {

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            //Disables automatic selection when DropDownsOpen!
            if (this.IsEditable &&
                this.IsDropDownOpen == false &&
                this.StaysOpenOnEdit)
            {
                this.IsDropDownOpen = true;
            }
        }

    }
}
