using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class PreviousCommandKlant : ICommand
    {
        private KlantenBeheerViewModel _viewModel;

        public PreviousCommandKlant(KlantenBeheerViewModel viewModel)
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
            if (_viewModel.KlantenView.CurrentPosition > 0)
                _viewModel.KlantenView.MoveCurrentToPrevious();
        }
    }
}
