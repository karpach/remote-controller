using System;
using System.Drawing;
using System.Windows.Forms;

namespace SampleCommand
{
    public class SampleCommandSettingsForm : Form
    {
        public readonly SampleCommandSettings Settings;

        private Button _btnOk;
        private Button _btnCancel;
        private Label _lbCommandName;
        private TextBox _txtCommandName;
        private Label _lbDelay;
        private TextBox _txtDelay;

        public SampleCommandSettingsForm(SampleCommandSettings settings)
        {
            InitializeComponent();
            Settings = settings;
            _txtCommandName.Text = Settings.CommandName;
            _txtDelay.Text = Settings.ExecutionDelay?.ToString() ?? "0";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Settings.CommandName = _txtCommandName.Text;
            int n;
            Settings.ExecutionDelay = int.TryParse(_txtDelay.Text, out n) ? n : 0;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitializeComponent()
        {
            _btnOk = new Button();
            _btnCancel = new Button();
            _lbCommandName = new Label();
            _txtCommandName = new TextBox();
            _lbDelay = new Label();
            _txtDelay = new TextBox();

            // 
            // btnOk
            // 
            _btnOk.DialogResult = DialogResult.OK;
            _btnOk.Location = new Point(130, 100);
            _btnOk.Name = "_btnOk";
            _btnOk.Size = new Size(75, 23);
            _btnOk.TabIndex = 2;
            _btnOk.Text = "Ok";
            _btnOk.UseVisualStyleBackColor = true;
            _btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            _btnCancel.DialogResult = DialogResult.Cancel;
            _btnCancel.Location = new Point(211, 100);
            _btnCancel.Name = "_btnCancel";
            _btnCancel.Size = new Size(75, 23);
            _btnCancel.TabIndex = 3;
            _btnCancel.Text = "Cancel";
            _btnCancel.UseVisualStyleBackColor = true;
            _btnCancel.Click += btnCancel_Click;
            // 
            // lbCommandName
            // 
            _lbCommandName.AutoSize = true;
            _lbCommandName.Location = new Point(22, 25);
            _lbCommandName.Name = "_lbCommandName";
            _lbCommandName.Size = new Size(88, 13);
            _lbCommandName.TabIndex = 0;
            _lbCommandName.Text = "Command Name:";
            // 
            // txtCommandName
            // 
            _txtCommandName.Location = new Point(116, 22);
            _txtCommandName.Name = "_txtCommandName";
            _txtCommandName.Size = new Size(269, 20);
            _txtCommandName.TabIndex = 0;
            // 
            // lbDelay
            // 
            _lbDelay.AutoSize = true;
            _lbDelay.Location = new Point(9, 60);
            _lbDelay.Name = "_lbDelay";
            _lbDelay.Size = new Size(101, 13);
            _lbDelay.TabIndex = 0;
            _lbDelay.Text = "Execution delay ms:";
            // 
            // txtDelay
            // 
            _txtDelay.Location = new Point(116, 57);
            _txtDelay.Name = "_txtDelay";
            _txtDelay.Size = new Size(271, 20);
            _txtDelay.TabIndex = 1;
            // 
            // SampleCommandSettingsForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(397, 140);            
            Controls.Add(_txtDelay);            
            Controls.Add(_txtCommandName);            
            Controls.Add(_lbDelay);
            Controls.Add(_lbCommandName);            
            Controls.Add(_btnCancel);
            Controls.Add(_btnOk);
            FormBorderStyle = FormBorderStyle.FixedDialog;            
            MaximizeBox = false;
            Name = "SampleCommandSettingsForm";
            Text = "Sample Command Settings";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
