using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class AddPersoneelCommand : ICommand
    {
        private PersoneelBeheerViewModel _viewModel;

        public AddPersoneelCommand(PersoneelBeheerViewModel viewModel)
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
            return true;
        }

        public void Execute(object parameter)
        {
            if (_viewModel.ButtonToevoegenContent == "Toevoegen")
            {
                Strijker addPersoneel = new Strijker();
                int tempId = 1;

                //Search for first valid ID           
                while (_viewModel.ViewSource.View.Cast<Strijker>().Any(k => k.ID == tempId))          //The ViewSource.View contains every Klant, but first I have to cast it back to a klant!
                {
                    tempId++;
                }

                addPersoneel.ID = tempId;
                addPersoneel.IndienstVanaf = DateTime.Now;
                _viewModel.SelectedPersoneel = addPersoneel;
                _viewModel.ButtonToevoegenContent = "Annuleren";
            }
            else
            {
                _viewModel.SelectedPersoneel = _viewModel.ViewSource.View.CurrentItem as Strijker;
                _viewModel.ButtonToevoegenContent = "Toevoegen";
            }
        }
    }
}

