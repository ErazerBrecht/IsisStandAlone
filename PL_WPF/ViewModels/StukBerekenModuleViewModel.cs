using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using DAL_Repository;
using EF_Model;
using PL_WPF.Services;

namespace PL_WPF.ViewModels
{
    class StukBerekenModuleViewModel : PrestatieBerekenModuleViewModel
    {
        #region AddStukPrestatie full property
        private StukPrestatie _addStukPrestatie;

        public StukPrestatie AddStukPrestatie
        {
            get { return _addStukPrestatie; }
            set
            {
                _addStukPrestatie = value;
                NoticeMe("AddStukPrestatie");
                _addStukPrestatie.PropertyChanged += _addStukPrestatie_PropertyChanged;
            }
        }

        private void _addStukPrestatie_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsValid = false;
        }

        #endregion

        public StukBerekenModuleViewModel(UnitOfWork ctx) : base(ctx)
        {
            Init();
        }

        public override void Init()
        {
            AddStukPrestatie = new StukPrestatie();
        }

        public override void Bereken()
        {
            //Calculate...
            AddStukPrestatie.Tegoed = SelectedKlant.Tegoed;
            AddStukPrestatie.CalculatePrestatie();

            //The "prestatie" is calculated you're know able to save it!
            //But first check if there where validation errors
            if (AddStukPrestatie.CanSave)
                IsValid = true;
        }

        public override void EditLast(DatumBeheerViewModel vm)
        {
            IsValid = false;

            if (Ctx.StukPrestaties.Any())
            {
                //Get all previous StukPrestaties of the selected klant
                var tempPrestatie = Ctx.StukPrestaties.GetLatestPrestatie(SelectedKlant);
                if (tempPrestatie != null)
                {
                    AddStukPrestatie = tempPrestatie;
                    //Load the previous dates
                    vm.EditLast(tempPrestatie.Id);
                }
                else
                {
                    throw new Exception("Deze klant heeft geen vorige prestaties, u kunt niets wijzigen");
                }
            }
            else
            {
                throw new Exception("Er bevinden zich nog geen prestaties in de databank, u kunt niets wijzigen");
            }

            //Thinking reverse => The current Tegoed of the Klant was the NiewTegoed of the last prestatie
            SelectedKlant.Tegoed = AddStukPrestatie.RecalculatePrestatie(SelectedKlant.Tegoed);
        }

        public override void Cancel()
        {
            //Reset the changes we made
            Ctx.StukPrestaties.Refresh();
            Ctx.Klanten.Refresh();

            Init();
        }

        //Adding new prestatie
        public override void Save(DateTime lastdate)
        {
            //The "prestatie" will be saved, the next "prestatie" has to be calculated first!
            //Setting this one false, disbales the save button
            IsValid = false;

            AddStukPrestatie.Klant = SelectedKlant;

            //We query local context first to see if it's there.
            var klant = Ctx.Klanten.Get(SelectedKlant.Id);

            //We have it in the entity, need to update.
            if (klant != null)
            {
                klant.Tegoed = Convert.ToByte(AddStukPrestatie.NieuwTegoed);
                klant.LaatsteActiviteit = lastdate;
            }

            Ctx.StukPrestaties.Add(AddStukPrestatie);

        }

        //Updating last prestatie
        public override void UpdateLast()
        {
            //The "prestatie" will be saved, the next "prestatie" has to be calculated first!
            //Setting this one false, disbales the save button
            IsValid = false;
            SelectedKlant.Tegoed = Convert.ToByte(AddStukPrestatie.NieuwTegoed);

            ////Legacy code
            //    var klant = context.Klanten.Find(SelectedKlant.ID);

            //    //We have it in the entity, need to update.
            //    if (klant != null)
            //    {
            //        context.Entry(klant).CurrentValues.SetValues(SelectedKlant);
            //    }

            //    var prestatie = context.Prestaties.Find(AddPrestatie.Id);

            //    //We have it in the entity, need to update.
            //    if (prestatie != null)
            //    {
            //        context.Entry(prestatie).CurrentValues.SetValues(AddPrestatie);
            //    }
        }
    }
}
