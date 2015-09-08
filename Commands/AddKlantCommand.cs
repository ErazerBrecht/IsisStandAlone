using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class AddKlantCommand : ICommand
    {
        private KlantenBeheerViewModel _viewModel;

        public AddKlantCommand(KlantenBeheerViewModel viewModel)
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
                Klant addClient = new Klant();
                int tempId = 1;

                //Search for first valid ID           
                while (_viewModel.ViewSource.View.Cast<Klant>().Any(k => k.ID == tempId))          //The ViewSource.View contains every Klant, but first I have to cast it back to a klant!
                {
                    tempId++;
                }

                addClient.ID = tempId;
                addClient.Datum = DateTime.Now;
                _viewModel.SelectedKlant = addClient;
                _viewModel.ButtonToevoegenContent = "Annuleren";
            }
            else
            {
                _viewModel.SelectedKlant = _viewModel.ViewSource.View.CurrentItem as Klant;
                _viewModel.ButtonToevoegenContent = "Toevoegen";
            }
        }
    }
}

