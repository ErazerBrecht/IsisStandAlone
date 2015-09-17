using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Data.Entity;
using ISIS.Commands;
using System.Windows;

namespace ISIS.ViewModels
{
    class KlantenBeheerViewModel : BeheerExtendViewModel, ISelectedKlant
    {
        #region SelectedKlant fullproperty

        private Klant _selectedKlant;

        public Klant SelectedKlant
        {
            get { return _selectedKlant; }
            set
            {
                _selectedKlant = value;
                ButtonToevoegenContent = "Toevoegen";               //Reset button back to original content, otherwise it keeps on annuleren...
                if (value != null)
                {
                    NoticeMe("SelectedKlant");
                    NoticeMe("SoortKlantPlaatsItems");
                    NoticeMe("IsComboBoxSoortKlantPlaatsEnabled");
                    NoticeMe("ElektronischBetalenVisibility");
                    _selectedKlant.PropertyChanged += _selectedKlant_PropertyChanged;
                }
            }
        }

        private void _selectedKlant_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SoortKlant")
            {
                NoticeMe("SoortKlantPlaatsItems");                  //If SoortKlant is adjusted the ItemSource of the combobox for SoortKlantPlaats should also change!
                NoticeMe("IsComboBoxSoortKlantPlaatsEnabled");
            }
            else if (e.PropertyName == "Betalingswijze")
                NoticeMe("ElektronischBetalenVisibility");
            else if (e.PropertyName == "Strijkbox")
            {
                if (SelectedKlant.Strijkbox == 0)
                    SelectedKlant.Waarborg = 0;                     //If klant doesn't have any strijkboxes, he can't have Waarborg anymore!
                NoticeMe("WaarborgVisibility");
            }
        }

        #endregion

        private Klant _errorKlant;

        public SearchBoxKlantViewModel SearchBoxViewModel { get; private set; }

        #region KlantenBeheerViewModel specific commands
        public AddKlantCommand AddCommandEvent { get; private set; }
        #endregion

        #region Overrided properties
        public override bool IsValid
        {
            get
            {
                if (!SelectedKlant.CanSave)
                    return false;

                foreach (Klant k in ViewSource.View.SourceCollection)           //Check if there is somewhere a validation error!
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

        public override string ButtonToevoegenContent
        {
            get
            {
                return base.ButtonToevoegenContent;
            }

            set
            {
                if (SelectedKlant != null)
                {
                    if (value == "Annuleren")
                        SelectedKlant.CanValidateID = true;             //We should ony check the ID for unicity when the user is adding a new klant!
                    else                                                
                        SelectedKlant.CanValidateID = false;
                }

                base.ButtonToevoegenContent = value;
            }
        }
        #endregion

        #region Convertor properties
        public Visibility ElektronischBetalenVisibility
        {
            get
            {
                if (SelectedKlant.Betalingswijze == "Elektronisch")
                {
                    if (String.IsNullOrWhiteSpace(SelectedKlant.Gebruikersnummer))
                        SelectedKlant.Gebruikersnummer = null;      //Force data validation

                    return Visibility.Visible;
                }

                return Visibility.Hidden;
            }
       }

        public Visibility WaarborgVisibility
        {
            get
            {
                if (SelectedKlant.Strijkbox != null && SelectedKlant.Strijkbox > 0)
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
                if (String.IsNullOrWhiteSpace(SelectedKlant.SoortKlant))
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

        public List<string> SoortKlantPlaatsItems
        {
            get
            {
                    if (SelectedKlant.SoortKlant == "Winkel")
                    {
                        ctx.Winkels.Load();
                        return ctx.Winkels.Local.Select(s => s.ToString()).ToList();     //Convert Winkels to list of strings
                    }

                    if (SelectedKlant.SoortKlant == "Bedrijf")
                    {
                        ctx.Bedrijven.Load();
                        return ctx.Bedrijven.Local.Select(s => s.ToString()).ToList();
                    }

                    if (SelectedKlant.SoortKlant == "School")
                    {
                        ctx.Scholen.Load();
                        return ctx.Scholen.Local.Select(s => s.ToString()).ToList();
                    }

                    if (SelectedKlant.SoortKlant == "Ophaling")
                    {
                        ctx.Ophalingen.Load();
                        return ctx.Ophalingen.Local.Select(s => s.ToString()).ToList();
                    }

                return null;
            }
        }

        public List<string> BetalingswijzeItems {
            get
            {
                List<string> data = new List<string>();
                data.Add("Papier");
                data.Add("Elektronisch");
                return data;
            }
        }

        public List<string> BerichtItems { 
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

        public ICollectionView KlantenView
        {
            get
            {
                return ViewSource.View;
            }
        }
        #endregion

        public KlantenBeheerViewModel() : base()
        {
            Header = "KlantenBeheer";
            GetData();
            DeleteCommandEvent = new DeleteExtendCommand(this);
            RefreshCommandEvent = new RefreshExtendCommand(this);
            AddCommandEvent = new AddKlantCommand(this);
            SearchBoxViewModel = new SearchBoxKlantViewModel(this);
        }

        private void GetData()
        {
            Refresh(); 
            SelectedKlant = ViewSource.View.CurrentItem as Klant;      
        }

        public override void Add()
        {
            ctx.Klanten.Add(SelectedKlant);
        }

        public override void Delete()
        {
            ctx.Klanten.Remove(SelectedKlant);
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Klanten.Load();
            ViewSource.Source = ctx.Klanten.Local;
            ViewSource.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));            //Zorgt ervoor dat de DataGrid geordend is op ID!!
        }
        
        public override void SaveChanges()
        {
            if (ButtonToevoegenContent == "Annuleren")
            {
                Add();
            }

            ctx.SaveChanges();
            ButtonToevoegenContent = "Toevoegen";
        }

        public override void SetErrorAsSelected()
        {
            if (_errorKlant != null)            //If this is null the errorKlant is already the selected one!
                SelectedKlant = _errorKlant;
        }
    }
}
