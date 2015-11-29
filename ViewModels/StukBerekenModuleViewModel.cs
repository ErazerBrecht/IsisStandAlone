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
    class StukBerekenModuleViewModel : PrestatieBerekenModuleViewModel
    {
        public StukBerekenModuleViewModel(ISIS_DataEntities context) : base(context)
        {
            AddPrestatie.StukPrestaties = new StukPrestatie();         
        }

        public override void Bereken()
        {
            if (SelectedKlant == null)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            //Totaal needs to be saved in StukPrestaties otherwise it won't be placed in the DB
            AddPrestatie.TotaalMinuten = AddPrestatie.StukPrestaties.Totaal;

            //Calculate...
            AddPrestatie.TotaalBetalen = AddPrestatie.TotaalMinuten - SelectedKlant.Tegoed;
            AddPrestatie.TotaalDienstenChecks = Convert.ToByte(Math.Ceiling(AddPrestatie.TotaalBetalen / 60.0));
            if (AddPrestatie.TotaalDienstenChecks == 0)
                AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed - AddPrestatie.TotaalMinuten;
            else
                AddPrestatie.NieuwTegoed = (AddPrestatie.TotaalDienstenChecks * 60) - AddPrestatie.TotaalBetalen;

            SetIsValid(true);
        }

        public override void Refresh()
        {
            SetIsValid(false);

            if (SelectedKlant == null)
            {
                MessageBoxService messageBoxService = new MessageBoxService();
                messageBoxService.ShowMessageBox("Je hebt nog geen klant gekozen!");
                return;
            }

            if (ButtonChangeContent == "Laatste prestatie aanpassen")
            {

                if (ctx.StukPrestaties.Any())
                {
                    var previousPrestaties = ctx.StukPrestaties.Where(p => p.Prestaties.KlantenNummer == SelectedKlant.ID);

                    if (previousPrestaties.Any())
                    {
                        //Get the id of the latest stukprestatie
                        var tempId = previousPrestaties.OrderByDescending(p => p.Id).First().Id;
                        //Get the prestatie with the same id of the latest stukprestatie
                        AddPrestatie = ctx.Prestaties.First(p => p.Id == tempId);
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
                    messageBoxService.ShowMessageBox("Er bevinden zich nog geen prestaties in de databank, u kunt niets wijzigen");
                    return;
                }

                //Thinking reverse => The current Tegoed of the Klant was the NiewTegoed of the last prestatie
                AddPrestatie.NieuwTegoed = SelectedKlant.Tegoed;

                if (AddPrestatie.TotaalDienstenChecks > 0)
                {
                    AddPrestatie.TotaalBetalen = (AddPrestatie.TotaalDienstenChecks * 60) - AddPrestatie.NieuwTegoed;
                    SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.StukPrestaties.Totaal - AddPrestatie.TotaalBetalen);
                }
                else
                {
                    AddPrestatie.TotaalBetalen = 0;
                    SelectedKlant.Tegoed = Convert.ToByte(AddPrestatie.StukPrestaties.Totaal + AddPrestatie.NieuwTegoed);
                }

                ButtonBerekenContent = "Herbereken";
                ButtonToevoegenContent = "Aanpassen";
                ButtonChangeContent = "Annuleren";
            }
            else
            {
                ButtonBerekenContent = "Bereken";
                ButtonToevoegenContent = "Toevoegen";
                ButtonChangeContent = "Laatste prestatie aanpassen";

                ctx.Entry(AddPrestatie).Reload();
                ctx.Entry(SelectedKlant).Reload();

                AddPrestatie = new Prestatie();
                AddPrestatie.StukPrestaties = new StukPrestatie();
            }
        }
    }
}
