using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;

namespace InkGraph
{
    public partial class FormMain : Form
    {
        private static Microsoft.Msagl.GraphViewerGdi.GViewer _viewer;
        private static Graph _graph;

        public static readonly Color kPurple = new Color(224, 210, 227);
        public static readonly Color kGreen = new Color(166, 223, 157);
        public static readonly Color kOrange = new Color(255, 196, 71);
        public static readonly Color kBlue = new Color(122, 179, 255);
        public static readonly Color kTeal = new Color(74, 219, 206);
        public static readonly Color kGrey = new Color(200, 200, 200);
        public static readonly Color kLightGrey = new Color(230, 230, 230);

        private static string startingScene = string.Empty;


        private static Node _selectedNode = null;

        private static CustomToolTip _tTip = null;


        private static bool isPanning = false;
        private static int mouseDownX;
        private static int mouseDownY;
        private static PlaneTransformation mouseDownTransform;

        #region Init
        public FormMain()
        {
            InitializeComponent();

            InitMSAGL();
        }

        private void InitMSAGL()
        {
            _viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            _viewer.ToolBarIsVisible = false;

            //create a graph object
            _graph = new Graph();

            _viewer.MouseMove += ViewerMouseMove;
            _viewer.MouseUp += ViewerMouseUp;
            _viewer.MouseDown += ViewerMouseDown;
            _viewer.ObjectUnderMouseCursorChanged += ViewerMouseUnderCusrorChange;
            _viewer.MouseClick += ViewerMouseClick;

            // Distance between connected nodes
            _graph.LayoutAlgorithmSettings.NodeSeparation = Properties.Settings.Default.NodeSeparation;


            _tTip = new CustomToolTip();
            _tTip.Active = false;
            _tTip.AutoPopDelay = 30000;
            _tTip.InitialDelay = 1;
            _tTip.ReshowDelay = 1;


            SuspendLayout();
            _viewer.Graph = _graph;
            _viewer.Dock = DockStyle.Fill;
            Controls.Add(_viewer);
            ResumeLayout();
        }


        #endregion

        #region Mouse


        private void ViewerMouseClick(object sender, MouseEventArgs e)
        {
            var o = _viewer.SelectedObject;

            if (o != null && o is Node)
            {
                var curNode = o as Node;

                if (_selectedNode != null)
                {
                    foreach (var edge in _selectedNode.Edges)
                    {
                        edge.Attr.Color = Color.LightSlateGray;
                        edge.Attr.LineWidth = Properties.Settings.Default.DefaultEdgeLineWidth;
                        if (edge.SourceNode != null)
                        {
                            edge.SourceNode.Attr.Color = Utils.ConvertToMsaglColor(Properties.Settings.Default.DefaultNodeColor);
                            edge.SourceNode.Attr.LineWidth = Properties.Settings.Default.DefaultNodeLineWidth;
                        }
                        if (edge.TargetNode != null)
                        {
                            edge.TargetNode.Attr.Color = Utils.ConvertToMsaglColor(Properties.Settings.Default.DefaultNodeColor);
                            edge.TargetNode.Attr.LineWidth = Properties.Settings.Default.DefaultNodeLineWidth;
                        }
                    }
                }

                curNode.Attr.LineWidth = Properties.Settings.Default.HighlightedNodeLineWidth;

                foreach (var edge in curNode.Edges)
                {
                    Color newLineColor;
                    if (edge.SourceNode == curNode)
                        // Color for outgoing edges
                        newLineColor = Utils.ConvertToMsaglColor(Properties.Settings.Default.HighlightedOutEdgeColor);
                    else
                        // Color for incoming edges
                        newLineColor = Utils.ConvertToMsaglColor(Properties.Settings.Default.HighlightedInEdgeColor);

                    edge.Attr.Color = newLineColor;
                    edge.Attr.LineWidth = Properties.Settings.Default.HighlightedEdgeLineWidth;

                    if (edge.TargetNode != null && edge.TargetNode != curNode)
                    {
                        edge.TargetNode.Attr.Color = Utils.ConvertToMsaglColor(Properties.Settings.Default.HighlightedOutNodeColor);
                        edge.TargetNode.Attr.LineWidth = Properties.Settings.Default.HighlightedEdgeLineWidth;
                    }

                    if (edge.SourceNode != null && edge.SourceNode != curNode)
                    {
                        edge.SourceNode.Attr.Color = Utils.ConvertToMsaglColor(Properties.Settings.Default.HighlightedInNodeColor);
                        edge.SourceNode.Attr.LineWidth = Properties.Settings.Default.HighlightedEdgeLineWidth;
                    }
                }

                _viewer.Invalidate();
                _selectedNode = curNode;
                var cd = (CustomData)(curNode.UserData);
                if (cd != null)
                {
                    rtbDetails.Text = cd.Text;
                    return;
                }
            }
            else
            {
                if (_selectedNode != null)
                {
                    foreach (var edge in _graph.Edges)
                    {
                        edge.Attr.Color = Utils.ConvertToMsaglColor(Properties.Settings.Default.DefaultEdgeColor);
                        edge.Attr.LineWidth = Properties.Settings.Default.DefaultEdgeLineWidth;
                        if (edge.SourceNode != null)
                        {
                            edge.SourceNode.Attr.Color = Utils.ConvertToMsaglColor(Properties.Settings.Default.DefaultNodeColor);
                            edge.SourceNode.Attr.LineWidth = Properties.Settings.Default.DefaultNodeLineWidth;
                        }
                        if (edge.TargetNode != null)
                        {
                            edge.TargetNode.Attr.Color = Utils.ConvertToMsaglColor(Properties.Settings.Default.DefaultNodeColor);
                            edge.SourceNode.Attr.LineWidth = Properties.Settings.Default.DefaultNodeLineWidth;
                        }
                    }
                    _selectedNode = null;
                    _viewer.Invalidate();
                }
                rtbDetails.Text = string.Empty;
            }



        }

        private void ViewerMouseMove(object sender, MouseEventArgs e)
        {
            // If holding left mouse button and no objects are selected
            // Pan the image
            if (isPanning && _viewer.SelectedObject == null)
            {
                if (mouseDownTransform != null)
                {
                    _viewer.Transform[0, 2] = mouseDownTransform[0, 2] + e.X - mouseDownX;
                    _viewer.Transform[1, 2] = mouseDownTransform[1, 2] + e.Y - mouseDownY;
                }
                _viewer.Invalidate();

            }
        }

        private void ViewerMouseDown(object sender, MouseEventArgs e)
        {
            // Start panning
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isPanning = true;
                mouseDownX = e.X;
                mouseDownY = e.Y;
                mouseDownTransform = _viewer.Transform.Clone();
            }
        }

        private void ViewerMouseUp(object sender, MouseEventArgs e)
        {
            // Stop panning
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                isPanning = false;
        }

        private void ViewerMouseUnderCusrorChange(object sender, ObjectUnderMouseCursorChangedEventArgs e)
        {
            if (!Properties.Settings.Default.ShowTooltips)
                return;

            var selectedObject = e.NewObject != null ? e.NewObject.DrawingObject : null;
            if (selectedObject == e.OldObject)
                return;

            if (selectedObject != null && (selectedObject is Edge || selectedObject is Node))
            {
                var cData = (CustomData)selectedObject.UserData;
                if (cData == null)
                {
                    _tTip.Active = false;
                }
                else
                {
                    _tTip._text = string.Join(Environment.NewLine, cData.Actions);
                    _tTip.Active = true;
                    _viewer.SetToolTip(_tTip, string.Join(Environment.NewLine, cData.Actions));
                }
            }
            else
            {
                _tTip.Active = false;
            }

        }
        #endregion

        private void BuildGraph(string[] content)
        {
            _graph = new Graph();
            var curScene = string.Empty;
            var sceneReg = new Regex(@"\=\=\=\s*(?<scene>\w+)\s*\=\=\=");
            var dialogueDivertReg = new Regex(@"(\*|\+)\s*(?<desc>.*)\s*->\s*(?<scene>\w+)");
            var absDivertReg = new Regex(@"^->\s*(?<scene>\w+)\s*$");
            var condReg = new Regex(@"{(?<cond>:)");
            var funcReg = new Regex(@"^~\s*(?<func>.*)$");

            var rawText = new List<string>();

            foreach (var s in content)
            {
                var m = sceneReg.Match(s);
                if (m.Success)
                {
                    if (curScene != string.Empty)
                    {
                        CustomData.SetNodeText(_graph.FindNode(curScene), string.Join(Environment.NewLine, rawText));
                        rawText = new List<string>();
                    }
                    else
                    {
                        startingScene = m.Groups["scene"].Value;
                    }
                    rawText.Add(s);
                    curScene = m.Groups["scene"].Value;

                    _graph.AddNode(curScene);
                    continue;
                }
                rawText.Add(s);


                m = dialogueDivertReg.Match(s);
                if (m.Success)
                {
                    if (curScene != string.Empty)
                        _graph.AddEdge(curScene, m.Groups["scene"].Value);
                    continue;
                }

                m = absDivertReg.Match(s);
                if (m.Success)
                {
                    if (curScene != string.Empty)
                        _graph.AddEdge(curScene, m.Groups["scene"].Value);
                    continue;
                }

                m = funcReg.Match(s);
                if (m.Success)
                {
                    if (curScene != string.Empty)
                    {
                        var n = _graph.FindNode(curScene);
                        var cData = (CustomData)n.UserData;
                        if (cData == null)
                            cData = new CustomData();
                        cData.Actions.Add(s);
                        n.UserData = cData;
                    }
                    continue;
                }
            }
            PaintGraph();
            _viewer.Graph = _graph;
        }

        private void PaintGraph()
        {
            // Distance between connected nodes
            _graph.LayoutAlgorithmSettings.NodeSeparation = Properties.Settings.Default.NodeSeparation;

            foreach (var n in _graph.Nodes)
            {
                n.Label.FontSize = Properties.Settings.Default.NodeFontSize;

                // Margin between label and box
                n.Attr.LabelMargin = 5;
                // Distance between arrow and node
                n.Attr.Padding = 10;
                n.Attr.AddStyle(Style.Bold);

                var inC = n.InEdges.ToList().Count;
                var outC = n.OutEdges.ToList().Count;
                if (inC == 0 && outC == 0)
                {
                    // orphan node
                    n.Attr.FillColor = Utils.ConvertToMsaglColor(Properties.Settings.Default.OrphanNodeFill); ;
                }
                else if (inC == 0 && outC > 0)
                {
                    // start node
                    n.Attr.FillColor = Utils.ConvertToMsaglColor(Properties.Settings.Default.StartNodeFill);
                }
                else if (inC > 0 && outC == 0)
                {
                    // end node
                    n.Attr.FillColor = Utils.ConvertToMsaglColor(Properties.Settings.Default.EndNodeFill); ;
                }
            }

            foreach (var e in _graph.Edges)
            {
                e.Attr.Color = Utils.ConvertToMsaglColor(Properties.Settings.Default.DefaultEdgeColor);
                e.Attr.LineWidth = Properties.Settings.Default.DefaultEdgeLineWidth;
                e.Attr.ArrowheadLength = Properties.Settings.Default.ArrowheadLength;
                e.Attr.Separation = Properties.Settings.Default.EdgeSeparation;
            }

            _viewer.Graph = _graph;
            _viewer.Invalidate();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (var d = new OpenFileDialog())
            {
                d.Filter = "Ink file (*.ink)|*.ink";
                d.RestoreDirectory = true;

                if (d.ShowDialog() == DialogResult.OK)
                    BuildGraph(File.ReadAllLines(d.FileName));
            }
        }

        private static void RedoLayout()
        {
            _viewer.NeedToCalculateLayout = true;
            _viewer.Graph = _graph;
            _viewer.NeedToCalculateLayout = false;
            _viewer.Graph = _graph;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panelLegend.Width = (int)(Width * 0.2);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (_graph == null || _viewer == null || _viewer.Entities.Count() == 0)
            {
                rtbDetails.Text = "INK file is not loaded";
                return;
            }

            var sb = new StringBuilder();

            foreach (var n in _graph.Nodes)
            {
                if (n.Edges.Count() == 0)
                {
                    sb.AppendLine(string.Format("{0}: orphan knot", n.LabelText));
                    continue;
                }

                if (n.InEdges.Count() == 0 && n.LabelText != startingScene)
                {
                    sb.AppendLine(string.Format("{0}: unexpected quest start", n.LabelText));
                }

                foreach (var edge in n.Edges)
                {
                    if (edge.SourceNode == edge.TargetNode)
                    {
                        sb.AppendLine(string.Format("{0}: knot diverts to itself", n.LabelText));
                    }
                }
            }
            if (sb.Length == 0)
                sb.Append("No issues found");
            rtbDetails.Text = sb.ToString();
        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            var sForm = new FormSettings();
            if (sForm.ShowDialog() == DialogResult.OK)
            {
                PaintGraph();
            }
        }
    }
}
