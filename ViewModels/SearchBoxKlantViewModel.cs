using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ISIS.Models;

namespace ISIS.ViewModels
{
    class SearchBoxKlantViewModel : INotifyPropertyChanged
    {
        private ISelectedKlant _viewmodel;
        private int _oldLengthSearchBox = 0;
        private ISIS_DataEntities _ctx;

        public string SearchBoxText
        {
        //    get
        //    {
        //        return "";          //Disables get error from XAML
        //    }

            set
            {
                int tempID;
                var tempSearchList = new List<Klant>();

                //If text is getting longer search in items from the combobox self
                //If not search in the list with all the items (in this situation you're deleting chars)
                //Result: If you're adding chars, it will go faster because you need to search in a smaller list!
                if (_oldLengthSearchBox < value.Length && value.Length > 1)
                {
                    tempSearchList = SearchBoxResults;
                }
                else
                {
                    tempSearchList = _ctx.Klanten.ToList();
                }

                if (int.TryParse(value, out tempID))            //Check if user only typed numbers (an id)
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
            get
            {
                return _viewmodel.SelectedKlant;
            }
            set
            {
                if (value != null)
                    _viewmodel.SelectedKlant = value;
            }
        }
        public SearchBoxKlantViewModel(ISelectedKlant viewmodel)
        {
            _viewmodel = viewmodel;
            _ctx = ((WorkspaceViewModel) viewmodel).ctx;
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

    public interface ISelectedKlant
    {
       Klant SelectedKlant { get; set; }
    }

    public class SearchComboBox : ComboBox
    {

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            //Disables automatic selection when DropDownsOpen!
            if (this.IsEditable && this.IsDropDownOpen == false && this.StaysOpenOnEdit)
            {
                this.IsDropDownOpen = true;
            }
        }

    }
}
