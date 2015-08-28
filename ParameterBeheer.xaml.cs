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

namespace ISIS
{
    /// <summary>
    /// Interaction logic for BerekenModule.xaml
    /// </summary>
    public partial class ParameterBeheer : UserControl
    {
        Parameters _parameters;

        public ParameterBeheer()
        {
            InitializeComponent();
            LoadParameters();
            ListBoxWinkel.ItemsSource = SoortKlant.Winkels;
            ListBoxBedrijven.ItemsSource = SoortKlant.Bedrijven;
            ListBoxOphaling.ItemsSource = SoortKlant.Ophaling;
            ListBoxScholen.ItemsSource = SoortKlant.Scholen;
        }
        
        private void LoadParameters()
        {
            _parameters = new Parameters();
            ParameterGrid.DataContext = _parameters;
        }

        private void ButtonOpslaan_Click(object sender, RoutedEventArgs e)
        {
            _parameters.SaveParameters();
        }

        private void ButtonAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            LoadParameters();
        }

        private void ButtonWinkelDelete_Click(object sender, RoutedEventArgs e)
        {
            string remove = (sender as Button).DataContext.ToString();
            SoortKlant.Winkels.Remove(remove);
            SoortKlant.Save();
        }

        private void TextBoxWinkelsAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrWhiteSpace(TextBoxWinkelsAdd.Text))
                    return;

                SoortKlant.Winkels.Add(TextBoxWinkelsAdd.Text);
                SoortKlant.Save();
            }
        }

        private void ButtonBedrijfDelete_Click(object sender, RoutedEventArgs e)
        {
            string remove = (sender as Button).DataContext.ToString();
            SoortKlant.Bedrijven.Remove(remove);
            SoortKlant.Save();
        }

        private void TextBoxBedrijvenAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrWhiteSpace(TextBoxBedrijvenAdd.Text))
                    return;

                SoortKlant.Bedrijven.Add(TextBoxBedrijvenAdd.Text);
                SoortKlant.Save();
            }
        }

        private void ButtonOphalingDelete_Click(object sender, RoutedEventArgs e)
        {
            string remove = (sender as Button).DataContext.ToString();
            SoortKlant.Ophaling.Remove(remove);
            SoortKlant.Save();
        }

        private void TextBoxOphalingAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrWhiteSpace(TextBoxOphalingAdd.Text))
                    return;

                SoortKlant.Ophaling.Add(TextBoxOphalingAdd.Text);
                SoortKlant.Save();
            }
        }

        private void ButtonSchoolDelete_Click(object sender, RoutedEventArgs e)
        {
            string remove = (sender as Button).DataContext.ToString();
            SoortKlant.Scholen.Remove(remove);
            SoortKlant.Save();
        }

        private void TextBoxScholenAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrWhiteSpace(TextBoxScholenAdd.Text))
                    return;

                SoortKlant.Scholen.Add(TextBoxScholenAdd.Text);
                SoortKlant.Save();
            }
        }
    }
}
