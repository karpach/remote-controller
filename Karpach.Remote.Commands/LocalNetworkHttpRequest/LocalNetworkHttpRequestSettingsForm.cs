using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Windows.Forms;

namespace Karpach.Remote.Commands.LocalNetworkHttpRequest
{
    public partial class LocalNetworkHttpRequestSettingsForm : Form
    {        
        public readonly LocalNetworkHttpRequestSettings Settings;

        public LocalNetworkHttpRequestSettingsForm(LocalNetworkHttpRequestSettings settings)
        {
            InitializeComponent();            
            Settings = settings;
            cbxPcName.Text = Settings.PcName;            
            txtCommandName.Text = Settings.CommandName;
            txtUrl.Text = Settings.Url;
        }

        private void cbxPcName_DropDown(object sender, EventArgs e)
        {
            if (cbxPcName.DataSource == null)
            {
                Cursor.Current = Cursors.WaitCursor;
                DirectoryEntry root = new DirectoryEntry("WinNT:");
                var pcs = new List<string>();
                foreach (DirectoryEntry computers in root.Children)
                {
                    foreach (DirectoryEntry computer in computers.Children)
                    {
                        if (computer.SchemaClassName == "Computer")
                        {
                            pcs.Add(computer.Name);
                        }
                    }
                }
                cbxPcName.DataSource = pcs;
                Cursor.Current = Cursors.Default;
            }            
        }
            

        private void btnOk_Click(object sender, EventArgs e)
        {
            Settings.PcName = cbxPcName.Text;            
            Settings.CommandName = txtCommandName.Text;
            Settings.Url = txtUrl.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
