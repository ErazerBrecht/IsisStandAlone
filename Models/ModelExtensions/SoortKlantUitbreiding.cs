using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ISIS
{
    public partial class SoortKlant
    {
        public override string ToString()
        {
            return this.Naam;
        }
    }
}
