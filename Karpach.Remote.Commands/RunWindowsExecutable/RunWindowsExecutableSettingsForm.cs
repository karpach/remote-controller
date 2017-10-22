using System;
using System.Windows.Forms;

namespace Karpach.Remote.Commands.RunWindowsExecutable
{
    public partial class RunWindowsExecutableSettingsForm : Form
    {        
        public readonly RunWindowsExecutableSettings Settings;

        public RunWindowsExecutableSettingsForm(RunWindowsExecutableSettings settings)
        {
            InitializeComponent();            
            Settings = settings;             
            txtCommandName.Text = Settings.CommandName;
            txtExecutablePath.Text = Settings.ExecutablePath;
        }                    

        private void btnOk_Click(object sender, EventArgs e)
        {            
            Settings.CommandName = txtCommandName.Text;
            Settings.ExecutablePath = txtExecutablePath.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }        
    }
}
