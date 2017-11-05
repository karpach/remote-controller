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
            txtArguments.Text = Settings.Arguments;
            txtDelay.Text = Settings.ExecutionDelay?.ToString() ?? "0";
        }                    

        private void btnOk_Click(object sender, EventArgs e)
        {            
            Settings.CommandName = txtCommandName.Text;
            Settings.ExecutablePath = txtExecutablePath.Text;
            Settings.Arguments = txtArguments.Text;
            int n;
            Settings.ExecutionDelay = int.TryParse(txtDelay.Text, out n) ? n : 0;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }        
    }
}
