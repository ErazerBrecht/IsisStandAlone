using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class DeleteExtendCommand : ICommand
    {
        private BeheerExtendViewModel _viewModel;

        public DeleteExtendCommand(BeheerExtendViewModel viewModel)
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
            if (_viewModel.ButtonToevoegenContent == "Toevoegen")
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            _viewModel.Delete();
        }
    }
}

