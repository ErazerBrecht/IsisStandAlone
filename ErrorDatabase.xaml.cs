using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
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
using Microsoft.Win32;
using System.Diagnostics;

namespace ISIS
{
    /// <summary>
    /// Interaction logic for ErrorDatabase.xaml
    /// </summary>
    public partial class ErrorDatabase : Window
    {
        public ErrorDatabase()
        {            
            InitializeComponent();
            ErrorImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Error.Handle, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            TextBlockPath.Text = "Standaard locatie: " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ISIS Rijkevorsel", "ISIS_Data.mdf"); 
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Database files | *.mdf";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.Copy(openFileDialog.FileName, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ISIS Rijkevorsel", openFileDialog.SafeFileName));     //TODO Add Exception handling
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kon het gekozen bestand niet openenen!\nKijk na of u de juiste rechten hebt\nHet bastand kan ook al in gebruik zijn!\n\nExtra informatie:\n" + ex.ToString());
                }
            }
        }

        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
