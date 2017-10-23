using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Karpach.Remote.Commands.Shutdown
{
    public partial class ShutdownCommandSettingsForm : Form
    {        
        public readonly ShutdownSettings Settings;

        public ShutdownCommandSettingsForm(ShutdownSettings settings)
        {
            InitializeComponent();            
            Settings = settings;
            cbxCommandType.DisplayMember = "Name";
            cbxCommandType.ValueMember = "CommandType";
            List<ShutdownCommandType> commandTypes = Enum.GetValues(typeof(ShutdownCommandType)).Cast<ShutdownCommandType>().ToList();
            cbxCommandType.DataSource = commandTypes;
            cbxCommandType.SelectedItem = commandTypes.SingleOrDefault(c => (int)c == (int)Settings.CommandType);                     
        }                    

        private void btnOk_Click(object sender, EventArgs e)
        {            
            Settings.CommandType = (ShutdownCommandType)cbxCommandType.SelectedItem;            
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }        
    }
}
