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
    class TijdBerekenModuleViewModel : PrestatieBerekenModuleViewModel
    {
        #region CurrentParameter full property
        private Parameters _currentParameters;
        public Parameters CurrentParameters
        {
            get
            {
                return _currentParameters;
            }
            private set
            {
                _currentParameters = value;
                NoticeMe("CurrentParameters");
            }
        }
        #endregion

        public TijdBerekenModuleViewModel() : base()
        {
            CurrentParameters = new Parameters();
            LoadData();
        }

        public override void Init()
        {
            AddPrestatie = new Prestatie();
            AddPrestatie.TijdPrestaties = new TijdPrestatie();
        }

        public override void LoadData()
        {
            //Load parameters from settings!
            CurrentParameters.LoadParameters();
        }

        public override void Bereken()
        {
            //Check if there is a klant selected
            if (SelectedKlant == null)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            //Add current parameter values into the prestatie!
            AddPrestatie.TijdPrestaties.AddParameters(CurrentParameters);

            //Calculate how long every part takes
            CalculateStrijk();

            //Calculate the "administratie" time
            if (AddPrestatie.TijdPrestaties.TotaalStrijk < 20)
                AddPrestatie.TijdPrestaties.TijdAdministratie = 5;
            else if (AddPrestatie.TijdPrestaties.TotaalStrijk < 40)
                AddPrestatie.TijdPrestaties.TijdAdministratie = 10;
            else if (AddPrestatie.TijdPrestaties.TotaalStrijk < 80)
                AddPrestatie.TijdPrestaties.TijdAdministratie = 15;
            else
                AddPrestatie.TijdPrestaties.TijdAdministratie = 20;

            AddPrestatie.TotaalMinuten = AddPrestatie.TijdPrestaties.TotaalHemden + AddPrestatie.TijdPrestaties.TotaalLakens1 + AddPrestatie.TijdPrestaties.TotaalLakens2 + AddPrestatie.TijdPrestaties.TotaalAndereStrijk + AddPrestatie.TijdPrestaties.TijdAdministratie;

            AddPrestatie.TotaalBetalen = AddPrestatie.TotaalMinuten - SelectedKlant.Tegoed;
            AddPrestatie.TotaalDienstenChecks = Convert.ToByte(Math.Ceiling(AddPrestatie.TotaalBetalen / 60.0));
            if (AddPrestatie.TotaalDienstenChecks == 0)
                AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed - AddPrestatie.TotaalMinuten;
            else
                AddPrestatie.NieuwTegoed = (AddPrestatie.TotaalDienstenChecks * 60) - AddPrestatie.TotaalBetalen;

            //The "prestatie" is calculated you're know able to save it!
            IsValid = true;
        }

        private void CalculateStrijk()
        {
            AddPrestatie.TijdPrestaties.TotaalHemden = (int)Math.Ceiling(AddPrestatie.TijdPrestaties.AantalHemden * AddPrestatie.TijdPrestaties.ParameterHemden);
            AddPrestatie.TijdPrestaties.TotaalLakens1 = (int)Math.Ceiling(AddPrestatie.TijdPrestaties.AantalLakens1 * AddPrestatie.TijdPrestaties.ParameterLakens1);
            AddPrestatie.TijdPrestaties.TotaalLakens2 = (int)Math.Ceiling(AddPrestatie.TijdPrestaties.AantalLakens2 * AddPrestatie.TijdPrestaties.ParameterLakens2);
            AddPrestatie.TijdPrestaties.TotaalAndereStrijk = (int)Math.Ceiling(AddPrestatie.TijdPrestaties.TijdAndereStrijk * AddPrestatie.TijdPrestaties.ParameterAndereStrijk);
            AddPrestatie.TijdPrestaties.TotaalStrijk = (AddPrestatie.TijdPrestaties.AantalHemden + AddPrestatie.TijdPrestaties.AantalLakens1 + AddPrestatie.TijdPrestaties.AantalLakens2 + AddPrestatie.TijdPrestaties.AantalAndereStrijk);
        }

        public override void Refresh()
        {
            IsValid = false;

            if (SelectedKlant == null)
            {
                MessageBoxService messageBoxService = new MessageBoxService();
                messageBoxService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            if (ButtonChangeContent == "Laatste prestatie aanpassen")
            {
                using (var context = new ISIS_DataEntities())
                {
                    if (context.Prestaties.Any())
                    {
                        //Get all previous TijdPrestaties of the selected klant
                        var previousPrestaties =
                            context.TijdPrestaties.Where(p => p.Prestaties.KlantenNummer == SelectedKlant.ID);

                        if (previousPrestaties.Any())
                        {
                            //Get the id of the latest tijdprestatie
                            var tempId = previousPrestaties.OrderByDescending(p => p.Id).First().Id;
                            //Get the prestatie with the same id of the latest tijdprestatie
                            AddPrestatie = context.Prestaties.First(p => p.Id == tempId);
                        }
                        else
                        {
                            MessageBoxService messageBoxService = new MessageBoxService();
                            messageBoxService.ShowMessageBox(
                                "Deze klant heeft geen vorige prestaties, u kunt niets wijzigen");
                            return;
                        }
                    }
                    else
                    {
                        MessageBoxService messageBoxService = new MessageBoxService();
                        messageBoxService.ShowMessageBox(
                            "Er bevinden zich nog geen prestaties in de databank, u kunt niets wijzigen");
                        return;
                    }

                    //Change the current parameters to the parameters that where active at the time the parameter was made!
                    CurrentParameters.ParameterHemden = AddPrestatie.TijdPrestaties.ParameterHemden;
                    CurrentParameters.ParameterLakens1 = AddPrestatie.TijdPrestaties.ParameterLakens1;
                    CurrentParameters.ParameterLakens2 = AddPrestatie.TijdPrestaties.ParameterLakens2;
                    CurrentParameters.ParameterAndereStrijk = AddPrestatie.TijdPrestaties.ParameterAndereStrijk;

                    //Thinking reverse => The current Tegoed of the Klant was the NiewTegoed of the last prestatie
                    AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed;

                    CalculateStrijk();

                    AddPrestatie.TotaalMinuten = AddPrestatie.TijdPrestaties.TotaalHemden +
                                                 AddPrestatie.TijdPrestaties.TotaalLakens1 +
                                                 AddPrestatie.TijdPrestaties.TotaalLakens2 +
                                                 AddPrestatie.TijdPrestaties.TotaalAndereStrijk +
                                                 AddPrestatie.TijdPrestaties.TijdAdministratie;

                    //Recalculate the previous Tegoed of the klant
                    if (AddPrestatie.TotaalDienstenChecks > 0)
                    {
                        AddPrestatie.TotaalBetalen = (AddPrestatie.TotaalDienstenChecks*60) - AddPrestatie.NieuwTegoed;
                        SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.TotaalMinuten - AddPrestatie.TotaalBetalen);
                    }
                    else
                    {
                        AddPrestatie.TotaalBetalen = 0;
                        SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.TotaalMinuten + AddPrestatie.NieuwTegoed);
                    }

                    ButtonBerekenContent = "Herberekenen";
                    ButtonToevoegenContent = "Aanpassen";
                    ButtonChangeContent = "Annuleren";
                }
            }
            else
            {
                ButtonBerekenContent = "Berekenen";
                ButtonToevoegenContent = "Toevoegen";
                ButtonChangeContent = "Laatste prestatie aanpassen";

                using (var context = new ISIS_DataEntities())
                {
                    context.Entry(AddPrestatie).Reload();
                    context.Entry(SelectedKlant).Reload();
                }

                CurrentParameters = new Parameters();
                AddPrestatie = new Prestatie();
                AddPrestatie.TijdPrestaties = new TijdPrestatie();
                AddPrestatie.TijdPrestaties.AddParameters(CurrentParameters);
            }
        }
    }
}
