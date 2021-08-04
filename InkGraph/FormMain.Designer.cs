
namespace InkGraph
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpenInk = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCheck = new System.Windows.Forms.ToolStripButton();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.rtbDetails = new System.Windows.Forms.RichTextBox();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.panelLegend.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenInk,
            this.toolStripButtonCheck,
            this.toolStripButtonSettings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(790, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonOpenInk
            // 
            this.toolStripButtonOpenInk.Image = global::InkGraph.Properties.Resources.open_file;
            this.toolStripButtonOpenInk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenInk.Name = "toolStripButtonOpenInk";
            this.toolStripButtonOpenInk.Size = new System.Drawing.Size(95, 36);
            this.toolStripButtonOpenInk.Text = "Open .ink";
            this.toolStripButtonOpenInk.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButtonCheck
            // 
            this.toolStripButtonCheck.Image = global::InkGraph.Properties.Resources.validate;
            this.toolStripButtonCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCheck.Name = "toolStripButtonCheck";
            this.toolStripButtonCheck.Size = new System.Drawing.Size(72, 36);
            this.toolStripButtonCheck.Text = "Check";
            this.toolStripButtonCheck.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // panelLegend
            // 
            this.panelLegend.Controls.Add(this.rtbDetails);
            this.panelLegend.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLegend.Location = new System.Drawing.Point(0, 49);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(81919, 405);
            this.panelLegend.TabIndex = 1;
            // 
            // rtbDetails
            // 
            this.rtbDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.rtbDetails.Location = new System.Drawing.Point(0, 0);
            this.rtbDetails.Name = "rtbDetails";
            this.rtbDetails.ReadOnly = true;
            this.rtbDetails.Size = new System.Drawing.Size(65535, 405);
            this.rtbDetails.TabIndex = 2;
            this.rtbDetails.Text = "";
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.Image = global::InkGraph.Properties.Resources.settings;
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(86, 36);
            this.toolStripButtonSettings.Text = "Settings";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 363);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.Text = "InkGraph";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelLegend.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenInk;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.RichTextBox rtbDetails;
        private System.Windows.Forms.ToolStripButton toolStripButtonCheck;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
    }
}

