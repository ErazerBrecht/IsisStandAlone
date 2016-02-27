using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DAL_Repository;
using EF_Context;
using GalaSoft.MvvmLight;

namespace PL_WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private WorkspaceViewModel _selectedWorkspace;
        public WorkspaceViewModel SelectedWorkspace
        {
            get
            {
                return _selectedWorkspace;
            }
            set
            {
                //Step one: Check if there needs to be saved something
                if (_selectedWorkspace == null || _selectedWorkspace.Leave())
                {

                    //Step two: Change the current view into the new selected view
                    _selectedWorkspace = value;

                    //Step three: Init functionality of new view
                    //Actions that has to happen when view is (re)opened.
                    _selectedWorkspace.LoadData();
                }
            }
        }

        private UnitOfWork _ctx;

        public ObservableCollection<WorkspaceViewModel> Workspaces { get; }

        public MainViewModel()
        {
            Application.Current.MainWindow.Closing += MainWindow_Closing;
            //var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ISIS Rijkevorsel");

            //try
            //{
                //AppDomain.CurrentDomain.SetData("DataDirectory", path);
                //TestConnection();

                _ctx = new UnitOfWork(new IsisContext());

                Workspaces = new ObservableCollection<WorkspaceViewModel>();
                Workspaces.Add(new BerekenModuleViewModel(_ctx));
                //Workspaces.Add(new PrestatieBeheerViewModel());
                Workspaces.Add(new KlantenBeheerViewModel(_ctx));
                Workspaces.Add(new PersoneelBeheerViewModel(_ctx));
                Workspaces.Add(new ParameterBeheerViewModel(_ctx));
                SelectedWorkspace = Workspaces.First();
            //}
            //catch
            //{
                //var errorWindow = new ErrorDataBaseWindowService();
                //errorWindow.Show(new ErrorWindowViewModel());
                //Application.Current.MainWindow.Close();
            //}
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SelectedWorkspace != null)
            {
                e.Cancel = !(SelectedWorkspace.Leave());

                //The program is actual going to close when e.Cancel is false
                if(!e.Cancel)
                    _ctx.Dispose();
            }
            else
                _ctx.Dispose();

        }

        private void TestConnection()
        {
            //using (var db = new ISIS_DataEntities())
            //{
            //    DbConnection conn = db.Database.Connection;
            //    conn.Open();   // check the database connection
            //}
        }

    }
}
