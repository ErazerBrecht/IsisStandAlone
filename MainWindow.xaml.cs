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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ISIS.ISIS_KlantenDataSet iSIS_KlantenDataSet = ((ISIS.ISIS_KlantenDataSet)(this.FindResource("iSIS_KlantenDataSet")));
            // Load data into the table Klanten. You can modify this code as needed.
            ISIS.ISIS_KlantenDataSetTableAdapters.KlantenTableAdapter iSIS_KlantenDataSetKlantenTableAdapter = new ISIS.ISIS_KlantenDataSetTableAdapters.KlantenTableAdapter();
            iSIS_KlantenDataSetKlantenTableAdapter.Fill(iSIS_KlantenDataSet.Klanten);
            System.Windows.Data.CollectionViewSource klantenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("klantenViewSource")));
            klantenViewSource.View.MoveCurrentToFirst();
        }
    }
}
