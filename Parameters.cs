using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIS
{
    class Parameters
    {
        public Parameters()
        {
            ParameterHemden = Properties.Settings.Default.ParameterHemden;
            ParameterLakens1 = Properties.Settings.Default.ParameterLakens1;
            ParameterLakens2 = Properties.Settings.Default.ParameterLakens2;
            ParameterAndereStrijk = Properties.Settings.Default.ParameterAndereStrijk;
        } 

        public byte ParameterHemden { get; set; }
        public byte ParameterLakens1 { get; set; }
        public byte ParameterLakens2 { get; set; }
        public byte ParameterAndereStrijk { get; set; }
    }
}
