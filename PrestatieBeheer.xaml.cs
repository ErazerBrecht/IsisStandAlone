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
    public partial class PrestatieBeheer : System.Windows.Controls.UserControl
    {
        ISIS_DataEntities _entities;
        CollectionViewSource _prestatieViewSource;
        int _oldLengthSearchBox = 0;

        public PrestatieBeheer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _prestatieViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["prestatiesViewSource"];
            Refresh();
        }      

        private void Refresh()
        {
            _entities = new ISIS_DataEntities();
            _entities.Prestaties.Load();
            _entities.Klanten.Load();
            _prestatieViewSource.Source = _entities.Prestaties.Local;
            TextBoxSearch.ItemsSource = _entities.Klanten.Local;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (rb.Content != null)
            {
                if (rb.Content.ToString() == "Iedereen")
                {
                    _prestatieViewSource.Source = _entities.Prestaties.Local;
                    TextBoxSearch.IsEnabled = false;
                }
                else
                    TextBoxSearch.IsEnabled = true;
            }
        }

        #region SearchBox
        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            int tempID;
            var tempSearchList = new List<Klant>();

            //If text is getting longer search in items from the combobox self
            //If not search in the list with all the items (in this situation you're deleting chars)
            //Result: If you're adding chars, it will go faster because you need to search in a smaller list!
            if (_oldLengthSearchBox < TextBoxSearch.Text.Length)
            {
                tempSearchList = TextBoxSearch.Items.OfType<Klant>().ToList();
            }
            else
            {
                tempSearchList = _entities.Klanten.Local.ToList();
            }

            if (int.TryParse(TextBoxSearch.Text, out tempID))
            {
                List<Klant> tempList = tempSearchList.Where(k => k.ID.ToString().Contains(TextBoxSearch.Text)).ToList();
                TextBoxSearch.ItemsSource = tempList;
            }
            else
            {
                //If the text contains off digits don't search after it in Naam and Voornaam (after all you will not find anything!)
                if (!TextBoxSearch.Text.Any(char.IsDigit))
                {
                    List<Klant> tempList = tempSearchList.Where(k => k.Naam.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower()) || k.Voornaam != null && k.Voornaam.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
                    TextBoxSearch.ItemsSource = tempList;
                }
            }

            _oldLengthSearchBox = TextBoxSearch.Text.Length;        //Save current textlength, in the future we can compare it to determine if the textlength became bigger or smaller!
        }

        private void TextBoxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextBoxSearch.SelectedItem is Klant)
            {
                Klant tempKlant = (Klant)TextBoxSearch.SelectedItem;
                List<Prestatie> tempList;
                tempList = _entities.Prestaties.Where(p => p.KlantenNummer == tempKlant.ID).ToList();
                _prestatieViewSource.Source = tempList;
            }
        }
        #endregion
    }
}
