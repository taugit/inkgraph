using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkGraph
{
    static class Utils
    {
        public static Microsoft.Msagl.Drawing.Color ConvertToMsaglColor(System.Drawing.Color c)
        {
            return new Microsoft.Msagl.Drawing.Color(c.A, c.R, c.G, c.B);
        }
    }
}
