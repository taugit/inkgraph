using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InkGraph
{
    class CustomToolTip : ToolTip
    {
        public string _text;
        private Font _font;
        private int _xOffset = 10;
        private int _yOffset = 10;

        public CustomToolTip()
        {
            this.OwnerDraw = true;
            this.Popup += new PopupEventHandler(OnPopup);
            this.Draw += new DrawToolTipEventHandler(OnDraw);
            _font = new Font("Segoe UI", Properties.Settings.Default.ToolTipFontSize, FontStyle.Bold);
        }

        private void OnPopup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = TextRenderer.MeasureText(_text, _font);
            e.ToolTipSize = new Size(e.ToolTipSize.Width + _xOffset, e.ToolTipSize.Height + _yOffset);
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            Graphics g = e.Graphics;

            //LinearGradientBrush b = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.MintCream, 45f);
            LinearGradientBrush b = new LinearGradientBrush(e.Bounds, Color.FromArgb(170, 219, 238), Color.FromArgb(170, 219, 238), 45f); ;

            g.FillRectangle(b, e.Bounds);

            g.DrawRectangle(new Pen(Brushes.Gray, 1), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));

            // shadow layer
            g.DrawString(e.ToolTipText, _font, Brushes.Silver, new PointF(e.Bounds.X + 6, e.Bounds.Y + 6));
            // top layer
            g.DrawString(e.ToolTipText, _font, Brushes.Black, new PointF(e.Bounds.X + 5, e.Bounds.Y + 5)); 

            b.Dispose();
        }
    }
}
