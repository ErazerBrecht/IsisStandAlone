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

namespace ISIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        ISIS_KlantenEntities _entities;
        CollectionViewSource _klantenViewSource;
        Klanten _addClient;
        bool _unsavedChanges;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _klantenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("klantenViewSource")));
            Refresh();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = CheckChanges();
        }

        private void Refresh()
        {
            _unsavedChanges = false;

            if (_entities != null)
                _entities.Dispose();

            _entities = new ISIS_KlantenEntities();
            _entities.Klanten.Local.CollectionChanged += Local_CollectionChanged;
            _entities.Klanten.Load();
            _klantenViewSource.Source = _entities.Klanten.Local;
            ButtonAdd.IsEnabled = true;
            GridInformation.DataContext = _klantenViewSource;
        }

        void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Klanten item in e.OldItems)
                {
                    //Removed items => Delete event
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Klanten item in e.NewItems)
                {
                    //Added items => Add event
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }
        }

        public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //This will get called when the property of an object inside the collection changes
            _unsavedChanges = true;
        }

        private bool CheckChanges()
        {
            if (_unsavedChanges == true)
            {
                MessageBoxResult result = MessageBox.Show("Er zijn nog onopgeslagen wijzigingen.\nWilt u deze wijzingen nog opslaan?", "ISIS", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    Save();
                }
                else if (result == MessageBoxResult.No)
                {
                    Refresh();
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private void Save()
        {
            if (_addClient != null)
            {
                _entities.Klanten.Add(_addClient);
            }

            _entities.SaveChanges();
            _unsavedChanges = false;
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (_klantenViewSource.View.CurrentPosition < _entities.Klanten.Local.Count() - 1)
                _klantenViewSource.View.MoveCurrentToNext();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (_klantenViewSource.View.CurrentPosition > 0)
                _klantenViewSource.View.MoveCurrentToPrevious();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            CheckChanges();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Confirm the action
            _entities.Klanten.Remove((Klanten)_klantenViewSource.View.CurrentItem);
            _entities.SaveChanges();
            Refresh();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            _addClient = new Klanten();
            StackPanelInfo.DataContext = _addClient;

            ButtonAdd.IsEnabled = false;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Refresh();
        }
    }
}
