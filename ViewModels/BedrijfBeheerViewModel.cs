using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class BedrijfBeheerViewModel : SoortKlantBeheerViewModel
    {
        public BedrijfBeheerViewModel()
        {
            AddSoort.Type = "Bedrijf";
        }

        public override void Refresh()
        {
            base.Refresh();
            ViewSource.Source = ctx.SoortKlant.Local.Where(s => s.Type == "Bedrijf").ToList();
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            AddSoort.Type = "Bedrijf";
        }
    }
}
