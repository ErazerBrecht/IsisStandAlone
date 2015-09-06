using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIS.ViewModels
{
    class MainViewModel
    {

        public ObservableCollection<WorkspaceViewModel> Workspaces { get; }

        public MainViewModel()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ISIS Rijkevorsel");

            //try
            //{
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            TestConnection();
            //}
            //catch
            //{
            //    var errorWindow = new ErrorDatabase();
            //    errorWindow.Show();
            //    this.Close();
            //}

            Workspaces = new ObservableCollection<WorkspaceViewModel>();
            Workspaces.Add(new KlantenBeheerViewModel());
            Workspaces.Add(new PersoneelBeheerViewModel());
        }


    private void TestConnection()
    {
        using (var db = new ISIS_DataEntities())
        {
            DbConnection conn = db.Database.Connection;
            conn.Open();   // check the database connection
        }
    }

}
}
