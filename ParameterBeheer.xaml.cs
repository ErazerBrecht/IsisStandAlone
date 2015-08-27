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
        SoortKlant _soortKlant;

        public ParameterBeheer()
        {
            InitializeComponent();
            LoadParameters();
            _soortKlant = new SoortKlant();
            ListBoxWinkel.ItemsSource = _soortKlant.Winkels;
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

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            string remove = (sender as Button).DataContext.ToString();
            _soortKlant.Winkels.Remove(remove);
            _soortKlant.Save();
        }
    }
}
