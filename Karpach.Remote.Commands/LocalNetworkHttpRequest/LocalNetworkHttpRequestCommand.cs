using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using Karpach.Remote.Commands.Interfaces;
using NLog;

namespace Karpach.Remote.Commands.LocalNetworkHttpRequest
{
    public class LocalNetworkHttpRequestCommand: CommandBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public override string CommandTitle => ConfiguredValue ? ((LocalNetworkHttpRequestSettings)Settings).CommandName : $"Local Network Command - {NotConfigured}";

        public override Image TrayIcon => Resources.Lan.ToBitmap();

        protected override Type SettingsType => typeof(LocalNetworkHttpRequestSettings);

        public LocalNetworkHttpRequestCommand():base(null)
        {
        }        

        private LocalNetworkHttpRequestCommand(Guid id) : base(id)
        {
        }

        public override void RunCommand(object sender, EventArgs e)
        {
            var settings = (LocalNetworkHttpRequestSettings)Settings;
            HttpClient client = new HttpClient();
            if (string.IsNullOrEmpty(settings.PcName))
            {
                Logger.Warn($"{settings.Id} Local Network Command no PC Name supplied.");
                return;
            }
            if (string.IsNullOrEmpty(settings.Url))
            {
                Logger.Warn($"{settings.Id} Local Network Command no Url supplied.");
                return;
            }
            string url = settings.Url.StartsWith("/") ? settings.Url.Substring(1) : settings.Url;
            HttpResponseMessage message = client.GetAsync($"http://{settings.PcName}/{url}").ConfigureAwait(false).GetAwaiter().GetResult();
            if (message.StatusCode != HttpStatusCode.OK)
            {
                Logger.Error($"{settings.Id} Local Network Command is unable to reach {url}");
            }
        }

        public override void ShowSettings()
        {
            var dlg = new LocalNetworkHttpRequestSettingsForm((LocalNetworkHttpRequestSettings)Settings);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                LibSettings[Id] = dlg.Settings;
                ConfiguredValue = true;
            }
        }

        public override IRemoteCommand Create(Guid id)
        {
            return new LocalNetworkHttpRequestCommand(id);
        }
    }
}