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
    public partial class PersoneelBeheer : System.Windows.Controls.UserControl
    {
        ISIS_DataEntities _entities;
        CollectionViewSource _PersoneelViewSource;
        Strijkers _addPersoneel;
        bool _unsavedChanges;

        public PersoneelBeheer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _PersoneelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("strijkersViewSource")));
            Refresh();
            var window = Window.GetWindow(this);
            window.Closing += window_Closing;
            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        void window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = CheckChanges();
        }        

        private void Refresh()
        {
            _unsavedChanges = false;

            if (_entities != null)
                _entities.Dispose();

            _entities = new ISIS_DataEntities();
            //_entities.Strijkers.Local.CollectionChanged += Local_CollectionChanged;
            _entities.Strijkers.Load();
            _PersoneelViewSource.Source = _entities.Strijkers.Local;
            ButtonAdd.Content = "Add";
            GridInformation.DataContext = _PersoneelViewSource;
        }

        /*void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Strijkers item in e.OldItems)
                {
                    //Removed items => Delete event
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Strijkers item in e.NewItems)
                {
                    //Added items => Add event
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }
        }*/

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
            if (_addPersoneel != null)
            {
                _entities.Strijkers.Add(_addPersoneel);
            }

            _entities.SaveChanges();
            _unsavedChanges = false;
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (_PersoneelViewSource.View.CurrentPosition < _entities.Strijkers.Local.Count() - 1)
                _PersoneelViewSource.View.MoveCurrentToNext();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (_PersoneelViewSource.View.CurrentPosition > 0)
                _PersoneelViewSource.View.MoveCurrentToPrevious();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            CheckChanges();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            _entities.Strijkers.Remove((Strijkers)_PersoneelViewSource.View.CurrentItem);
            _unsavedChanges = true;
            //_entities.SaveChanges();
            //Refresh();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonAdd.Content.ToString() == "Add")
            {
                _addPersoneel = new Strijkers();
                GridInformation.DataContext = _addPersoneel;
                ButtonAdd.Content = "Cancel";
            }
            else
            {
                GridInformation.DataContext = _PersoneelViewSource;
                ButtonAdd.Content = "Add";
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Refresh();
        }
    }
}
