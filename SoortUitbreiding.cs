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
    public partial class Bedrijf
    {
        public override string ToString()
        {
            return this.Naam;
        }
    }

    public partial class Winkel
    {
        public override string ToString()
        {
            return this.Naam;
        }
    }

    public partial class Ophaling
    {
        public override string ToString()
        {
            return this.Naam;
        }
    }

    public partial class School
    {
        public override string ToString()
        {
            return this.Naam;
        }
    }
}
