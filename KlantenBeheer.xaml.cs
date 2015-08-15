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

namespace ISIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class KlantenBeheer : System.Windows.Controls.UserControl
    {
        ISIS_DataEntities _entities;
        CollectionViewSource _klantenViewSource;
        Klanten _addClient;
        bool _unsavedChanges;
        int _oldLengthSearchBox = 0;

        public KlantenBeheer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _klantenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("klantenViewSource")));
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
            _entities.Klanten.Local.CollectionChanged += Local_CollectionChanged;
            _entities.Klanten.Load();
            _klantenViewSource.Source = _entities.Klanten.Local;
            TextBoxSearch.ItemsSource = _entities.Klanten.Local;
            ButtonAdd.Content = "Toevoegen";
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
            if (e.PropertyName != "CanSave")     //There isn't actual data changed in that property
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
            {
                _klantenViewSource.View.MoveCurrentToNext();
                klantenDataGrid.ScrollIntoView(klantenDataGrid.SelectedItem);       //Make sure the datagrid follows (scroll), otherwise you can't see what you're doing
            }
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (_klantenViewSource.View.CurrentPosition > 0)
            {
                _klantenViewSource.View.MoveCurrentToPrevious();
                klantenDataGrid.ScrollIntoView(klantenDataGrid.SelectedItem);       //Make sure the datagrid follows (scroll), otherwise you can't see what you're doing
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            CheckChanges();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            _entities.Klanten.Remove((Klanten)_klantenViewSource.View.CurrentItem);
            _unsavedChanges = true;
            //_entities.SaveChanges();
            //Refresh();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonAdd.Content.ToString() == "Toevoegen")
            {
                _addClient = new Klanten();

                int tempId = 1;

                while (_entities.Klanten.Any(k => k.ID == tempId))
                {
                    tempId++;
                }

                _addClient.ID = tempId;
                _addClient.Datum = DateTime.Now;
                GridInformation.DataContext = _addClient;
                TextBlockID.Visibility = Visibility.Hidden;
                TextBoxID.Visibility = Visibility.Visible;
                ButtonAdd.Content = "Annuleren";
            }
            else
            {
                GridInformation.DataContext = _klantenViewSource;
                ButtonAdd.Content = "Toevoegen";
                TextBlockID.Visibility = Visibility.Visible;
                TextBoxID.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            TextBlockID.Visibility = Visibility.Visible;
            TextBoxID.Visibility = Visibility.Hidden;
            Save();
            Refresh();
        }

        private void ComboBoxSoortKlant_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Winkel Rijkevorsel");
            data.Add("Winkel Heist");
            data.Add("Ophaling Brecht");
            data.Add("Bedrijven Ecover");
            ComboBoxSoortKlant.ItemsSource = data;
        }

        private void ComboBoxBetalingswijze_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Papier");
            data.Add("Elektronisch");
            ComboBoxBetalingswijze.ItemsSource = data;
        }

        private void ComboBoxBericht_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Neen");
            data.Add("SMS");
            data.Add("E-mail");
            ComboBoxBericht.ItemsSource = data;
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            int tempID;
            var tempSearchList = new List<Klanten>();

            //If text is getting longer search in items from the combobox self
            //If not search in the list with all the items (in this situation you're deleting chars)
            //Result: If you're adding chars, it will go faster because you need to search in a smaller list!
            if (_oldLengthSearchBox < TextBoxSearch.Text.Length)
            {
                tempSearchList = TextBoxSearch.Items.OfType<Klanten>().ToList();
            }
            else
            {
                tempSearchList = _entities.Klanten.Local.ToList();
            }

            if (int.TryParse(TextBoxSearch.Text, out tempID))
            {
                List<Klanten> tempList = tempSearchList.Where(k => k.ID.ToString().Contains(TextBoxSearch.Text)).ToList();
                TextBoxSearch.ItemsSource = tempList;
            }
            else
            {
                //If the text contains off digits don't search after it in Naam and Voornaam (after all you will not find anything!)
                if (!TextBoxSearch.Text.Any(char.IsDigit))
                {
                    List<Klanten> tempList = tempSearchList.Where(k => k.Naam.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower()) || k.Voornaam != null && k.Voornaam.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
                    TextBoxSearch.ItemsSource = tempList;
                }
            }

            _oldLengthSearchBox = TextBoxSearch.Text.Length;        //Save current textlength, in the future we can compare it to determine if the textlength became bigger or smaller!
        }

        private void TextBoxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextBoxSearch.SelectedItem is Klanten)
            {
                Klanten temp = (Klanten)TextBoxSearch.SelectedItem;
                _klantenViewSource.View.MoveCurrentTo(temp);
            }

        }
    }

    public class SearchComboBox: ComboBox
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
