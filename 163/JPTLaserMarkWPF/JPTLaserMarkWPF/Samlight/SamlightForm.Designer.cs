namespace JPTLaserMarkWPF.Samlight
{
    partial class SamlightForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SamlightForm));
            this.SLCtrl = new AxSAMLIGHT_CLIENT_CTRL_OCXLib.AxScSamlightClientCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.SLCtrl)).BeginInit();
            this.SuspendLayout();
            // 
            // SLCtrl
            // 
            this.SLCtrl.Enabled = true;
            this.SLCtrl.Location = new System.Drawing.Point(11, 11);
            this.SLCtrl.Margin = new System.Windows.Forms.Padding(2);
            this.SLCtrl.Name = "SLCtrl";
            this.SLCtrl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("SLCtrl.OcxState")));
            this.SLCtrl.Size = new System.Drawing.Size(102, 110);
            this.SLCtrl.TabIndex = 6;
            this.SLCtrl.Visible = false;
            // 
            // SamlightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(130, 124);
            this.Controls.Add(this.SLCtrl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SamlightForm";
            this.Text = "SamlightForm";
            ((System.ComponentModel.ISupportInitialize)(this.SLCtrl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxSAMLIGHT_CLIENT_CTRL_OCXLib.AxScSamlightClientCtrl SLCtrl;
    }
}