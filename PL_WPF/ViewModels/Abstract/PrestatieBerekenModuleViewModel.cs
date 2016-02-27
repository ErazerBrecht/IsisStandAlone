using System;
using DAL_Repository;
using EF_Model;

namespace PL_WPF.ViewModels
{
    abstract class PrestatieBerekenModuleViewModel : WorkspaceViewModel
    {

        #region SelectedKlant full property
        private Klant _selectedKlant;
        public Klant SelectedKlant
        {
            get
            {
                return _selectedKlant;
            }

            set
            {
                _selectedKlant = value;
                NoticeMe("SelectedKlant");
            }
        }
        #endregion

        #region IsValid full property => used for enable "save" button
        private bool _isValid;
        public bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                // if (isValid == true) => check for validation errors
                return _isValid;
            }
            set
            {
                _isValid = value;
                NoticeMe("IsValid");
            }
        }
        #endregion

        protected PrestatieBerekenModuleViewModel(UnitOfWork ctx) : base(ctx)
        {
            //Data is just loaded so first have to recalculate before we can save!!
            IsValid = false;         
        }

        public abstract void Init();

        public abstract void Bereken();

        public abstract void EditLast(DatumBeheerViewModel vm);

        public abstract void Cancel();

        public abstract void Save(DateTime lastdate);

        //TODO: Place base code here, less duplicate code
        public abstract void UpdateLast();
    }
}
