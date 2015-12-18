using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ISIS.Models;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class DatumBeheerViewModel : BeheerViewModel
    {
        #region AddDatum fullproperty
        private Datum _addDatum;
        public Datum AddDatum
        {
            get
            {
                return _addDatum;
            }
            private set
            {
                _addDatum = value;
                NoticeMe("AddDatum");
            }
        }
        #endregion

        public ISIS_DataEntities ctx { get; set; }
        public ObservableCollection<Datum> CurrentDates { get; set; }

        #region Strijkers list for searchbox full property
        private IEnumerable<Strijker> _strijkers;
        public IEnumerable<Strijker> Strijkers
        {
            get { return _strijkers; }
            set
            {
                _strijkers = value;
                NoticeMe("Strijkers");
            }
        }
        #endregion

        public override bool IsValid        //Used to disable or Enable Toevoegen button
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public DatumBeheerViewModel(ISIS_DataEntities context)
        {
            ctx = context;
            CurrentDates = new ObservableCollection<Datum>();

            AddDatum = new Datum { Date = DateTime.Now };
        }

        //Gets called by MainvViewModel
        public override void LoadData()
        {
            using (var context = new ISIS_DataEntities())
            {
                Strijkers = context.Strijkers.ToList();
            }
        }

        public AutoCompleteFilterPredicate<object> StrijkerFilter
        {
            get
            {
                return (searchText, obj) =>
                    Convert.ToString((obj as Strijker).ID).Contains(searchText) || (obj as Strijker).Naam.Contains(searchText);
            }
        }

        public override void Delete()
        {
            //TODO
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }

        public override void Add()
        {
            CurrentDates.Add(AddDatum);
            ctx.Datum.Add(AddDatum);

            //EF is retarded and thinks that I readded the Strijkers to the db, while I didn't.
            //I Just use them as foreign relantionschip, I can't just use the id, because I need the name
            //Manually said this is not true
            //https://msdn.microsoft.com/en-us/magazine/dn166926.aspx
            //This link explains it!

            if (AddDatum.Strijker1 != null)
                ctx.Entry(AddDatum.Strijker1).State = EntityState.Unchanged;
            if (AddDatum.Strijker2 != null)
                ctx.Entry(AddDatum.Strijker2).State = EntityState.Unchanged;
            if (AddDatum.Strijker3 != null)
                ctx.Entry(AddDatum.Strijker3).State = EntityState.Unchanged;
            if (AddDatum.Strijker4 != null)
                ctx.Entry(AddDatum.Strijker4).State = EntityState.Unchanged;
            if (AddDatum.Strijker5 != null)
                ctx.Entry(AddDatum.Strijker5).State = EntityState.Unchanged;

            AddDatum = new Datum { Date = DateTime.Now };
        }

        public override void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
