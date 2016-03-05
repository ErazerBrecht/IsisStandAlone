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
using System.Windows.Shapes;
using EF_Model;

namespace PL_WPF.Views
{
    /// <summary>
    /// Interaction logic for TijdPrestatiePrint.xaml
    /// </summary>
    public partial class TijdPrestatiePrint : Window
    {
        public TijdPrestatiePrint(object datacontext)
        {
            InitializeComponent();

            var prestatie = datacontext as Prestatie;
            Strings currentStrings = new Strings();

            LabelHemden.Content = currentStrings.StringHemden;
            LabelLakens1.Content = currentStrings.StringLakens1;
            LabelLakens2.Content = currentStrings.StringLakens2;
            LabelAndereStrijk.Content = currentStrings.StringAndere;
            LabelAdministratie.Content = currentStrings.StringAdministratie;

            if (prestatie.Klant.Betalingswijze == "Elektronisch")
            {
                LabelGebruikersnummer.Visibility = Visibility.Visible;
                LabelDienstenChecks.Text = "aantal dienstencheques te betalen (elektronisch)";
            }

            this.DataContext = datacontext;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(PrintZone, "My First Print Job");
                this.Close();
            }
        }
    }
}
