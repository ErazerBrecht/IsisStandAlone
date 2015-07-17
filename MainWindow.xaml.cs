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
    public partial class MainWindow : Window
    {
        ISIS_KlantenEntities _entities;
        CollectionViewSource _klantenViewSource;
        Klanten _addClient;
        bool _unsavedChanges;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _klantenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("klantenViewSource")));
            Refresh();
        }

        private void Refresh()
        {
            _unsavedChanges = false;

            if (_entities != null)
                _entities.Dispose();

            _entities = new ISIS_KlantenEntities();
            //_entities.Schoolresultaten.Local.CollectionChanged += Local_CollectionChanged;
            _entities.Klanten.Load();
            _klantenViewSource.Source = _entities.Klanten.Local;
            ButtonAdd.IsEnabled = true;
            //StackPanelInfo.DataContext = _schoolresultatenViewSource;
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
    }
}
