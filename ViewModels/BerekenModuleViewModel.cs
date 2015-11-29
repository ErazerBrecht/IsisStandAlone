using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ISIS.Commands;
using ISIS.Services;

namespace ISIS.ViewModels
{
    class BerekenModuleViewModel: BeheerViewModel, ISelectedKlant, IBereken
    {
        public DatumBeheerViewModel DatumViewModel { get; set; }
        public SearchBoxKlantViewModel SearchBoxViewModel { get; set; }
        public BerekenCommand BerekenCommandEvent { get; set; }

        #region CurrentView full property
        private PrestatieBerekenModuleViewModel _currentView;
        public PrestatieBerekenModuleViewModel CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                NoticeMe("CurrentView");
            }
        }
        #endregion

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
                if (_selectedKlant.SoortKlant.StukTarief)
                    CurrentView = new StukBerekenModuleViewModel(ctx);
                else
                    CurrentView = new TijdBerekenModuleViewModel(ctx);
                CurrentView.SelectedKlant = value;
                NoticeMe("SelectedKlant");
            }
        }

        public BerekenModuleViewModel()
        {
            Header = "Berekenmodule";
            ctx = new ISIS_DataEntities();
            DatumViewModel = new DatumBeheerViewModel(ctx);
            SearchBoxViewModel = new SearchBoxKlantViewModel(this);
            CurrentView = new StukBerekenModuleViewModel(ctx);
            BerekenCommandEvent = new BerekenCommand(this);
        }

        public override bool IsValid
        {
            get { return CurrentView.IsValid; }
        }

        public void Bereken()
        {
            CurrentView.Bereken();
        }

        public override void Refresh()
        {
            CurrentView.Refresh();
        }

        public override void SaveChanges()
        {
            if (DatumViewModel.CurrentDates.Count < 1)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen datum gekozen!");
                return;
            }

            CurrentView.SaveChanges();

            foreach (var d in DatumViewModel.CurrentDates)
            {
                d.Id = CurrentView.AddPrestatie.Id;
            }

            ctx.Datum.AddRange(DatumViewModel.CurrentDates);
            ctx.SaveChanges();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }
        public override void Add()
        {
            throw new NotImplementedException();
        }
    }
}
