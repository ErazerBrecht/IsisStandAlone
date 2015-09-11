using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.Entity;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ISIS.Views
{
    /// <summary>
    /// Interaction logic for BerekenModule.xaml
    /// </summary>
    public partial class ParameterBeheer : UserControl
    {
        //ISIS_DataEntities _entities;

        public ParameterBeheer()
        {
            InitializeComponent();          
        }

        private void ParameterBeheer_Loaded(object sender, RoutedEventArgs e)
        {

            //_entities = new ISIS_DataEntities();
            //_entities.Winkels.Load();
            ////_entities.Bedrijven.Load();
            ////_entities.Scholen.Load();
            ////_entities.Ophalingen.Load();

            //ListBoxWinkel.ItemsSource = _entities.Winkels.Local;
            //GridAddWinkel.DataContext = new Winkel();
            ////ListBoxBedrijven.ItemsSource = _entities.Bedrijven.Local;
            ////ListBoxOphaling.ItemsSource = _entities.Ophalingen.Local;
            ////ListBoxScholen.ItemsSource = _entities.Scholen.Local;
        }

        private void ButtonWinkelDelete_Click(object sender, RoutedEventArgs e)
        {
            //Winkel remove = (sender as Button).DataContext as Winkel;
            //_entities.Winkels.Remove(remove);
            //_entities.SaveChanges();
        }

        private void ButtonWinkelAdd_Click(object sender, RoutedEventArgs e)
        {
            //Winkel add = (sender as Button).DataContext as Winkel;
            //if (_entities.Winkels.Local.Where(t => t.Naam == add.Naam).Count() > 0)
            //    MessageBox.Show("Deze winkel bestaat al!");
            //else
            //{ 
            //    _entities.Winkels.Add(add);
            //    _entities.SaveChanges();
            //}

            //GridAddWinkel.DataContext = new Winkel();       //Reset
        }


        private void TextBoxWinkelsAdd_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    if (String.IsNullOrWhiteSpace(TextBoxWinkelsAdd.Text))
            //        return;

            //    Winkel temp = new Winkel() { Naam = TextBoxWinkelsAdd.Text};
            //    _entities.Winkels.Add(temp);
            //    _entities.SaveChanges();
            //}
        }

        private void ButtonBedrijfDelete_Click(object sender, RoutedEventArgs e)
        {
            //string remove = (sender as Button).DataContext.ToString();
            //SoortKlant.Bedrijven.Remove(remove);
            //SoortKlant.Save();
        }

        private void TextBoxBedrijvenAdd_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    if (String.IsNullOrWhiteSpace(TextBoxBedrijvenAdd.Text))
            //        return;

            //    SoortKlant.Bedrijven.Add(TextBoxBedrijvenAdd.Text);
            //    SoortKlant.Save();
            //}
        }

        private void ButtonOphalingDelete_Click(object sender, RoutedEventArgs e)
        {
            //string remove = (sender as Button).DataContext.ToString();
            //SoortKlant.Ophaling.Remove(remove);
            //SoortKlant.Save();
        }

        private void TextBoxOphalingAdd_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    if (String.IsNullOrWhiteSpace(TextBoxOphalingAdd.Text))
            //        return;

            //    SoortKlant.Ophaling.Add(TextBoxOphalingAdd.Text);
            //    SoortKlant.Save();
            //}
        }

        private void ButtonSchoolDelete_Click(object sender, RoutedEventArgs e)
        {
            //string remove = (sender as Button).DataContext.ToString();
            //SoortKlant.Scholen.Remove(remove);
            //SoortKlant.Save();
        }

        private void TextBoxScholenAdd_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    if (String.IsNullOrWhiteSpace(TextBoxScholenAdd.Text))
            //        return;

            //    SoortKlant.Scholen.Add(TextBoxScholenAdd.Text);
            //    SoortKlant.Save();
            //}
        }
    }
}
