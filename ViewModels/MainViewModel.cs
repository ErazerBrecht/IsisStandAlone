using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ISIS.Services;

namespace ISIS.ViewModels
{
    class MainViewModel
    {
        private WorkspaceViewModel _selectedWorkspace;
        public WorkspaceViewModel SelectedWorkspace {
            get
            {
                return _selectedWorkspace;
            }
            set
            {
                _selectedWorkspace = value;

                //Actions that has to happen when Tab is (re)opened.
                if (value is PrestatieBeheerViewModel)
                    (value as PrestatieBeheerViewModel).LoadData();
                //else if (value is BerekenModuleViewModel)
                    //(value as BerekenModuleViewModel).LoadData();
            }
        }
        public ObservableCollection<WorkspaceViewModel> Workspaces { get; }

        public MainViewModel()
        {
            Application.Current.MainWindow.Closing += MainWindow_Closing;
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ISIS Rijkevorsel");

            try
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", path);
                TestConnection();
                Workspaces = new ObservableCollection<WorkspaceViewModel>();
                Workspaces.Add(new BerekenModuleViewModel());
                Workspaces.Add(new PrestatieBeheerViewModel());
                Workspaces.Add(new KlantenBeheerViewModel());
                Workspaces.Add(new PersoneelBeheerViewModel());
                Workspaces.Add(new ParameterBeheerViewModel());
                SelectedWorkspace = Workspaces.First();
            }
            catch
            {
                var errorWindow = new ErrorDataBaseWindowService();
                errorWindow.Show(new ErrorWindowViewModel());
                Application.Current.MainWindow.Close();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Workspaces != null)         //This happens when the database failed to load! The ErrorWindow will pop up!
            {
                foreach (WorkspaceViewModel wvm in Workspaces)
                {
                    if (wvm is BeheerViewModel)
                    {
                        if (((BeheerViewModel)wvm).Close())
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
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
