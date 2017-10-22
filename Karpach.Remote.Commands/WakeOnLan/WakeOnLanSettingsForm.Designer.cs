namespace Karpach.Remote.Commands.WakeOnLan
{
    partial class WakeOnLanSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WakeOnLanSettingsForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbPcName = new System.Windows.Forms.Label();
            this.lbMacAddress = new System.Windows.Forms.Label();
            this.cbxPcName = new System.Windows.Forms.ComboBox();
            this.txtMacAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(57, 107);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(138, 107);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbPcName
            // 
            this.lbPcName.AutoSize = true;
            this.lbPcName.Location = new System.Drawing.Point(29, 31);
            this.lbPcName.Name = "lbPcName";
            this.lbPcName.Size = new System.Drawing.Size(55, 13);
            this.lbPcName.TabIndex = 0;
            this.lbPcName.Text = "PC Name:";
            // 
            // lbMacAddress
            // 
            this.lbMacAddress.AutoSize = true;
            this.lbMacAddress.Location = new System.Drawing.Point(12, 62);
            this.lbMacAddress.Name = "lbMacAddress";
            this.lbMacAddress.Size = new System.Drawing.Size(72, 13);
            this.lbMacAddress.TabIndex = 0;
            this.lbMacAddress.Text = "Mac Address:";
            // 
            // cbxPcName
            // 
            this.cbxPcName.FormattingEnabled = true;
            this.cbxPcName.Location = new System.Drawing.Point(91, 28);
            this.cbxPcName.Name = "cbxPcName";
            this.cbxPcName.Size = new System.Drawing.Size(121, 21);
            this.cbxPcName.TabIndex = 0;
            this.cbxPcName.DropDown += new System.EventHandler(this.cbxPcName_DropDown);
            this.cbxPcName.SelectedValueChanged += new System.EventHandler(this.cbxPcName_Leave);
            this.cbxPcName.Leave += new System.EventHandler(this.cbxPcName_Leave);
            // 
            // txtMacAddress
            // 
            this.txtMacAddress.Location = new System.Drawing.Point(91, 62);
            this.txtMacAddress.Name = "txtMacAddress";
            this.txtMacAddress.Size = new System.Drawing.Size(121, 20);
            this.txtMacAddress.TabIndex = 1;
            // 
            // WakeOnLanSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 144);
            this.Controls.Add(this.txtMacAddress);
            this.Controls.Add(this.cbxPcName);
            this.Controls.Add(this.lbMacAddress);
            this.Controls.Add(this.lbPcName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WakeOnLanSettingsForm";
            this.Text = "Wake-On-Lan Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbPcName;
        private System.Windows.Forms.Label lbMacAddress;
        private System.Windows.Forms.ComboBox cbxPcName;
        private System.Windows.Forms.TextBox txtMacAddress;
    }
}