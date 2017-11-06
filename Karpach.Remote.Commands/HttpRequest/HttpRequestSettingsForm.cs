using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Karpach.Remote.Commands.HttpRequest
{
    public partial class HttpRequestSettingsForm : Form
    {        
        public readonly HttpRequestSettings Settings;

        public HttpRequestSettingsForm(HttpRequestSettings settings)
        {
            InitializeComponent();            
            Settings = settings;                    
            txtCommandName.Text = Settings.CommandName;
            txtUrl.Text = Settings.Url;
        }        
            

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCommandName.Text))
            {
                MessageBox.Show("Please specify URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Regex.IsMatch(txtUrl.Text, @"https?:\/\/.+"))
            {
                MessageBox.Show("Invalid URL format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Settings.CommandName = txtCommandName.Text;
            Settings.Url = txtUrl.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HttpRequestSettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
