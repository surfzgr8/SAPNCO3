namespace Tester
{
    partial class frmMain
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
            this.openInputFile = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.txtIDoc = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.chkDisplayInput = new System.Windows.Forms.CheckBox();
            this.chkDisplayOutput = new System.Windows.Forms.CheckBox();
            this.btnRxIDoc = new System.Windows.Forms.Button();
            this.btnRgsterDeastination = new System.Windows.Forms.Button();
            this.btnStopRx = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openInputFile
            // 
            this.openInputFile.InitialDirectory = "c:\\";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.webBrowser1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtIDoc);
            this.splitContainer1.Size = new System.Drawing.Size(999, 593);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(327, 587);
            this.webBrowser1.TabIndex = 0;
            // 
            // txtIDoc
            // 
            this.txtIDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIDoc.Location = new System.Drawing.Point(3, 3);
            this.txtIDoc.Multiline = true;
            this.txtIDoc.Name = "txtIDoc";
            this.txtIDoc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtIDoc.Size = new System.Drawing.Size(656, 587);
            this.txtIDoc.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 653);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1023, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // chkDisplayInput
            // 
            this.chkDisplayInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDisplayInput.AutoSize = true;
            this.chkDisplayInput.Checked = true;
            this.chkDisplayInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayInput.Location = new System.Drawing.Point(12, 608);
            this.chkDisplayInput.Name = "chkDisplayInput";
            this.chkDisplayInput.Size = new System.Drawing.Size(60, 17);
            this.chkDisplayInput.TabIndex = 4;
            this.chkDisplayInput.Text = "Display";
            this.chkDisplayInput.UseVisualStyleBackColor = true;
            // 
            // chkDisplayOutput
            // 
            this.chkDisplayOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDisplayOutput.AutoSize = true;
            this.chkDisplayOutput.Checked = true;
            this.chkDisplayOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayOutput.Location = new System.Drawing.Point(352, 608);
            this.chkDisplayOutput.Name = "chkDisplayOutput";
            this.chkDisplayOutput.Size = new System.Drawing.Size(60, 17);
            this.chkDisplayOutput.TabIndex = 5;
            this.chkDisplayOutput.Text = "Display";
            this.chkDisplayOutput.UseVisualStyleBackColor = true;
            // 
            // btnRxIDoc
            // 
            this.btnRxIDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRxIDoc.Location = new System.Drawing.Point(582, 623);
            this.btnRxIDoc.Name = "btnRxIDoc";
            this.btnRxIDoc.Size = new System.Drawing.Size(104, 23);
            this.btnRxIDoc.TabIndex = 6;
            this.btnRxIDoc.Text = "Start Receive";
            this.btnRxIDoc.UseVisualStyleBackColor = true;
            this.btnRxIDoc.Click += new System.EventHandler(this.btnRxIDoc_Click);
            // 
            // btnRgsterDeastination
            // 
            this.btnRgsterDeastination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRgsterDeastination.Location = new System.Drawing.Point(453, 623);
            this.btnRgsterDeastination.Name = "btnRgsterDeastination";
            this.btnRgsterDeastination.Size = new System.Drawing.Size(123, 23);
            this.btnRgsterDeastination.TabIndex = 7;
            this.btnRgsterDeastination.Text = "Register Destination";
            this.btnRgsterDeastination.UseVisualStyleBackColor = true;
            this.btnRgsterDeastination.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnStopRx
            // 
            this.btnStopRx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopRx.Location = new System.Drawing.Point(826, 623);
            this.btnStopRx.Name = "btnStopRx";
            this.btnStopRx.Size = new System.Drawing.Size(94, 23);
            this.btnStopRx.TabIndex = 8;
            this.btnStopRx.Text = "Stop Receive";
            this.btnStopRx.UseVisualStyleBackColor = true;
            this.btnStopRx.Click += new System.EventHandler(this.btnStopRx_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(936, 623);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(704, 622);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(104, 23);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit IDoc";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btsServer2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1023, 675);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStopRx);
            this.Controls.Add(this.btnRgsterDeastination);
            this.Controls.Add(this.btnRxIDoc);
            this.Controls.Add(this.chkDisplayOutput);
            this.Controls.Add(this.chkDisplayInput);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAP IDoc Viewer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openInputFile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.CheckBox chkDisplayInput;
        private System.Windows.Forms.CheckBox chkDisplayOutput;
        private System.Windows.Forms.Button btnRxIDoc;
        private System.Windows.Forms.Button btnRgsterDeastination;
        private System.Windows.Forms.TextBox txtIDoc;
        private System.Windows.Forms.Button btnStopRx;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSubmit;

    }
}

