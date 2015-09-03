using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
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
namespace ISIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            //SoortKlant.Load();
            InitializeComponent();

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ISIS Rijkevorsel");

            try
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", path);
                TestConnection();
            }
            catch
            {
                var errorWindow = new ErrorDatabase();
                errorWindow.Show();
                this.Close();
            }
        }


        public void TestConnection()
        {
            using (var db = new ISIS_DataEntities())
            {
                DbConnection conn = db.Database.Connection;
                conn.Open();   // check the database connection
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.KlantenBeheerUserControl.CheckChanges())
                e.Cancel = true;
            else if (this.PersoneelBeheerUserControl.CheckChanges())
                e.Cancel = true;
        }
    }
}
