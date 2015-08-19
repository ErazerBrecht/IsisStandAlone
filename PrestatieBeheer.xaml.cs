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
            _prestatieViewSource.Source = _entities.Prestaties.Local;
        }
    }
}
