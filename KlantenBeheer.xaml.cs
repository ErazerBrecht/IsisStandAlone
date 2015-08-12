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
            ButtonAdd.Content = "Add";
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
            _entities.Klanten.Remove((Klanten)_klantenViewSource.View.CurrentItem);
            _unsavedChanges = true;
            //_entities.SaveChanges();
            //Refresh();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonAdd.Content.ToString() == "Add")
            {
                _addClient = new Klanten();

                int tempId = 1;

                while(_entities.Klanten.Any(k => k.ID == tempId))
                {
                    tempId++;
                }

                _addClient.ID = tempId;
                _addClient.Datum = DateTime.Now;
                GridInformation.DataContext = _addClient;
                TextBoxID.IsReadOnly = false;
                ButtonAdd.Content = "Cancel";
            }
            else
            {
                GridInformation.DataContext = _klantenViewSource;
                ButtonAdd.Content = "Add";
                TextBoxID.IsReadOnly = true;
                RemoveBorderBrushID();
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            TextBoxID.IsReadOnly = true;
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
            soortKlantColumn.ItemsSource = data;
        }

        private void ComboBoxBetalingswijze_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Papier");
            data.Add("Elektronisch");
            ComboBoxBetalingswijze.ItemsSource = data;
            betalingswijzeColumn.ItemsSource = data;
        }

        private void ComboBoxBericht_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Neen");
            data.Add("SMS");
            data.Add("E-mail");
            ComboBoxBericht.ItemsSource = data;
            berichtColumn.ItemsSource = data;
        }

        private void TextBoxID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxID.IsReadOnly == false)
            {
                ToolTip tooltip = new ToolTip { Content = "De gekozen ID bestaat al!" };
                //Check if ID already exists, if this is the case give error and add red border!
                int tempId = Convert.ToInt32(TextBoxID.Text);
                if ((_entities.Klanten.Any(k => k.ID == tempId)))
                {
                    TextBoxID.BorderBrush = new SolidColorBrush(Colors.Red);
                    TextBoxID.BorderThickness = new Thickness(2);
                    tooltip.PlacementTarget = TextBoxID;
                    tooltip.Placement = PlacementMode.Right;
                    tooltip.VerticalOffset = 4;
                    tooltip.HorizontalOffset = -155;
                    TextBoxID.ToolTip = tooltip;
                    tooltip.IsOpen = true;               
                    ButtonSave.IsEnabled = false;
                }
                else
                {
                    RemoveBorderBrushID();
                }
            }

        }

        private void RemoveBorderBrushID()
        {
            if (TextBoxID.ToolTip != null)
            {
                ToolTip t = (ToolTip)TextBoxID.ToolTip;
                t.IsOpen = false;
            }
            TextBoxID.ClearValue(TextBox.BorderBrushProperty);
            TextBoxID.ClearValue(TextBox.BorderThicknessProperty);
            ButtonSave.IsEnabled = true;
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            int tempID;
            if (int.TryParse(TextBoxSearch.Text, out tempID))
            {
               List<Klanten> tempList = _entities.Klanten.Local.Where(k => k.ID.ToString().Contains(TextBoxSearch.Text)).ToList();
                TextBoxSearch.ItemsSource = tempList;
                TextBoxSearch.IsDropDownOpen = true;
            }
            else
            {
                List<Klanten> tempList = _entities.Klanten.Local.Where(k => k.Naam.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower()) || k.Voornaam != null && k.Voornaam.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
                TextBoxSearch.ItemsSource = tempList;
                TextBoxSearch.IsDropDownOpen = true;
            }           
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
}
