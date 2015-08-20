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
        Parameters _parameters;
        int _oldLengthSearchBox = 0;
        bool _ableToSave;

        public BerekenModule()
        {
            InitializeComponent();
            _entities = new ISIS_DataEntities();
            _entities.Klanten.Load();
            _entities.Prestaties.Load();
            TextBoxSearch.ItemsSource = _entities.Klanten.Local;
            _tempPrestatie = new Prestatie();
            MainGrid.DataContext = _tempPrestatie;
            _parameters = new Parameters();
            var elements = BerekenGrid.Children.Cast<FrameworkElement>().Where(c => Grid.GetColumn(c) == 2).ToList();       //Only elements in column 2 need _parameters as datacontext
            foreach (var element in elements)
            {
                element.DataContext = _parameters;
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
                _tempKlant = (Klant)TextBoxSearch.SelectedItem;
        }
        #endregion

        private void ButtonBereken_Click(object sender, RoutedEventArgs e)
        {
            if (_tempKlant == null)
            {
                MessageBox.Show("Je hebt nog geen klant gekozen!");
                return;
            }

            _tempPrestatie.ParameterHemden = _parameters.ParameterHemden;
            _tempPrestatie.ParameterLakens1 = _parameters.ParameterLakens1;
            _tempPrestatie.ParameterLakens2 = _parameters.ParameterLakens2;
            _tempPrestatie.ParameterAndereStrijk = _parameters.ParameterAndereStrijk;

            _tempPrestatie.TotaalStrijk = (_tempPrestatie.AantalHemden + _tempPrestatie.AantalLakens1 + _tempPrestatie.AantalLakens2 + _tempPrestatie.AantalAndereStrijk);

            _tempPrestatie.TotaalHemden = _tempPrestatie.AantalHemden * _tempPrestatie.ParameterHemden;
            _tempPrestatie.TotaalLakens1 = _tempPrestatie.AantalLakens1 * _tempPrestatie.ParameterLakens1;
            _tempPrestatie.TotaalLakens2 = _tempPrestatie.AantalLakens2 * _tempPrestatie.ParameterLakens2;
            _tempPrestatie.TotaalAndereStrijk = _tempPrestatie.TijdAndereStrijk * _tempPrestatie.ParameterAndereStrijk;

            if (_tempPrestatie.TotaalStrijk < 20)
                _tempPrestatie.TijdAdministratie = 5;
            else if (_tempPrestatie.TotaalStrijk >= 20)
                _tempPrestatie.TijdAdministratie = 10;
            else if (_tempPrestatie.TotaalStrijk >= 40)
                _tempPrestatie.TijdAdministratie = 15;
            else if (_tempPrestatie.TotaalStrijk >= 80)
                _tempPrestatie.TijdAdministratie = 20;

            _tempPrestatie.TotaalMinuten = _tempPrestatie.TotaalHemden + _tempPrestatie.TotaalLakens1 + _tempPrestatie.TotaalLakens2 + _tempPrestatie.TotaalAndereStrijk + _tempPrestatie.TijdAdministratie;

            TextBlockTegoed.DataContext = _tempKlant;

            _tempPrestatie.TotaalBetalen = _tempPrestatie.TotaalMinuten - _tempKlant.Tegoed;
            _tempPrestatie.TotaalDienstenChecks = Convert.ToInt32(Math.Ceiling(_tempPrestatie.TotaalBetalen / 60.0));
            if (_tempPrestatie.TotaalDienstenChecks == 0)
                _tempPrestatie.NieuwTegoed = _tempKlant.Tegoed - _tempPrestatie.TotaalMinuten;
            else
                _tempPrestatie.NieuwTegoed = (_tempPrestatie.TotaalDienstenChecks * 60) - _tempPrestatie.TotaalBetalen;

            _ableToSave = true;
        }

        private void ButtonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (!_ableToSave)
            {
                MessageBox.Show("U hebt nog niets berekend!");
                return;
            }

            //The second time you want to add a "prestatie" EF is following the first objct
            //If you change the object EF will track the edits
            //This will cause errors because the ID wil change (is normally not possible)
            //But in our case it's a new "prestatie" so the ID should change
            //To solve this we have to detach the object if EF is tracking it
            Prestatie attachedPrestatie = _entities.Prestaties.Find(_tempPrestatie.Id);
            if (attachedPrestatie != null)
                _entities.Entry(attachedPrestatie).State = EntityState.Detached;


            int tempId = 1;

            //Search for first valid ID
            while (_entities.Prestaties.Any(p => p.Id == tempId))
            {
                tempId++;
            }

            _tempPrestatie.Id = tempId;
            _tempPrestatie.Datum = DatePickerDatum.SelectedDate.GetValueOrDefault();
            _tempPrestatie.KlantenNummer = _tempKlant.ID;

            _tempKlant.Tegoed = Convert.ToByte(_tempPrestatie.NieuwTegoed);
            _tempKlant.LaatsteActiviteit = _tempPrestatie.Datum;


            //We query local context first to see if it's there.
            var attachedEntity = _entities.Klanten.Find(_tempKlant.ID);

            //We have it in the entity, need to update.
            if (attachedEntity != null)
            {
                var attachedEntry = _entities.Entry(attachedEntity);
                attachedEntry.CurrentValues.SetValues(_tempKlant);
            }

            _entities.Prestaties.Add(_tempPrestatie);
            _entities.SaveChanges();

        }

        private void DatePickerDatum_Loaded(object sender, RoutedEventArgs e)
        {
            DatePickerDatum.SelectedDate = DateTime.Now;
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            if (_tempKlant == null)
            {
                MessageBox.Show("Je hebt nog geen klant gekozen!");
                return;
            }


        }
    }
}
