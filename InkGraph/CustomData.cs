using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkGraph
{
    class CustomData
    {
        public List<string> Actions = new List<string>();

        public string Text = string.Empty;


        public static void AddNodeActions(Node n, string s)
        {
            if (n != null)
            {
                var cd = (CustomData)n.UserData;
                if (cd == null)
                    cd = new CustomData();
                cd.Actions.Add(s);
                n.UserData = cd;
            }
        }

        public static void SetNodeText(Node n, string s)
        {
            if (n != null)
            {
                var cd = (CustomData)n.UserData;
                if (cd == null)
                    cd = new CustomData();
                cd.Text = s;
                n.UserData = cd;
            }
        }

    }
}
