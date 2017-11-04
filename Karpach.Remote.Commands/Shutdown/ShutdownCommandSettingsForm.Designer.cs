namespace Karpach.Remote.Commands.Shutdown
{
    partial class ShutdownCommandSettingsForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbCommandType = new System.Windows.Forms.Label();
            this.cbxCommandType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(71, 64);
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
            this.btnCancel.Location = new System.Drawing.Point(152, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbCommandType
            // 
            this.lbCommandType.AutoSize = true;
            this.lbCommandType.Location = new System.Drawing.Point(12, 22);
            this.lbCommandType.Name = "lbCommandType";
            this.lbCommandType.Size = new System.Drawing.Size(84, 13);
            this.lbCommandType.TabIndex = 0;
            this.lbCommandType.Text = "Command Type:";
            // 
            // cbxCommandType
            // 
            this.cbxCommandType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCommandType.FormattingEnabled = true;
            this.cbxCommandType.Location = new System.Drawing.Point(106, 19);
            this.cbxCommandType.Name = "cbxCommandType";
            this.cbxCommandType.Size = new System.Drawing.Size(146, 21);
            this.cbxCommandType.TabIndex = 4;
            // 
            // ShutdownCommandSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 100);
            this.Controls.Add(this.cbxCommandType);
            this.Controls.Add(this.lbCommandType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ShutdownCommandSettingsForm";
            this.Text = "Shutdown Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbCommandType;
        private System.Windows.Forms.ComboBox cbxCommandType;
    }
}