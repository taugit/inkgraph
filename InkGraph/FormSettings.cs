using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InkGraph
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            pStartNodeFillColor.BackColor = Properties.Settings.Default.StartNodeFill;
            pEndNodeFillColor.BackColor = Properties.Settings.Default.EndNodeFill;
            pOrphanNodeFillColor.BackColor = Properties.Settings.Default.OrphanNodeFill;
            pDefaultEdgeColor.BackColor = Properties.Settings.Default.DefaultEdgeColor;
            pDefaultNodeColor.BackColor = Properties.Settings.Default.DefaultNodeColor;
            pHighlightedInEdgeColor.BackColor = Properties.Settings.Default.HighlightedInEdgeColor;
            pHighlightedOutEdgeColor.BackColor = Properties.Settings.Default.HighlightedOutEdgeColor;
            pHighlightedInNodeColor.BackColor = Properties.Settings.Default.HighlightedInNodeColor;
            pHighlightedOutNodeColor.BackColor = Properties.Settings.Default.HighlightedOutNodeColor;
            nUDDefaultNodeLineWidth.Value = Properties.Settings.Default.DefaultNodeLineWidth;
            nUDNodeFontSize.Value = Properties.Settings.Default.NodeFontSize;
            nUDToolTipFontSize.Value = Properties.Settings.Default.ToolTipFontSize;
            nUDNodeSeparation.Value = Properties.Settings.Default.NodeSeparation;
            nUDDefaultEdgeLineWidth.Value = Properties.Settings.Default.DefaultEdgeLineWidth;
            nUDArrowheadLength.Value = Properties.Settings.Default.ArrowheadLength;
            nUDHighlightedNodeLineWidth.Value = Properties.Settings.Default.HighlightedNodeLineWidth;
            nUDHighlightedEdgeLineWidth.Value = Properties.Settings.Default.HighlightedEdgeLineWidth;
            nUDEdgeSeparation.Value = Properties.Settings.Default.EdgeSeparation;
        }

        private Color ShowColorPicker(Color initColor)
        {
            var colorDialog = new ColorDialog();
            colorDialog.Color = initColor;
            colorDialog.ShowDialog();
            return colorDialog.Color;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.StartNodeFill = pStartNodeFillColor.BackColor;
            Properties.Settings.Default.EndNodeFill = pEndNodeFillColor.BackColor;
            Properties.Settings.Default.OrphanNodeFill = pOrphanNodeFillColor.BackColor;
            Properties.Settings.Default.DefaultEdgeColor = pDefaultEdgeColor.BackColor;
            Properties.Settings.Default.DefaultNodeColor = pDefaultNodeColor.BackColor;
            Properties.Settings.Default.HighlightedInEdgeColor = pHighlightedInEdgeColor.BackColor;
            Properties.Settings.Default.HighlightedOutEdgeColor = pHighlightedOutEdgeColor.BackColor;
            Properties.Settings.Default.HighlightedInNodeColor = pHighlightedInNodeColor.BackColor;
            Properties.Settings.Default.HighlightedOutNodeColor = pHighlightedOutNodeColor.BackColor;
            Properties.Settings.Default.DefaultNodeLineWidth = (int)nUDDefaultNodeLineWidth.Value;
            Properties.Settings.Default.NodeFontSize = (int)nUDNodeFontSize.Value;
            Properties.Settings.Default.ToolTipFontSize = (int)nUDToolTipFontSize.Value;
            Properties.Settings.Default.NodeSeparation = (int)nUDNodeSeparation.Value;
            Properties.Settings.Default.DefaultEdgeLineWidth = (int)nUDDefaultEdgeLineWidth.Value;
            Properties.Settings.Default.ArrowheadLength = (int)nUDArrowheadLength.Value;
            Properties.Settings.Default.HighlightedNodeLineWidth = (int)nUDHighlightedNodeLineWidth.Value;
            Properties.Settings.Default.HighlightedEdgeLineWidth = (int)nUDHighlightedEdgeLineWidth.Value;
            Properties.Settings.Default.EdgeSeparation = (int)nUDEdgeSeparation.Value;

            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void pStartNodeFillColor_Click(object sender, EventArgs e)
        {
            var c = ShowColorPicker(Properties.Settings.Default.StartNodeFill);
            pStartNodeFillColor.BackColor = c;
        }

        private void pEndNodeFillColor_Click(object sender, EventArgs e)
        {
            var c = ShowColorPicker(Properties.Settings.Default.EndNodeFill);
            pEndNodeFillColor.BackColor = c;
        }

        private void pOrphanNodeFillColor_Click(object sender, EventArgs e)
        {
            var c = ShowColorPicker(Properties.Settings.Default.OrphanNodeFill);
            pOrphanNodeFillColor.BackColor = c;
        }

        private void pDefaultNodeColor_Click(object sender, EventArgs e)
        {
            var c = ShowColorPicker(Properties.Settings.Default.DefaultNodeColor);
            pDefaultNodeColor.BackColor = c;
        }

        private void pDefaultEdgeColor_Click(object sender, EventArgs e)
        {
            var c = ShowColorPicker(Properties.Settings.Default.DefaultEdgeColor);
            pDefaultEdgeColor.BackColor = c;
        }

        private void pHighlightedInNodeColor_Click(object sender, EventArgs e)
        {
            var c = ShowColorPicker(Properties.Settings.Default.HighlightedInNodeColor);
            pHighlightedInNodeColor.BackColor = c;
        }

        private void pHighlightedOutNodeColor_Click(object sender, EventArgs e)
        {
            var c = ShowColorPicker(Properties.Settings.Default.HighlightedOutNodeColor);
            pHighlightedOutNodeColor.BackColor = c;
        }

        private void pHighlightedInEdgeColor_Click(object sender, EventArgs e)
        {
            var c = ShowColorPicker(Properties.Settings.Default.HighlightedInEdgeColor);
            pHighlightedInEdgeColor.BackColor = c;
        }

        private void pHighlightedOutEdgeColor_Click(object sender, EventArgs e)
        {
            var c = ShowColorPicker(Properties.Settings.Default.HighlightedOutEdgeColor);
            pHighlightedOutEdgeColor.BackColor = c;
        }
    }
}
