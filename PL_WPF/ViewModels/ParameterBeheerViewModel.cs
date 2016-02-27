using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DAL_Repository;
using EF_Model;
using GalaSoft.MvvmLight.Command;

namespace PL_WPF.ViewModels
{
    class ParameterBeheerViewModel : WorkspaceViewModel
    {
        public Parameters ParameterData { get; set; }
        public Strings StringsData { get; set; }
        public WinkelTypeViewModel WinkelData { get; private set; }
        public SchoolTypeViewModel SchoolData { get; private set; }
        public BedrijfTypeViewModel BedrijfData { get; private set; }
        public OphalingBeheerViewModel OphalingData { get; private set; }

        //Commands
        public ICommand SaveParametersCommand { get; set; }
        public ICommand RefreshParametersCommand { get; set; }
        public ICommand SaveStringsCommand { get; set; }
        public ICommand RefreshStringsCommand { get; set; }

        public ParameterBeheerViewModel(UnitOfWork ctx) : base(ctx)
        {
            Header = "ParameterBeheer";
            ParameterData = new Parameters();
            StringsData = new Strings();

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
    }
}
