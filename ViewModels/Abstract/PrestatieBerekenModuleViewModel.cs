using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;
using ISIS.Commands;
using System.ComponentModel;
using System.Windows.Data;
using ISIS.Services;

namespace ISIS.ViewModels
{
    abstract class PrestatieBerekenModuleViewModel : WorkspaceViewModel
    {
        #region AddPrestatie full property
        private Prestatie _addPresatie;
        public Prestatie AddPrestatie
        {
            get
            {
                return _addPresatie;
            }
            protected set
            {
                _addPresatie = value;
                NoticeMe("AddPrestatie");
            }
        }
        #endregion

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
            set { _isValid = value; }
        }
        #endregion

        public PrestatieBerekenModuleViewModel()
        {
            Init();

            ButtonBerekenContent = "Berekenen";
            ButtonToevoegenContent = "Toevoegen";
            ButtonChangeContent = "Laatste prestatie aanpassen";

            //Data is just loaded so first have to recalculate before we can save!!
            IsValid = false;         
        }

        public abstract void Init();

        public abstract void Bereken();

        public abstract void Refresh();

        public virtual void SaveChanges(ISIS_DataEntities context)
        {
            //The "prestatie" will be saved, the next "prestatie" has to be calculated first!
            //Setting this one false, disbales the save button
            IsValid = false;

            //Adding new prestatie
            if (ButtonToevoegenContent == "Toevoegen")
            {
                int tempId = 1;

                //Search for first valid ID
                while (context.Prestaties.Any(p => p.Id == tempId))
                {
                    tempId++;
                }

                AddPrestatie.Id = tempId;
                AddPrestatie.KlantenNummer = SelectedKlant.ID;

                SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.NieuwTegoed);
                
                //TODO: Reanable this!!!!
                //TODO: After the new data adding functionality is done
                //SelectedKlant.LaatsteActiviteit = AddPrestatie.Datum;


                //We query local context first to see if it's there.
                var klant = context.Klanten.Find(SelectedKlant.ID);

                //We have it in the entity, need to update.
                if (klant != null)
                {
                    context.Entry(klant).CurrentValues.SetValues(SelectedKlant);
                }

                context.Prestaties.Add(AddPrestatie);
            }
            else
            {
                SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.NieuwTegoed);

                var klant = context.Klanten.Find(SelectedKlant.ID);

                //We have it in the entity, need to update.
                if (klant != null)
                {
                    context.Entry(klant).CurrentValues.SetValues(SelectedKlant);
                }

                var prestatie = context.Prestaties.Find(AddPrestatie.Id);

                //We have it in the entity, need to update.
                if (prestatie != null)
                {
                    context.Entry(prestatie).CurrentValues.SetValues(AddPrestatie);
                }

                ButtonBerekenContent = "Berekenen";
                ButtonToevoegenContent = "Toevoegen";
                ButtonChangeContent = "Laatste prestatie aanpassen";
            }
        }
    }
}
