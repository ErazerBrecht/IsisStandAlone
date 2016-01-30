using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;
using ISIS.Commands;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace ISIS.ViewModels
{
    class ParameterBeheerViewModel : BeheerViewModel
    {
        public Parameters ParameterData { get; set; }
        public Strings StringsData { get; set; }
        public WinkelBeheerViewModel WinkelData { get; private set; }
        public SchoolBeheerViewModel SchoolData { get; private set; }
        public BedrijfBeheerViewModel BedrijfData { get; private set; }
        public OphalingBeheerViewModel OphalingData { get; private set; }

        //Commands
        public ICommand SaveParametersCommand { get; set; }
        public ICommand RefreshParametersCommand { get; set; }
        public ICommand SaveStringsCommand { get; set; }
        public ICommand RefreshStringsCommand { get; set; }

        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public ParameterBeheerViewModel()
        {
            Header = "ParameterBeheer";
            ParameterData = new Parameters();
            StringsData = new Strings();

            WinkelData = new WinkelBeheerViewModel();
            SchoolData = new SchoolBeheerViewModel();
            BedrijfData = new BedrijfBeheerViewModel();
            OphalingData = new OphalingBeheerViewModel();

            #region Buttons
            SaveParametersCommand = new RelayCommand(
                () => SaveParameters(),
                () => IsValid
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

        //TODO, Implement MVVM Light everywhere!
        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public override bool Close()
        {
            return false;
        }
    }
}
