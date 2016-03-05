using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DAL_Repository;
using EF_Model;
using GalaSoft.MvvmLight.Command;
using PL_WPF.Services;
using PropertyChanged;

namespace PL_WPF.ViewModels
{
    [ImplementPropertyChanged]
    class BerekenModuleViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region CurrentView full - property
        private PrestatieBerekenModuleViewModel _currentView;
        public PrestatieBerekenModuleViewModel CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                _currentView.PropertyChanged += _currentView_PropertyChanged;
            }
        }

        private void _currentView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsValid")
                NoticeMe("IsValid");
        }
        #endregion
        public DatumBeheerViewModel DatumViewModel { get; set; }
        
        public bool IsValid
        {
            get
            {
                //TODO: Denk hier nog eens over na...
                NoticeMe("IsPrintAble");
                return CurrentView.IsValid;
            }
        }

        public bool IsPrintAble
        {
            get
            {
                if (CurrentView is TijdBerekenModuleViewModel)
                    return CurrentView.IsValid;
                return false;
            }
        }

        #region Commands
        public ICommand BerekenCommandEvent { get; private set; }
        public ICommand SaveCommandEvent { get; private set; }
        public ICommand RefreshCommandEvent { get; private set; }
        public ICommand PrintCommandEvent { get; private set; }
        #endregion

        #region ButtonBerekenContent full property
        private string _buttonBerekenContent;

        public string ButtonBerekenContent
        {
            get { return _buttonBerekenContent; }
            set { _buttonBerekenContent = value; NoticeMe("ButtonBerekenContent"); }
        }
        #endregion

        #region ButtonToevoegenContent full property
        private string _buttonToevoegenContent;

        public string ButtonToevoegenContent
        {
            get { return _buttonToevoegenContent; }
            set { _buttonToevoegenContent = value; NoticeMe("ButtonToevoegenContent"); }
        }

        #endregion

        #region ButtonChangeContent full property
        private string _buttonChangeContent;

        public string ButtonChangeContent
        {
            get { return _buttonChangeContent; }
            set { _buttonChangeContent = value; NoticeMe("ButtonChangeContent"); }
        }

        #endregion

        #region SelectedKlant LOGIC
        public bool IsKlantSelected
        {
            get
            {
                if (SelectedKlant != null)
                    return true;
                return false;
            }
        }

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

                if (_selectedKlant != null)
                {
                    if (_selectedKlant.KlantType.StukTarief)
                        CurrentView = new StukBerekenModuleViewModel(Ctx);
                    else
                    {
                        CurrentView = new TijdBerekenModuleViewModel(Ctx);
                    }

                    DatumViewModel.Init();
                    ButtonBerekenContent = "Berekenen";
                    ButtonToevoegenContent = "Toevoegen";
                    ButtonChangeContent = "Laatste prestatie aanpassen";
                }

                CurrentView.SelectedKlant = value;
                CurrentView.LoadData();
                NoticeMe("SelectedKlant");
                NoticeMe("IsKlantSelected");
            }
        }
        #endregion

        #region Searchbox Klant LOGIC

        public IEnumerable<Klant> Klanten => Ctx.Klanten.GetAll();

        public AutoCompleteFilterPredicate<object> KlantenFilter
        {
            get
            {
                return (searchText, obj) =>
                {
                    var klant = obj as Klant;
                    return klant != null && (klant.ToString().Trim().ToUpper().Contains(searchText.ToUpper()));
                };
            }
        }
        #endregion

        public BerekenModuleViewModel(UnitOfWork ctx) : base(ctx)
        {
            Header = "Berekenmodule";
            ButtonBerekenContent = "Berekenen";
            ButtonToevoegenContent = "Toevoegen";
            ButtonChangeContent = "Laatste prestatie aanpassen";

            DatumViewModel = new DatumBeheerViewModel(ctx);
            CurrentView = new TijdBerekenModuleViewModel(ctx);

            BerekenCommandEvent = new RelayCommand(Bereken);
            SaveCommandEvent = new RelayCommand(Save);
            RefreshCommandEvent = new RelayCommand(Edit);
            PrintCommandEvent = new RelayCommand(Print);
        }

        public void Bereken()
        {
            if (SelectedKlant == null)
            {
                MessageBoxService messageBoxService = new MessageBoxService();
                messageBoxService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            CurrentView.Bereken();
        }

        public void Edit()
        {
            if (SelectedKlant == null)
            {
                MessageBoxService messageBoxService = new MessageBoxService();
                messageBoxService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            if (ButtonToevoegenContent == "Toevoegen")
            {
                //TODO: Denk hier nog eens over na...
                try
                {
                    CurrentView.EditLast(DatumViewModel);
                    ButtonBerekenContent = "Herberekenen";
                    ButtonToevoegenContent = "Aanpassen";
                    ButtonChangeContent = "Annuleren";
                }
                catch (Exception e)
                {
                    MessageBoxService messageService = new MessageBoxService();
                    messageService.ShowMessageBox(e.Message);
                }

            }
            else
            {
                ButtonBerekenContent = "Berekenen";
                ButtonToevoegenContent = "Toevoegen";
                ButtonChangeContent = "Laatste prestatie aanpassen";
                CurrentView.Cancel();
                DatumViewModel.Init();
            }
        }

        public void Save()
        {
            if (!DatumViewModel.HasDates)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen datum gekozen!");
                return;
            }

            try
            {
                //Add prestatie + dates to DB
                if (ButtonToevoegenContent == "Toevoegen")
                {
                    //Add date(s) to DB
                    Ctx.Datums.AddRange(DatumViewModel.NewDates);
                    //Add prestatie to DB (parameter needs to be last date)
                    CurrentView.Save(DatumViewModel.NewDates.Last().Date);

                    #region Legacy code

                    ////Add Dates to DB
                    //foreach (var d in DatumViewModel.NewDates)
                    //{
                    //    d.Id = CurrentView.AddPrestatie.Id;
                    //    context.Datum.Add(d);

                    //    //EF is retarded and thinks that I readded the Strijkers to the db, while I didn't.
                    //    //I Just use them as foreign relantionschip, I can't just use the id, because I need the name
                    //    //Manually said this is not true
                    //    //https://msdn.microsoft.com/en-us/magazine/dn166926.aspx
                    //    //This link explains it!

                    //    if (d.Strijker1 != null)
                    //        context.Entry(d.Strijker1).State = EntityState.Unchanged;
                    //    if (d.Strijker2 != null)
                    //        context.Entry(d.Strijker2).State = EntityState.Unchanged;
                    //    if (d.Strijker3 != null)
                    //        context.Entry(d.Strijker3).State = EntityState.Unchanged;
                    //    if (d.Strijker4 != null)
                    //        context.Entry(d.Strijker4).State = EntityState.Unchanged;
                    //    if (d.Strijker5 != null)
                    //        context.Entry(d.Strijker5).State = EntityState.Unchanged;
                    //}

                    #endregion
                }
                //Update (edit) last prestatie + dates
                else
                {
                    CurrentView.UpdateLast();

                    ButtonBerekenContent = "Berekenen";
                    ButtonToevoegenContent = "Toevoegen";
                    ButtonChangeContent = "Laatste prestatie aanpassen";
                }


                //Save changes to DB
                Ctx.Complete();
            }
            catch (Exception ex)
            {
                Ctx.DiscardChanges();
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowErrorBox("Er heeft zich een probleem voorgedaan bij het opslaan van een prestatie \n\nError: " + ex.Message);
            }

            //Re init CurrentView => Make new Prestatie, ...
            CurrentView.Init();
            DatumViewModel.Init();
        }

        public override void LoadData()
        {
            CurrentView.IsValid = false;
            CurrentView.LoadData();
        }

        public void Print()
        {
            if (!DatumViewModel.HasDates)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen datum gekozen!");
                return;
            }

            PrintWindow print = new PrintWindow();
            print.ShowPrintPreview(this);
        }

        #region Datavalidation implementation
        public string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case "SelectedKlant":
                        {
                            if (SelectedKlant == null)
                            {
                                result = "Selecteer een klant voor je verder gaat!";
                            }

                            break;
                        }
                }

                return result;
            }
        }

        public string Error { get; }
        #endregion
    }
}
