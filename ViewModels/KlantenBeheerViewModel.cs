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
    class KlantenBeheerViewModel : BeheerViewModel
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
                NoticeMe("SelectedKlant");
                NoticeMe("SoortKlantPlaatsItems");
                NoticeMe("IsComboBoxSoortKlantPlaatsEnabled");
                NoticeMe("ElektronischBetalenVisibility");
                _selectedKlant.PropertyChanged += _selectedKlant_PropertyChanged;
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
        }

        #endregion

        #region TextBoxSearch
        private int _oldLengthSearchBox = 0;

        public string SearchBoxText
        {
            set
            {
                int tempID;
                var tempSearchList = new List<Klant>();

                //If text is getting longer search in items from the combobox self
                //If not search in the list with all the items (in this situation you're deleting chars)
                //Result: If you're adding chars, it will go faster because you need to search in a smaller list!
                if (_oldLengthSearchBox < value.Length && SearchBoxResults != null)
                {
                        tempSearchList = SearchBoxResults;
                }
                else
                {
                    tempSearchList = ViewSource.View.SourceCollection.Cast<Klant>().ToList();
                }

                if (int.TryParse(value, out tempID))            //Check if user only typed numebers (an id)
                {
                    List<Klant> tempList = tempSearchList.Where(k => k.ID.ToString().Contains(value)).ToList();
                    SearchBoxResults = tempList;
                }
                else
                {
                    //If the text contains off digits don't search after it in Naam and Voornaam (after all you will not find anything!)
                    if (!value.Any(char.IsDigit))
                    {
                        List<Klant> tempList = tempSearchList.Where(k => k.Naam.ToString().ToLower().Contains(value.ToLower()) || k.Voornaam != null && k.Voornaam.ToString().ToLower().Contains(value.ToLower())).ToList();
                        SearchBoxResults = tempList;
                    }
                }

                _oldLengthSearchBox = value.Length;        //Save current textlength, in the future we can compare it to determine if the textlength became bigger or smaller!
            }
    }

        private List<Klant> _searchBoxResults;
        public List<Klant> SearchBoxResults
        {
            get
            {
                return _searchBoxResults;
            }

            private set
            {
                _searchBoxResults = value;
                NoticeMe("SearchBoxResults");
            }
        }

        public Klant SearchBoxSelectedKlant
        {
            set
            {
                if (value != null)
                    SelectedKlant = value;
            }
        }
        #endregion

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

                foreach (Klant k in ViewSource.View.SourceCollection)
                {
                    if (!k.CanSave)
                        return false;
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
                        SelectedKlant.CanValidateID = true;             //We should ony check the ID for unicity when the user is adding a new user!
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

                if(SelectedKlant.SoortKlant == "School")
                {
                    ctx.Scholen.Load();
                        return ctx.Scholen.Local.Select(s => s.ToString()).ToList();
                }

                if(SelectedKlant.SoortKlant == "Ophaling")
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
        #endregion

        public KlantenBeheerViewModel() : base()
        {
            Header = "KlantenBeheer";
            GetData();
            AddCommandEvent = new AddKlantCommand(this);

        }

        private void GetData()
        {
            Refresh(); 
            ViewSource.View.CollectionChanged += View_CurrentChanged;
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
            ctx.SaveChanges();
        } 

        protected override void View_CurrentChanged(object sender, EventArgs e)
        {
            SelectedKlant = (sender as CollectionView).CurrentItem as Klant;           
        }
    }
}
