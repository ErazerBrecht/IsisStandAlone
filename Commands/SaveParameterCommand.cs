using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class SaveParameterCommand : ICommand
    {
        private ParameterBeheerViewModel _viewModel;

        public SaveParameterCommand(ParameterBeheerViewModel viewModel)
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
            //TODO Change model inheritance...
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.SaveParameters();

        }
    }
}

