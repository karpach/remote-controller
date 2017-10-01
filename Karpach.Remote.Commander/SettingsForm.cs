using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Karpach.Remote.Commander.Properties;
using Karpach.Remote.Commands.Interfaces;

namespace Karpach.Remote.Commander
{
    public partial class SettingsForm : Form
    {        
        public bool AutoStart => chkAutoLoad.Checked;
        public int Port => int.Parse(txtRemotePort.Text);
        public string SecretCode => txtSecretCode.Text;
        private BindingList<IRemoteCommand> _commands;

        public SettingsForm()
        {
            InitializeComponent();
            txtSecretCode.Text = Settings.Default.SecretCode;
            txtRemotePort.Text = Settings.Default.RemotePort.ToString();
            chkAutoLoad.Checked = Settings.Default.AutoStart;
            dgvCommands.AutoGenerateColumns = false;
        }

        public void InitialiazeCommands(IRemoteCommand[] commands)
        {
            _commands = new BindingList<IRemoteCommand>(commands.ToList()) { AllowNew = true };
            dgvCommands.DataSource = _commands;
        }

        private void txtPort_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int port;
            if (!int.TryParse(txtRemotePort.Text, out port))
            {
                e.Cancel = false;
            }
        }

        private void dgvCommands_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                DataGridViewButtonColumn button = senderGrid.Columns[e.ColumnIndex] as DataGridViewButtonColumn;
                if (button.Name == "btnEdit")
                {
                    _commands[e.RowIndex].ShowSettings();
                }
                if (button.Name == "btnAdd")
                {
                    _commands.Add(_commands[e.RowIndex].Create(_commands.Count));                    
                }
                if (button.Name == "btnRemove")
                {                    
                }
            }
        }
    }
}
