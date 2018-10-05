namespace TfsNodeImportExport
{
    partial class MainForm
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tlstrpProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.tlstrpLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.grpbxConnectionDetails = new System.Windows.Forms.GroupBox();
			this.txtbxTfsSelectedProject = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTfsCollectionUrl = new System.Windows.Forms.TextBox();
			this.btnSelectTfsProject = new System.Windows.Forms.Button();
			this.lblTfsCollectionUrl = new System.Windows.Forms.Label();
			this.grpbxStatus = new System.Windows.Forms.GroupBox();
			this.txtbxProgress = new System.Windows.Forms.TextBox();
			this.grpbxActions = new System.Windows.Forms.GroupBox();
			this.rbtnTypeIterations = new System.Windows.Forms.RadioButton();
			this.rbtnTypeAreaPaths = new System.Windows.Forms.RadioButton();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnImport = new System.Windows.Forms.Button();
			this.ofdlgImport = new System.Windows.Forms.OpenFileDialog();
			this.sfdlgExport = new System.Windows.Forms.SaveFileDialog();
			this.statusStrip1.SuspendLayout();
			this.grpbxConnectionDetails.SuspendLayout();
			this.grpbxStatus.SuspendLayout();
			this.grpbxActions.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrpProgress,
            this.tlstrpLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 323);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(531, 22);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
			// 
			// tlstrpProgress
			// 
			this.tlstrpProgress.Name = "tlstrpProgress";
			this.tlstrpProgress.Size = new System.Drawing.Size(100, 16);
			this.tlstrpProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.tlstrpProgress.Click += new System.EventHandler(this.tlstrpProgress_Click);
			// 
			// tlstrpLabel
			// 
			this.tlstrpLabel.Name = "tlstrpLabel";
			this.tlstrpLabel.Size = new System.Drawing.Size(39, 17);
			this.tlstrpLabel.Text = "Ready";
			this.tlstrpLabel.Click += new System.EventHandler(this.tlstrpLabel_Click);
			// 
			// grpbxConnectionDetails
			// 
			this.grpbxConnectionDetails.Controls.Add(this.txtbxTfsSelectedProject);
			this.grpbxConnectionDetails.Controls.Add(this.label1);
			this.grpbxConnectionDetails.Controls.Add(this.txtTfsCollectionUrl);
			this.grpbxConnectionDetails.Controls.Add(this.btnSelectTfsProject);
			this.grpbxConnectionDetails.Controls.Add(this.lblTfsCollectionUrl);
			this.grpbxConnectionDetails.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpbxConnectionDetails.Location = new System.Drawing.Point(0, 0);
			this.grpbxConnectionDetails.Name = "grpbxConnectionDetails";
			this.grpbxConnectionDetails.Size = new System.Drawing.Size(531, 73);
			this.grpbxConnectionDetails.TabIndex = 8;
			this.grpbxConnectionDetails.TabStop = false;
			this.grpbxConnectionDetails.Text = "Connection Details";
			this.grpbxConnectionDetails.Enter += new System.EventHandler(this.grpbxConnectionDetails_Enter);
			// 
			// txtbxTfsSelectedProject
			// 
			this.txtbxTfsSelectedProject.Location = new System.Drawing.Point(126, 42);
			this.txtbxTfsSelectedProject.Name = "txtbxTfsSelectedProject";
			this.txtbxTfsSelectedProject.ReadOnly = true;
			this.txtbxTfsSelectedProject.Size = new System.Drawing.Size(280, 20);
			this.txtbxTfsSelectedProject.TabIndex = 11;
			this.txtbxTfsSelectedProject.TextChanged += new System.EventHandler(this.txtbxTfsSelectedProject_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Tfs Selected Project";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// txtTfsCollectionUrl
			// 
			this.txtTfsCollectionUrl.Location = new System.Drawing.Point(126, 16);
			this.txtTfsCollectionUrl.Name = "txtTfsCollectionUrl";
			this.txtTfsCollectionUrl.ReadOnly = true;
			this.txtTfsCollectionUrl.Size = new System.Drawing.Size(280, 20);
			this.txtTfsCollectionUrl.TabIndex = 9;
			this.txtTfsCollectionUrl.TextChanged += new System.EventHandler(this.txtTfsCollectionUrl_TextChanged);
			// 
			// btnSelectTfsProject
			// 
			this.btnSelectTfsProject.Location = new System.Drawing.Point(412, 16);
			this.btnSelectTfsProject.Name = "btnSelectTfsProject";
			this.btnSelectTfsProject.Size = new System.Drawing.Size(100, 23);
			this.btnSelectTfsProject.TabIndex = 8;
			this.btnSelectTfsProject.Text = "Select Project";
			this.btnSelectTfsProject.UseVisualStyleBackColor = true;
			this.btnSelectTfsProject.Click += new System.EventHandler(this.btnSelectTfsProject_Click);
			// 
			// lblTfsCollectionUrl
			// 
			this.lblTfsCollectionUrl.AutoSize = true;
			this.lblTfsCollectionUrl.Location = new System.Drawing.Point(28, 23);
			this.lblTfsCollectionUrl.Name = "lblTfsCollectionUrl";
			this.lblTfsCollectionUrl.Size = new System.Drawing.Size(92, 13);
			this.lblTfsCollectionUrl.TabIndex = 7;
			this.lblTfsCollectionUrl.Text = "TFS Collection Url";
			this.lblTfsCollectionUrl.Click += new System.EventHandler(this.lblTfsCollectionUrl_Click);
			// 
			// grpbxStatus
			// 
			this.grpbxStatus.Controls.Add(this.txtbxProgress);
			this.grpbxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpbxStatus.Location = new System.Drawing.Point(0, 120);
			this.grpbxStatus.Name = "grpbxStatus";
			this.grpbxStatus.Size = new System.Drawing.Size(531, 203);
			this.grpbxStatus.TabIndex = 9;
			this.grpbxStatus.TabStop = false;
			this.grpbxStatus.Text = "Progress";
			this.grpbxStatus.Enter += new System.EventHandler(this.grpbxStatus_Enter);
			// 
			// txtbxProgress
			// 
			this.txtbxProgress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbxProgress.Location = new System.Drawing.Point(3, 16);
			this.txtbxProgress.Multiline = true;
			this.txtbxProgress.Name = "txtbxProgress";
			this.txtbxProgress.ReadOnly = true;
			this.txtbxProgress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtbxProgress.Size = new System.Drawing.Size(525, 184);
			this.txtbxProgress.TabIndex = 0;
			this.txtbxProgress.WordWrap = false;
			this.txtbxProgress.TextChanged += new System.EventHandler(this.txtbxProgress_TextChanged);
			// 
			// grpbxActions
			// 
			this.grpbxActions.Controls.Add(this.rbtnTypeIterations);
			this.grpbxActions.Controls.Add(this.rbtnTypeAreaPaths);
			this.grpbxActions.Controls.Add(this.btnExport);
			this.grpbxActions.Controls.Add(this.btnImport);
			this.grpbxActions.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpbxActions.Location = new System.Drawing.Point(0, 73);
			this.grpbxActions.Name = "grpbxActions";
			this.grpbxActions.Size = new System.Drawing.Size(531, 47);
			this.grpbxActions.TabIndex = 10;
			this.grpbxActions.TabStop = false;
			this.grpbxActions.Text = "Actions";
			this.grpbxActions.Enter += new System.EventHandler(this.grpbxActions_Enter);
			// 
			// rbtnTypeIterations
			// 
			this.rbtnTypeIterations.AutoSize = true;
			this.rbtnTypeIterations.Location = new System.Drawing.Point(149, 19);
			this.rbtnTypeIterations.Name = "rbtnTypeIterations";
			this.rbtnTypeIterations.Size = new System.Drawing.Size(68, 17);
			this.rbtnTypeIterations.TabIndex = 1;
			this.rbtnTypeIterations.Text = "Iterations";
			this.rbtnTypeIterations.UseVisualStyleBackColor = true;
			this.rbtnTypeIterations.CheckedChanged += new System.EventHandler(this.rbtnTypeIterations_CheckedChanged);
			// 
			// rbtnTypeAreaPaths
			// 
			this.rbtnTypeAreaPaths.AutoSize = true;
			this.rbtnTypeAreaPaths.Checked = true;
			this.rbtnTypeAreaPaths.Location = new System.Drawing.Point(55, 20);
			this.rbtnTypeAreaPaths.Name = "rbtnTypeAreaPaths";
			this.rbtnTypeAreaPaths.Size = new System.Drawing.Size(77, 17);
			this.rbtnTypeAreaPaths.TabIndex = 1;
			this.rbtnTypeAreaPaths.TabStop = true;
			this.rbtnTypeAreaPaths.Text = "Area Paths";
			this.rbtnTypeAreaPaths.UseVisualStyleBackColor = true;
			this.rbtnTypeAreaPaths.CheckedChanged += new System.EventHandler(this.rbtnTypeAreaPaths_CheckedChanged);
			// 
			// btnExport
			// 
			this.btnExport.Enabled = false;
			this.btnExport.Location = new System.Drawing.Point(437, 14);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(75, 23);
			this.btnExport.TabIndex = 0;
			this.btnExport.Text = "Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnImport
			// 
			this.btnImport.Enabled = false;
			this.btnImport.Location = new System.Drawing.Point(356, 14);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(75, 23);
			this.btnImport.TabIndex = 0;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// ofdlgImport
			// 
			this.ofdlgImport.DefaultExt = "*.json";
			this.ofdlgImport.FileName = "TfsNode.json";
			this.ofdlgImport.Filter = "All Files|*.*";
			this.ofdlgImport.RestoreDirectory = true;
			this.ofdlgImport.Title = "Select File To Import";
			this.ofdlgImport.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdlgImport_FileOk);
			// 
			// sfdlgExport
			// 
			this.sfdlgExport.DefaultExt = "*.json";
			this.sfdlgExport.FileName = "TfsNodes.json";
			this.sfdlgExport.Filter = "All Files|*.*";
			this.sfdlgExport.RestoreDirectory = true;
			this.sfdlgExport.Title = "Enter a File to Export Tfs Area Paths too";
			this.sfdlgExport.FileOk += new System.ComponentModel.CancelEventHandler(this.sfdlgExport_FileOk);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(531, 345);
			this.Controls.Add(this.grpbxStatus);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.grpbxActions);
			this.Controls.Add(this.grpbxConnectionDetails);
			this.Name = "MainForm";
			this.Text = "Tfs Area Path and Iterations Import/Export Tool";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.grpbxConnectionDetails.ResumeLayout(false);
			this.grpbxConnectionDetails.PerformLayout();
			this.grpbxStatus.ResumeLayout(false);
			this.grpbxStatus.PerformLayout();
			this.grpbxActions.ResumeLayout(false);
			this.grpbxActions.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox grpbxConnectionDetails;
        private System.Windows.Forms.TextBox txtbxTfsSelectedProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTfsCollectionUrl;
        private System.Windows.Forms.Button btnSelectTfsProject;
        private System.Windows.Forms.Label lblTfsCollectionUrl;
        private System.Windows.Forms.GroupBox grpbxStatus;
        private System.Windows.Forms.TextBox txtbxProgress;
        private System.Windows.Forms.GroupBox grpbxActions;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ToolStripProgressBar tlstrpProgress;
        private System.Windows.Forms.ToolStripStatusLabel tlstrpLabel;
        private System.Windows.Forms.OpenFileDialog ofdlgImport;
        private System.Windows.Forms.SaveFileDialog sfdlgExport;
        private System.Windows.Forms.RadioButton rbtnTypeIterations;
        private System.Windows.Forms.RadioButton rbtnTypeAreaPaths;
    }
}

