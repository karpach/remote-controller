using System;
using System.Windows.Forms;
using Karpach.Remote.Commander.Interfaces;
using Karpach.Remote.Commander.Properties;
using Karpach.Remote.Commands.Interfaces;
using NLog;

namespace Karpach.Remote.Commander
{
    public partial class SettingsForm : Form
    {        
        public bool AutoStart => chkAutoLoad.Checked;
        public int Port => int.Parse(txtRemotePort.Text);
        public string SecretCode => txtSecretCode.Text;
        private readonly ICommandsManager _commands;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public delegate SettingsForm Factory(ICommandsManager commands);

        public SettingsForm(ICommandsManager commands)
        {
            InitializeComponent();
            txtSecretCode.Text = Settings.Default.SecretCode;
            txtRemotePort.Text = Settings.Default.RemotePort.ToString();
            chkAutoLoad.Checked = Settings.Default.AutoStart;
            dgvCommands.AutoGenerateColumns = false;
            _commands = commands;
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
                DataGridViewButtonColumn button = (DataGridViewButtonColumn)senderGrid.Columns[e.ColumnIndex];
                if (button.Name == "btnEdit")
                {                    
                    ((IRemoteCommand)_commands[e.RowIndex]).ShowSettings();
                    _commands.ResetItem(e.RowIndex);
                }
                if (button.Name == "btnAdd")
                {
                    _commands.Add(((IRemoteCommand)_commands[e.RowIndex]).Create(Guid.NewGuid()));                    
                }
                if (button.Name == "btnRemove")
                {      
                    _commands.Remove(_commands[e.RowIndex]);
                }
                if (button.Name == "btnUrl")
                {
                    Guid id = ((IRemoteCommand) _commands[e.RowIndex]).Id;
                    string secret = string.IsNullOrEmpty(txtSecretCode.Text) ? string.Empty : $"{txtSecretCode.Text}/";
                    string url = $"http://{Environment.MachineName}:{txtRemotePort.Text}/{secret}{id}";                    
                    if (MessageBox.Show(url, "Do you want to copy the following command URL to clipboard?", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    {
                        try
                        {
                            Clipboard.SetDataObject(url, true, 10, 100);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"Unable to copy to clipboard {ex}");
                        }
                    }
                }
            }
        }
    }
}
