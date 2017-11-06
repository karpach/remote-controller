namespace Karpach.Remote.Commands.HttpRequest
{
    partial class HttpRequestSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HttpRequestSettingsForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbCommandName = new System.Windows.Forms.Label();
            this.txtCommandName = new System.Windows.Forms.TextBox();
            this.lbUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(130, 97);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(211, 97);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbCommandName
            // 
            this.lbCommandName.AutoSize = true;
            this.lbCommandName.Location = new System.Drawing.Point(12, 22);
            this.lbCommandName.Name = "lbCommandName";
            this.lbCommandName.Size = new System.Drawing.Size(88, 13);
            this.lbCommandName.TabIndex = 0;
            this.lbCommandName.Text = "Command Name:";
            // 
            // txtCommandName
            // 
            this.txtCommandName.Location = new System.Drawing.Point(103, 22);
            this.txtCommandName.Name = "txtCommandName";
            this.txtCommandName.Size = new System.Drawing.Size(282, 20);
            this.txtCommandName.TabIndex = 0;
            // 
            // lbUrl
            // 
            this.lbUrl.AutoSize = true;
            this.lbUrl.Location = new System.Drawing.Point(65, 62);
            this.lbUrl.Name = "lbUrl";
            this.lbUrl.Size = new System.Drawing.Size(32, 13);
            this.lbUrl.TabIndex = 0;
            this.lbUrl.Text = "URL:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(103, 59);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(284, 20);
            this.txtUrl.TabIndex = 2;
            // 
            // HttpRequestSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 135);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.txtCommandName);
            this.Controls.Add(this.lbCommandName);
            this.Controls.Add(this.lbUrl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "HttpRequestSettingsForm";
            this.Text = "HTTP Request Settings";
            this.Load += new System.EventHandler(this.HttpRequestSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbCommandName;
        private System.Windows.Forms.TextBox txtCommandName;
        private System.Windows.Forms.Label lbUrl;
        private System.Windows.Forms.TextBox txtUrl;
    }
}