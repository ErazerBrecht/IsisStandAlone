using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class SaveKlantCommand : ICommand
    {
        private KlantenBeheerViewModel _viewModel;

        public SaveKlantCommand(KlantenBeheerViewModel viewModel)
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
                _viewModel.AddKlant(_viewModel.SelectedKlant);             
            }

            _viewModel.SaveChanges();        
            _viewModel.ButtonToevoegenContent = "Toevoegen";

        }
    }
}

