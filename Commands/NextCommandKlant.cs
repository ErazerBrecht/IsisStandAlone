using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class NextCommandKlant : ICommand
    {
        private KlantenBeheerViewModel _viewModel;

        public NextCommandKlant(KlantenBeheerViewModel viewModel)
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
            if (_viewModel.KlantenView.CurrentPosition < _viewModel.Klanten.Count() - 1)
                _viewModel.KlantenView.MoveCurrentToNext();
        }
    }
}
