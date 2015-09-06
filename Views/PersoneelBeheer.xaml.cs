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

namespace ISIS.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PersoneelBeheer : System.Windows.Controls.UserControl
    {
        //ISIS_DataEntities _entities;
        //CollectionViewSource _PersoneelViewSource;
        //Strijker _addPersoneel;
        //bool _unsavedChanges;
        //int _numberofErrors = 0;

        public PersoneelBeheer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //_PersoneelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("strijkersViewSource")));
            //Refresh();
            //SwitchToEditMode();
        }      

        //private void Refresh()
        //{
        //    _unsavedChanges = false;

        //    if (_entities != null)
        //        _entities.Dispose();

        //    _entities = new ISIS_DataEntities();
        //    _entities.Strijkers.Local.CollectionChanged += Local_CollectionChanged;
        //    _entities.Strijkers.Load();
        //    _PersoneelViewSource.Source = _entities.Strijkers.Local;
        //    ButtonAdd.Content = "Toevoegen";
        //    GridInformation.DataContext = _PersoneelViewSource;
        //}

        //void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == NotifyCollectionChangedAction.Remove)
        //    {
        //        foreach (Strijker item in e.OldItems)
        //        {
        //            //Removed items => Delete event
        //            item.PropertyChanged -= EntityViewModelPropertyChanged;
        //        }
        //    }
        //    else if (e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        foreach (Strijker item in e.NewItems)
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
        //        MessageBoxResult result = MessageBox.Show("U bent nog een nieuw persooneelslid aan het aanmaken! Deze is nog niet opgeslagen!\nWilt u zeker verder gaan?", "Personeelbeheer", MessageBoxButton.YesNo);
        //        if (result == MessageBoxResult.No)
        //        {
        //            return true;
        //        }
        //    }

        //    if (_unsavedChanges == true)
        //    {
        //        MessageBoxResult result = MessageBox.Show("Er zijn nog onopgeslagen wijzigingen.\nWilt u deze wijzingen nog opslaan?", "Personeelbeheer", MessageBoxButton.YesNoCancel);
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
        //    catch
        //    {
        //        var error = _entities.GetValidationErrors();
        //        MessageBox.Show("Opslagen is niet gelukt!\n\nWegens volgende reden:\n" + error.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
        //    }
        //}

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            //if (_PersoneelViewSource.View.CurrentPosition < _entities.Strijkers.Local.Count() - 1)
            //{
            //    _PersoneelViewSource.View.MoveCurrentToNext();
            //    strijkersDataGrid.ScrollIntoView(strijkersDataGrid.SelectedItem);       //Make sure the datagrid follows (scroll), otherwise you can't see what you're doing
            //}
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            //if (_PersoneelViewSource.View.CurrentPosition > 0)
            //{
            //    _PersoneelViewSource.View.MoveCurrentToPrevious();
            //    strijkersDataGrid.ScrollIntoView(strijkersDataGrid.SelectedItem);       //Make sure the datagrid follows (scroll), otherwise you can't see what you're doing
            //}
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //CheckChanges();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            //_entities.Strijkers.Remove((Strijker)_PersoneelViewSource.View.CurrentItem);
            //_unsavedChanges = true;
            //_entities.SaveChanges();
            //Refresh();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //if (ButtonAdd.Content.ToString() == "Toevoegen")
            //{
            //    _addPersoneel = new Strijker();

            //    int tempId = 1;

            //    //Search for first valid ID
            //    while (_entities.Strijkers.Any(s => s.ID == tempId))
            //    {
            //        tempId++;
            //    }

            //    _addPersoneel.ID = tempId;
            //    _addPersoneel.IndienstVanaf = DateTime.Now;
            //    GridInformation.DataContext = _addPersoneel;
            //    SwitchToAddMode();
            //}
            //else
            //{
            //    _addPersoneel = null;       //Clicked on cancel ("Annuleren") so the employee doesn't have to be saved!
            //    SwitchToEditMode();
            //}
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            //SwitchToEditMode();

            //if (_addPersoneel != null)
            //{
            //    _entities.Strijkers.Add(_addPersoneel);
            //}

            //Save();
            //Refresh();
        }

        private void SwitchToAddMode()
        {
            //TextBoxID.IsReadOnly = false;
            //(GridInformation.DataContext as Strijker).CanValidateID = true;
            //ButtonAdd.Content = "Annuleren";
        }

        private void SwitchToEditMode()
        {
            //GridInformation.DataContext = _PersoneelViewSource;
            //TextBoxID.IsReadOnly = true;
            ////Get Klant that is currently bind to GridInformation (this equals to the currentitem of the klantenViewSource)
            ////Because the ID textbox is now readonly disable data validation!
            //(_PersoneelViewSource.View.CurrentItem as Strijker).CanValidateID = false;
            //ButtonAdd.Content = "Toevoegen";
        }

        private void GridInformation_Error(object sender, ValidationErrorEventArgs e)
        {
            //if (e.Action.ToString() == "Added")
            //{
            //    ButtonSave.IsEnabled = false;
            //    _numberofErrors++;
            //}
            //else
            //{
            //    _numberofErrors--;
            //    if (_numberofErrors < 1)
            //        ButtonSave.IsEnabled = true;
            //}
        }
    }
}
