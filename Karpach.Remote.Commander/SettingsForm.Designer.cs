namespace Karpach.Remote.Commander
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.chkAutoLoad = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbSecretCode = new System.Windows.Forms.Label();
            this.txtSecretCode = new System.Windows.Forms.TextBox();
            this.lbRemotePort = new System.Windows.Forms.Label();
            this.txtRemotePort = new System.Windows.Forms.TextBox();
            this.dgvCommands = new System.Windows.Forms.DataGridView();
            this.lbCommands = new System.Windows.Forms.Label();
            this.Command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Assembly = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnAdd = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommands)).BeginInit();
            this.SuspendLayout();
            // 
            // chkAutoLoad
            // 
            this.chkAutoLoad.AutoSize = true;
            this.chkAutoLoad.Location = new System.Drawing.Point(18, 366);
            this.chkAutoLoad.Name = "chkAutoLoad";
            this.chkAutoLoad.Size = new System.Drawing.Size(165, 17);
            this.chkAutoLoad.TabIndex = 1;
            this.chkAutoLoad.Text = "Auto load at Windows startup";
            this.chkAutoLoad.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(227, 417);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(310, 417);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lbSecretCode
            // 
            this.lbSecretCode.AutoSize = true;
            this.lbSecretCode.Location = new System.Drawing.Point(15, 15);
            this.lbSecretCode.Name = "lbSecretCode";
            this.lbSecretCode.Size = new System.Drawing.Size(69, 13);
            this.lbSecretCode.TabIndex = 2;
            this.lbSecretCode.Text = "Secret Code:";
            // 
            // txtSecretCode
            // 
            this.txtSecretCode.Location = new System.Drawing.Point(89, 12);
            this.txtSecretCode.Name = "txtSecretCode";
            this.txtSecretCode.Size = new System.Drawing.Size(121, 20);
            this.txtSecretCode.TabIndex = 5;
            // 
            // lbRemotePort
            // 
            this.lbRemotePort.AutoSize = true;
            this.lbRemotePort.Location = new System.Drawing.Point(15, 46);
            this.lbRemotePort.Name = "lbRemotePort";
            this.lbRemotePort.Size = new System.Drawing.Size(69, 13);
            this.lbRemotePort.TabIndex = 2;
            this.lbRemotePort.Text = "Remote Port:";
            // 
            // txtRemotePort
            // 
            this.txtRemotePort.Location = new System.Drawing.Point(89, 43);
            this.txtRemotePort.Name = "txtRemotePort";
            this.txtRemotePort.Size = new System.Drawing.Size(121, 20);
            this.txtRemotePort.TabIndex = 5;
            this.txtRemotePort.Validating += new System.ComponentModel.CancelEventHandler(this.txtPort_Validating);
            // 
            // dgvCommands
            // 
            this.dgvCommands.AllowUserToAddRows = false;
            this.dgvCommands.AllowUserToDeleteRows = false;
            this.dgvCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCommands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Command,
            this.Assembly,
            this.Version,
            this.btnEdit,
            this.btnAdd,
            this.btnRemove});
            this.dgvCommands.Location = new System.Drawing.Point(18, 106);
            this.dgvCommands.Name = "dgvCommands";
            this.dgvCommands.ReadOnly = true;
            this.dgvCommands.Size = new System.Drawing.Size(612, 240);
            this.dgvCommands.TabIndex = 6;
            this.dgvCommands.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCommands_CellContentClick);
            // 
            // lbCommands
            // 
            this.lbCommands.AutoSize = true;
            this.lbCommands.Location = new System.Drawing.Point(15, 78);
            this.lbCommands.Name = "lbCommands";
            this.lbCommands.Size = new System.Drawing.Size(102, 13);
            this.lbCommands.TabIndex = 2;
            this.lbCommands.Text = "Remote Commands:";
            // 
            // Command
            // 
            this.Command.DataPropertyName = "CommandTitle";
            this.Command.FillWeight = 150F;
            this.Command.HeaderText = "Command";
            this.Command.Name = "Command";
            this.Command.ReadOnly = true;
            this.Command.Width = 150;
            // 
            // Assembly
            // 
            this.Assembly.DataPropertyName = "AssemblyName";
            this.Assembly.HeaderText = "Assembly";
            this.Assembly.Name = "Assembly";
            this.Assembly.ReadOnly = true;
            // 
            // Version
            // 
            this.Version.DataPropertyName = "Version";
            this.Version.FillWeight = 60F;
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Width = 60;
            // 
            // btnEdit
            // 
            this.btnEdit.FillWeight = 80F;
            this.btnEdit.HeaderText = "Settings";
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ReadOnly = true;
            this.btnEdit.Text = "Settings";
            this.btnEdit.UseColumnTextForButtonValue = true;
            this.btnEdit.Width = 80;
            // 
            // btnAdd
            // 
            this.btnAdd.FillWeight = 80F;
            this.btnAdd.HeaderText = "Add";
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ReadOnly = true;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseColumnTextForButtonValue = true;
            this.btnAdd.Width = 80;
            // 
            // btnRemove
            // 
            this.btnRemove.FillWeight = 80F;
            this.btnRemove.HeaderText = "Remove";
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.ReadOnly = true;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseColumnTextForButtonValue = true;
            this.btnRemove.Width = 80;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 466);
            this.Controls.Add(this.dgvCommands);
            this.Controls.Add(this.txtRemotePort);
            this.Controls.Add(this.txtSecretCode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lbCommands);
            this.Controls.Add(this.lbRemotePort);
            this.Controls.Add(this.lbSecretCode);
            this.Controls.Add(this.chkAutoLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommands)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAutoLoad;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbSecretCode;
        private System.Windows.Forms.TextBox txtSecretCode;
        private System.Windows.Forms.Label lbRemotePort;
        private System.Windows.Forms.TextBox txtRemotePort;
        private System.Windows.Forms.DataGridView dgvCommands;
        private System.Windows.Forms.Label lbCommands;
        private System.Windows.Forms.DataGridViewTextBoxColumn Command;
        private System.Windows.Forms.DataGridViewTextBoxColumn Assembly;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewButtonColumn btnEdit;
        private System.Windows.Forms.DataGridViewButtonColumn btnAdd;
        private System.Windows.Forms.DataGridViewButtonColumn btnRemove;
    }
}