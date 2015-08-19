using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace ISIS
{
    /// <summary>
    /// Interaction logic for BerekenModule.xaml
    /// </summary>
    public partial class BerekenModule : UserControl
    {
        ISIS_DataEntities _entities;
        Klant _tempKlant;
        Prestatie _tempPrestatie;
        int _oldLengthSearchBox = 0;

        public BerekenModule()
        {
            InitializeComponent();
            _entities = new ISIS_DataEntities();
            _entities.Klanten.Load();
            _entities.Prestaties.Load();
            TextBoxSearch.ItemsSource = _entities.Klanten.Local;
            _tempPrestatie = new Prestatie();
            MainGrid.DataContext = _tempPrestatie;
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
                _tempKlant = (Klant)TextBoxSearch.SelectedItem;
        }
        #endregion

        #region Automatic Selection TextBox
        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }
        #endregion

        private void ButtonBereken_Click(object sender, RoutedEventArgs e)
        {
            //At the moment the parameters are hardcoded
            //TODO: Make them changable in a seperate window
            _tempPrestatie.ParameterHemden = 9;
            _tempPrestatie.ParameterLakens1 = 10;
            _tempPrestatie.ParameterLakens2 = 15;
            _tempPrestatie.ParameterAndereStrijk = 1;

            _tempPrestatie.TotaalStrijk = (_tempPrestatie.AantalHemden + _tempPrestatie.AantalLakens1 + _tempPrestatie.AantalLakens2 + _tempPrestatie.AantalAndereStrijk);

            _tempPrestatie.TotaalHemden = _tempPrestatie.AantalHemden * _tempPrestatie.ParameterHemden;
            _tempPrestatie.TotaalLakens1 = _tempPrestatie.AantalLakens1 * _tempPrestatie.ParameterLakens1;
            _tempPrestatie.TotaalLakens2 = _tempPrestatie.AantalLakens2 * _tempPrestatie.ParameterLakens2;
            _tempPrestatie.TotaalAndereStrijk = _tempPrestatie.TijdAndereStrijk * _tempPrestatie.ParameterAndereStrijk;

            if (_tempPrestatie.TotaalStrijk < 20)
                _tempPrestatie.TotaalAdministratie = 5;
            else if (_tempPrestatie.TotaalStrijk >= 20)
                _tempPrestatie.TotaalAdministratie = 10;
            else if (_tempPrestatie.TotaalStrijk >= 40)
                _tempPrestatie.TotaalAdministratie = 15;
            else if (_tempPrestatie.TotaalStrijk >= 80)
                _tempPrestatie.TotaalAdministratie = 20;

            _tempPrestatie.TotaalMinuten = _tempPrestatie.TotaalHemden + _tempPrestatie.TotaalLakens1 + _tempPrestatie.TotaalLakens2 + _tempPrestatie.TotaalAndereStrijk + _tempPrestatie.TotaalAdministratie;

            TextBlockTegoed.DataContext = _tempKlant;

            _tempPrestatie.TotaalBetalen = _tempPrestatie.TotaalMinuten - _tempKlant.Tegoed;
            _tempPrestatie.TotaalDienstenChecks = Convert.ToInt32(Math.Ceiling(_tempPrestatie.TotaalBetalen / 60.0));
            if (_tempPrestatie.TotaalDienstenChecks == 0)
                _tempPrestatie.NieuwTegoed = _tempKlant.Tegoed - _tempPrestatie.TotaalMinuten;
            else
                _tempPrestatie.NieuwTegoed = (_tempPrestatie.TotaalDienstenChecks * 60) - _tempPrestatie.TotaalBetalen;
        }

        private void ButtonToevoegen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DatePickerDatum_Loaded(object sender, RoutedEventArgs e)
        {
            DatePickerDatum.SelectedDate = DateTime.Now;
        }
    }
}
