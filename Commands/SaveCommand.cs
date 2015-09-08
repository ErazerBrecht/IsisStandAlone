using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class SaveCommand : ICommand
    {
        private BeheerViewModel _viewModel;

        public SaveCommand(BeheerViewModel viewModel)
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
            //TODO: Check Validation!
            return true;
        }

        public void Execute(object parameter)
        {
            if (_viewModel.ButtonToevoegenContent == "Annuleren")
            {
                _viewModel.Add();             
            }

            _viewModel.SaveChanges();        
            _viewModel.ButtonToevoegenContent = "Toevoegen";

        }
    }
}

