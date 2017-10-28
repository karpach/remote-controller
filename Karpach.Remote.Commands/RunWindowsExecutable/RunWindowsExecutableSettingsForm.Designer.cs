namespace Karpach.Remote.Commands.RunWindowsExecutable
{
    partial class RunWindowsExecutableSettingsForm
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
            this.lbCommandName = new System.Windows.Forms.Label();
            this.txtCommandName = new System.Windows.Forms.TextBox();
            this.lbExecutablePath = new System.Windows.Forms.Label();
            this.txtExecutablePath = new System.Windows.Forms.TextBox();
            this.lbArguments = new System.Windows.Forms.Label();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(130, 142);
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
            this.btnCancel.Location = new System.Drawing.Point(211, 142);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbCommandName
            // 
            this.lbCommandName.AutoSize = true;
            this.lbCommandName.Location = new System.Drawing.Point(9, 22);
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
            // lbExecutablePath
            // 
            this.lbExecutablePath.AutoSize = true;
            this.lbExecutablePath.Location = new System.Drawing.Point(9, 65);
            this.lbExecutablePath.Name = "lbExecutablePath";
            this.lbExecutablePath.Size = new System.Drawing.Size(88, 13);
            this.lbExecutablePath.TabIndex = 0;
            this.lbExecutablePath.Text = "Exectuable Path:";
            // 
            // txtExecutablePath
            // 
            this.txtExecutablePath.Location = new System.Drawing.Point(103, 62);
            this.txtExecutablePath.Name = "txtExecutablePath";
            this.txtExecutablePath.Size = new System.Drawing.Size(284, 20);
            this.txtExecutablePath.TabIndex = 1;
            // 
            // lbArguments
            // 
            this.lbArguments.AutoSize = true;
            this.lbArguments.Location = new System.Drawing.Point(37, 105);
            this.lbArguments.Name = "lbArguments";
            this.lbArguments.Size = new System.Drawing.Size(60, 13);
            this.lbArguments.TabIndex = 0;
            this.lbArguments.Text = "Arguments:";
            // 
            // txtArguments
            // 
            this.txtArguments.Location = new System.Drawing.Point(103, 102);
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(284, 20);
            this.txtArguments.TabIndex = 1;
            // 
            // RunWindowsExecutableSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 180);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.txtExecutablePath);
            this.Controls.Add(this.txtCommandName);
            this.Controls.Add(this.lbArguments);
            this.Controls.Add(this.lbCommandName);
            this.Controls.Add(this.lbExecutablePath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RunWindowsExecutableSettingsForm";
            this.Text = "Run Windows Executable Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbCommandName;
        private System.Windows.Forms.TextBox txtCommandName;
        private System.Windows.Forms.Label lbExecutablePath;
        private System.Windows.Forms.TextBox txtExecutablePath;
        private System.Windows.Forms.Label lbArguments;
        private System.Windows.Forms.TextBox txtArguments;
    }
}