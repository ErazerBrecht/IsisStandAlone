using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    abstract class KlantTypeViewModel : INotifyPropertyChanged
    {
        #region AddType fullproperty
        private KlantType _addType;
        public KlantType AddType
        {
            get
            {
                return _addType;
            }
            private set
            {
                _addType = value;
                NoticeMe("AddType");
                _addType.PropertyChanged += _addType_PropertyChanged;
            }
        }

        private void _addType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NoticeMe("IsValid");

            if (e.PropertyName == "Naam")
            {
                AddType.InvalidKey = Ctx.KlantTypes.Exists(AddType.Type, AddType.Naam);
            }
        }
        #endregion

        #region SelectedType fullproperty + propertychanged event
        private KlantType _selectedType;
        public KlantType SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                NoticeMe("SelectedType");
                if (value != null)
                    _selectedType.PropertyChanged += SelectedTypePropertyChanged;
            }
        }

        private void SelectedTypePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var changed = sender as KlantType;

            try
            {
                Ctx.Complete();
            }
            catch (Exception ex)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowErrorBox("Er heeft zich een probleem voorgedaan bij het opslaan van een bestaande 'Klant Type' ("+ changed.Type + " " + changed.Naam + ")\n\nError: " + ex.Message);
            }

        }

        #endregion

        protected UnitOfWork Ctx;
        public CollectionViewSource ViewSource { get; private set; }

        #region Commands
        public ICommand SaveCommandEvent { get; private set; }
        public ICommand DeleteCommandEvent { get; private set; }
        #endregion

        public bool IsValid => AddType.CanSave;

        protected KlantTypeViewModel(UnitOfWork ctx)
        {
            AddType = new KlantType();
            Ctx = ctx;
            ViewSource = new CollectionViewSource();
            GetData();

            #region Buttons
            SaveCommandEvent = new RelayCommand(Add);
            DeleteCommandEvent = new RelayCommand(Delete);
            #endregion
        }

        public abstract void GetData();

        public void Delete()
        {
            try
            {
                Ctx.KlantTypes.Remove(SelectedType);
                Ctx.Complete();
            }
            catch (Exception ex)
            {
                Ctx.DiscardChanges();
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowErrorBox("Er heeft zich een probleem voorgedaan bij het verwijderen van een bestaande 'Klant Type' (" + SelectedType.Type + " " + SelectedType.Naam + ")\n\nError: " + ex.Message);
            }

            GetData();
        }

        public virtual void Add()
        {
            try
            {
                Ctx.KlantTypes.Add(AddType);
                Ctx.Complete();
            }
            catch (Exception ex)
            {
                Ctx.DiscardChanges();
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowErrorBox("Er heeft zich een probleem voorgedaan bij het toevoegen van een nieuwe 'Klant Type' (" + AddType.Type + " " + AddType.Naam + ")\n\nError: " + ex.Message);
            }

            GetData();
            AddType = new KlantType();
        }

        #region INotifyPropertyChanged
        protected void NoticeMe(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
