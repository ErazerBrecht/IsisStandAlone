using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using DAL_Repository;
using EF_Model;
using GalaSoft.MvvmLight.Command;
using PL_WPF.Services;

namespace PL_WPF.ViewModels
{
    class PersoneelBeheerViewModel : WorkspaceViewModel
    {
        private Strijker _errorPersoneel;

        #region SelectedPersoneel
        private Strijker _selectedPersoneel;

        public Strijker SelectedPersoneel
        {
            get { return _selectedPersoneel; }
            set
            {
                ButtonToevoegenContent = "Toevoegen";               //Reset button back to original content, otherwise it keeps on annuleren...
                _selectedPersoneel = value;

                if (value != null)
                {
                    NoticeMe("SelectedPersoneel");
                    _selectedPersoneel.PropertyChanged += _selectedPersoneel_PropertyChanged;
                }
            }
        }

        private void _selectedPersoneel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NoticeMe("IsValid");                                    //Everytime a propertychanges IsValid could change!
            NoticeMe("IsAddable");

            if (e.PropertyName == "Id")
            {
                if(SelectedPersoneel.Id != null)
                SelectedPersoneel.InvalidId = Ctx.Strijkers.Exists(Convert.ToInt32(SelectedPersoneel.Id));
            }
        }

        #endregion

        public CollectionViewSource ViewSource { get; private set; }

        #region Commands
        public ICommand NextButton { get; private set; }
        public ICommand PreviousButton { get; private set; }
        public ICommand RefreshButton { get; private set; }
        public ICommand DeleteButton { get; private set; }
        public ICommand AddButton { get; private set; }
        public ICommand SaveButton { get; private set; }
        public ICommand NextErrorButton { get; private set; }
        #endregion

        #region IsValid => Bool to check if we can save our Strijksters
        public bool IsValid
        {
            get
            {
                if (SelectedPersoneel != null && !SelectedPersoneel.CanSave)
                    return false;

                foreach (Strijker s in ViewSource.View.SourceCollection) //Check if there is somewhere a validation error!
                {
                    if (!s.CanSave)
                    {
                        _errorPersoneel = s;
                        return false;
                    }
                }
                return true;
            }
        }

        #endregion

        #region ButtonToevoegenContent
        private string _buttonToevoegenContent;
        public string ButtonToevoegenContent
        {
            get { return _buttonToevoegenContent; }

            set
            {
                _buttonToevoegenContent = value;
                NoticeMe("ButtonToevoegenContent");
            }
        }
        #endregion

        #region Convertor properties
        public bool IsIdReadOnly
        {
            get
            {
                if (ButtonToevoegenContent == "Annuleren")
                    return false;
                return true;
            }
        }

        public bool IsAddable
        {
            get
            {
                if (ButtonToevoegenContent == "Toevoegen" && !IsValid)
                    return false;
                return true;
            }
        }
        #endregion

        public PersoneelBeheerViewModel(UnitOfWork ctx) : base(ctx)
        {
            Header = "PersoneelBeheer";
            ViewSource = new CollectionViewSource();
            GetData();
            ButtonToevoegenContent = "Toevoegen";

            #region Commands / Buttons
            NextButton = new RelayCommand(NextStrijker);
            PreviousButton = new RelayCommand(PreviousKStrijker);
            RefreshButton = new RelayCommand(Refresh);
            DeleteButton = new RelayCommand(Delete);
            AddButton = new RelayCommand(Add);
            SaveButton = new RelayCommand(
                () => Save(),
                () => IsValid
            );
            NextErrorButton = new RelayCommand(SetErrorAsSelected);
            #endregion
        }

        private void GetData()
        {
            ViewSource.Source = Ctx.Strijkers.GetAll();
            ViewSource.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));            //Zorgt ervoor dat de DataGrid geordend is op ID!!
            SelectedPersoneel = ViewSource.View.CurrentItem as Strijker;
        }

        private void NextStrijker()
        {
            if (ViewSource.View.CurrentPosition < ViewSource.View.SourceCollection.Cast<object>().Count() - 1)
                ViewSource.View.MoveCurrentToNext();
        }

        private void PreviousKStrijker()
        {
            if (ViewSource.View.CurrentPosition > 0)
                ViewSource.View.MoveCurrentToPrevious();
        }

        private void Refresh()
        {
            ViewSource.Source = Ctx.Strijkers.Refresh();
            ViewSource.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));            //Zorgt ervoor dat de DataGrid geordend is op ID!!
            SelectedPersoneel = ViewSource.View.CurrentItem as Strijker;
        }

        public void Add()
        {
            if (ButtonToevoegenContent == "Toevoegen")
            {
                Strijker addPersoneel = new Strijker();
                int tempId = 1;

                //Search for first valid ID           
                while (ViewSource.View.Cast<Strijker>().Any(s => s.Id == tempId))          //The ViewSource.View contains every Klant, but first I have to cast it back to a klant!
                {
                    tempId++;
                }

                addPersoneel.Id = tempId;
                addPersoneel.IndienstVanaf = DateTime.Now;
                SelectedPersoneel = addPersoneel;
                ButtonToevoegenContent = "Annuleren";
            }
            else
                SelectedPersoneel = ViewSource.View.CurrentItem as Strijker;
        }

        public void Delete()
        {
            Ctx.Strijkers.Remove(SelectedPersoneel);
        }

        public void Save()
        {
            if (ButtonToevoegenContent == "Annuleren")
            {
                Ctx.Strijkers.Add(SelectedPersoneel);
            }

            try
            {
                Ctx.Complete();
            }
            catch (Exception ex)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowErrorBox("Er heeft zich een probleem voorgedaan bij het opslaan van de strijksters \n\nError: " + ex.Message);
            }

            ButtonToevoegenContent = "Toevoegen";
        }

        public void SetErrorAsSelected()
        {
            if (_errorPersoneel != null)            //If this is null the errorPersoneel is already the selected one!
                SelectedPersoneel = _errorPersoneel;
        }
    }
}
