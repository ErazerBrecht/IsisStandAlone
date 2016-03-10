using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DAL_Repository;
using EF_Model;
using GalaSoft.MvvmLight.Command;
using PL_WPF.Services;

namespace PL_WPF.ViewModels
{
    class ParameterBeheerViewModel : WorkspaceViewModel
    {
        public Parameters ParameterData { get; set; }
        public Strings StringsData { get; set; }
        public BegroetingPrint BegroetingPrintData { get; set; }
        public WinkelTypeViewModel WinkelData { get; private set; }
        public SchoolTypeViewModel SchoolData { get; private set; }
        public BedrijfTypeViewModel BedrijfData { get; private set; }
        public OphalingBeheerViewModel OphalingData { get; private set; }

        #region Commands
        public ICommand SaveParametersCommand { get; set; }
        public ICommand RefreshParametersCommand { get; set; }
        public ICommand SaveStringsCommand { get; set; }
        public ICommand RefreshStringsCommand { get; set; }
        public ICommand SaveBegroetingCommand { get; set; }
        public ICommand RefreshBegroetingCommand { get; set; }
        public ICommand BackupDatabaseCommand { get; set; }
        public ICommand RestoreDatabaseCommand { get; set; }
        #endregion

        public ParameterBeheerViewModel(UnitOfWork ctx) : base(ctx)
        {
            Header = "ParameterBeheer";
            ParameterData = new Parameters();
            StringsData = new Strings();
            BegroetingPrintData = new BegroetingPrint();

            WinkelData = new WinkelTypeViewModel(Ctx);
            SchoolData = new SchoolTypeViewModel(Ctx);
            BedrijfData = new BedrijfTypeViewModel(Ctx);
            OphalingData = new OphalingBeheerViewModel(Ctx);

            #region Buttons
            SaveParametersCommand = new RelayCommand(
                () => SaveParameters(),
                () => true
            );

            RefreshParametersCommand = new RelayCommand(
                () => LoadParameters(),
                () => true
            );

            SaveStringsCommand = new RelayCommand(
                () => SaveStrings(),
                () => true                   
            );

            RefreshStringsCommand = new RelayCommand(
                () => LoadStrings(),
                () => true
            );

            SaveBegroetingCommand = new RelayCommand(SaveBegroetingPrint);
            RefreshBegroetingCommand = new RelayCommand(LoadBegroetingPrint);
            BackupDatabaseCommand = new RelayCommand(BackupDatabase);
            RestoreDatabaseCommand = new RelayCommand(RestoreDatabase);
            #endregion
        }

        public void LoadParameters()
        {
            ParameterData.LoadParameters();
        }

        public void SaveParameters()
        {
            ParameterData.SaveParameters();
        }

        public void LoadStrings()
        {
            StringsData.LoadStrings();
        }

        public void SaveStrings()
        {
            StringsData.SaveStrings();
        }

        public void LoadBegroetingPrint()
        {
            BegroetingPrintData.LoadStrings();
        }

        public void SaveBegroetingPrint()
        {
            BegroetingPrintData.SaveStrings();
        }

        public void RestoreDatabase()
        {
            OpenFileDialogService openFileDialogService = new OpenFileDialogService();
            string path = openFileDialogService.Open();
            if (!String.IsNullOrWhiteSpace(path))
            {
                try
                {
                    Ctx.RestoreDatabase(path);
                    var message = new MessageBoxService();
                    var result = message.AskForConfirmation("Restore van backup van databank is geslaagd. Om effect te hebben moet u de applicatie herstarten! \nWilt u de applicatie nu herstarten?","Strijkdienst Conny Restore Database", System.Windows.MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        System.Windows.Forms.Application.Restart();
                        Process.GetCurrentProcess().Kill();
                    }

                }
                catch
                {
                    //TODO
                }
            }
        }

        public void BackupDatabase()
        {
            OpenFolderDialogService openFolderDialogService = new OpenFolderDialogService();
            string path = openFolderDialogService.Open();
            if (!String.IsNullOrWhiteSpace(path))
            {
                try
                {
                    Ctx.BackupDatabase(path);
                }
                catch
                {
                    //TODO
                }
            }
        }
    }
}
