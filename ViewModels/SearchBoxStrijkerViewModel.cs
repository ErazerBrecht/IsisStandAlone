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

namespace ISIS.ViewModels
{
    class SearchBoxStrijkerViewModel : INotifyPropertyChanged
    {
        private int _oldLengthSearchBox = 0;
        //private ISIS_DataEntities _ctx;

        public string SearchBoxText
        {
        //    get
        //    {
        //        return "";          //Disables get error from XAML
        //    }

            set
            {
                int tempID;
                var tempSearchList = new List<Strijker>();

                //If text is getting longer search in items from the combobox self
                //If not search in the list with all the items (in this situation you're deleting chars)
                //Result: If you're adding chars, it will go faster because you need to search in a smaller list!
                if (_oldLengthSearchBox < value.Length && value.Length > 1)
                {
                    tempSearchList = SearchBoxResults;
                }
                else
                {
                    using (var ctx = new ISIS_DataEntities())
                    {
                        tempSearchList = ctx.Strijkers.ToList();
                    }
                }

                if (int.TryParse(value, out tempID))            //Check if user only typed numbers (an id)
                {
                    List<Strijker> tempList = tempSearchList.Where(k => k.ID.ToString().Contains(value)).ToList();
                    SearchBoxResults = tempList;
                }
                else
                {
                    //If the text contains off digits don't search after it in Naam and Voornaam (after all you will not find anything!)
                    if (!value.Any(char.IsDigit))
                    {
                        List<Strijker> tempList = tempSearchList.Where(k => k.Naam.ToString().ToLower().Contains(value.ToLower()) || k.Voornaam != null && k.Voornaam.ToString().ToLower().Contains(value.ToLower())).ToList();
                        SearchBoxResults = tempList;
                    }
                }

                _oldLengthSearchBox = value.Length;        //Save current textlength, in the future we can compare it to determine if the textlength became bigger or smaller!
            }
        }

        private List<Strijker> _searchBoxResults;
        public List<Strijker> SearchBoxResults
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

        private Strijker _searchBoxSelectedStrijker;
        public Strijker SearchBoxSelectedStrijker
        {
            get { return _searchBoxSelectedStrijker; }
            set
            {
                if (value != null)
                {
                    _searchBoxSelectedStrijker = value;
                    NoticeMe("SearchBoxSelectedStrijker");
                }
            }
        }
        public SearchBoxStrijkerViewModel()
        {
            _searchBoxSelectedStrijker = new Strijker();
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
