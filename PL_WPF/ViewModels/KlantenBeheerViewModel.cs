using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DAL_Repository;
using EF_Model;
using GalaSoft.MvvmLight.Command;

namespace PL_WPF.ViewModels
{
    class KlantenBeheerViewModel : WorkspaceViewModel
    {
        #region SelectedKlant fullproperty

        private Klant _selectedKlant;

        public Klant SelectedKlant
        {
            get { return _selectedKlant; }
            set
            {
                ButtonToevoegenContent = "Toevoegen";               //Reset button back to original content, otherwise it keeps on annuleren...
                _selectedKlant = value;
                NoticeMe("SelectedKlant");
                NoticeMe("SoortKlantPlaatsItems");
                NoticeMe("IsComboBoxSoortKlantPlaatsEnabled");
                NoticeMe("ElektronischBetalenVisibility");

                if (value != null)
                    _selectedKlant.PropertyChanged += _selectedKlant_PropertyChanged;
            }
        }

        private void _selectedKlant_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NoticeMe("IsValid");                                    //Everytime a propertychanges IsValid could change!
            NoticeMe("IsAddable");

            if (e.PropertyName == "Id")
            {
                SelectedKlant.InvalidId = Ctx.Klanten.Exists(SelectedKlant.Id);
            }
            else if (e.PropertyName == "TypeNaam")
            {
                NoticeMe("SoortKlantPlaatsItems");                  //If SoortKlant is adjusted the ItemSource of the combobox for SoortKlantPlaats should also change!
                NoticeMe("IsComboBoxSoortKlantPlaatsEnabled");
            }
            else if (e.PropertyName == "Betalingswijze")            //If the klant pays with money he doesn't need a usernumber, notice the UI of this!
            {
                NoticeMe("ElektronischBetalenVisibility");
            }
            else if (e.PropertyName == "Strijkbox")
            {
                if (SelectedKlant.Strijkbox == 0)
                    SelectedKlant.Waarborg = 0;
                //If klant doesn't have any strijkboxes, he can't have Waarborg anymore!
                NoticeMe("WaarborgVisibility");
            }
        }

        #endregion

        //Use this to save the first Klant that has an error!
        private Klant _errorKlant;

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

        #region IsValid => Bool to check if we can save our Klanten
        public bool IsValid
        {
            get
            {
                if (SelectedKlant != null && !SelectedKlant.CanSave)
                    return false;

                foreach (Klant k in ViewSource.View.SourceCollection) //Check if there is somewhere a validation error!
                {
                    if (!k.CanSave)
                    {
                        _errorKlant = k;
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

        public Visibility ElektronischBetalenVisibility
        {
            get
            {
                if (SelectedKlant?.Betalingswijze == "Elektronisch")
                    return Visibility.Visible;
                return Visibility.Hidden;
            }
        }

        public Visibility WaarborgVisibility
        {
            get
            {
                if (SelectedKlant?.Strijkbox != null && SelectedKlant?.Strijkbox > 0)
                {
                    if (SelectedKlant.Waarborg == null)
                        SelectedKlant.Waarborg = null;      //Force data validation

                    return Visibility.Visible;
                }

                return Visibility.Hidden;
            }
        }
        public bool IsComboBoxSoortKlantPlaatsEnabled
        {
            get
            {
                if (String.IsNullOrWhiteSpace(SelectedKlant?.TypeNaam))
                    return false;
                return true;
            }
        }
        #endregion

        #region ItemSources for Comboboxes
        public List<string> SoortKlantItems
        {
            get
            {
                List<string> data = new List<string>();
                data.Add("Winkel");
                data.Add("Ophaling");
                data.Add("Bedrijf");
                data.Add("School");
                return data;
            }
        }


        private List<string> _winkels;
        private List<string> _bedrijven;
        private List<string> _ophalingen;
        private List<string> _scholen;

        public List<string> SoortKlantPlaatsItems
        {
            get
            {
                if (SelectedKlant?.TypeNaam == null)
                    return null;
                else if (SelectedKlant.TypeNaam == "Winkel")
                    return _winkels;
                else if (SelectedKlant.TypeNaam == "Ophaling")
                    return _ophalingen;
                else if (SelectedKlant.TypeNaam == "Bedrijf")
                    return _bedrijven;
                else if (SelectedKlant.TypeNaam == "School")
                    return _scholen;

                return null;
            }
        }

        public List<string> BetalingswijzeItems
        {
            get
            {
                List<string> data = new List<string>();
                data.Add("Papier");
                data.Add("Elektronisch");
                return data;
            }
        }

        public List<string> BerichtItems
        {
            get
            {
                List<string> data = new List<string>();
                data.Add("Neen");
                data.Add("SMS");
                data.Add("E-mail");
                return data;
            }
        }

        public List<int> StrijkboxItems
        {
            get
            {
                List<int> data = new List<int>();
                data.Add(0);
                data.Add(1);
                data.Add(2);
                data.Add(4);
                data.Add(5);
                data.Add(6);
                return data;
            }
        }
        #endregion

        public KlantenBeheerViewModel(UnitOfWork ctx) : base(ctx)
        {
            Header = "KlantenBeheer";
            GetData();
            ButtonToevoegenContent = "Toevoegen";

            #region Commands / Buttons
            NextButton = new RelayCommand(NextKlant);
            PreviousButton = new RelayCommand(PreviousKlant);
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
            ViewSource = new CollectionViewSource {Source = Ctx.Klanten.GetAll()};
            ViewSource.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));            //Zorgt ervoor dat de DataGrid geordend is op ID!!
            SelectedKlant = ViewSource.View.CurrentItem as Klant;
        }

        private void NextKlant()
        {
            if (ViewSource.View.CurrentPosition < ViewSource.View.SourceCollection.Cast<object>().Count() - 1)
                ViewSource.View.MoveCurrentToNext();
        }

        private void PreviousKlant()
        {
            if (ViewSource.View.CurrentPosition > 0)
                ViewSource.View.MoveCurrentToPrevious();
        }

        private void Refresh()
        {
            ViewSource.Source = Ctx.Klanten.Refresh();
            ViewSource.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));            //Zorgt ervoor dat de DataGrid geordend is op ID!!
            SelectedKlant = ViewSource.View.CurrentItem as Klant;
        }

        public void Add()
        {

            if (ButtonToevoegenContent == "Toevoegen")
            {
                Klant addClient = new Klant();
                int tempId = 1;

                //Search for first valid ID           
                while (ViewSource.View.Cast<Klant>().Any(k => k.Id == tempId))          //The ViewSource.View contains every Klant, but first I have to cast it back to a klant!
                {
                    tempId++;
                }

                addClient.Id = tempId;
                addClient.Datum = DateTime.Now;
                addClient.Bericht = "Neen";
                addClient.Strijkbox = 0;
                SelectedKlant = addClient;
                ButtonToevoegenContent = "Annuleren";
            }
            else
            {
                SelectedKlant = ViewSource.View.CurrentItem as Klant;
                ButtonToevoegenContent = "Toevoegen";
            }
        }

        public void Delete()
        {
            if (SelectedKlant != null)
                Ctx.Klanten.Remove(SelectedKlant);
        }

        public void Save()
        {
            if (ButtonToevoegenContent == "Annuleren")
            {
                Ctx.Klanten.Add(SelectedKlant);
            }

            Ctx.Complete();
            ButtonToevoegenContent = "Toevoegen";
        }

        public void SetErrorAsSelected()
        {
            if (_errorKlant != null)            //If this is null the errorKlant is already the selected one!
                SelectedKlant = _errorKlant;
        }

        public override void LoadData()
        {
            //Because I'm not able to execute queries in the getter of the list for the combobox
            //I cache the items in seperate lists
            //Don't know why, but this solves my problem
            _winkels = Ctx.KlantTypes.Find(t => t.Type == "Winkel").Select(s => s.ToString()).ToList();
            _ophalingen = Ctx.KlantTypes.Find(t => t.Type == "Ophaling").Select(s => s.ToString()).ToList();
            _bedrijven = Ctx.KlantTypes.Find(t => t.Type == "Bedrijf").Select(s => s.ToString()).ToList();
            _scholen = Ctx.KlantTypes.Find(t => t.Type == "School").Select(s => s.ToString()).ToList();

            SelectedKlant = ViewSource.View.CurrentItem as Klant;
        }

        #region Searchbox Klant LOGIC
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
    }
}
