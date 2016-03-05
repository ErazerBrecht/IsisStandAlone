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
using PL_WPF.Services;

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
            Application.Current.MainWindow.Closed += MainWindow_Closed;

            try
            {
                _ctx = new UnitOfWork(new IsisContext());
                Workspaces = new ObservableCollection<WorkspaceViewModel>
                {
                    new BerekenModuleViewModel(_ctx),
                    new PrestatieBeheerViewModel(_ctx),
                    new KlantenBeheerViewModel(_ctx),
                    new PersoneelBeheerViewModel(_ctx),
                    new ParameterBeheerViewModel(_ctx)
                };
                SelectedWorkspace = Workspaces.First();
            }
            catch (Exception ex)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("ERROR");      //Hack: The first Messagebox closes automatically
                messageService.ShowErrorBox("Er heeft zich een probleem voorgedaan bij het ophalen van de data \n\nError: " + ex);
                Application.Current.Shutdown(-1);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SelectedWorkspace != null)
                e.Cancel = !(SelectedWorkspace.Leave());
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if(_ctx != null)
                _ctx.Dispose();
        }
    }
}
