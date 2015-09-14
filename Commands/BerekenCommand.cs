using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class BerekenCommand : ICommand
    {
        private BerekenModuleViewModel _viewModel;

        public BerekenCommand(BerekenModuleViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            //return _viewModel.CanAdd;
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.Bereken();       
        }
    }
}

