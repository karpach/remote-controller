using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using Karpach.Remote.Commands.Base;
using Karpach.Remote.Commands.Interfaces;
using NLog;

namespace Karpach.Remote.Commands.HttpRequest
{
    public class HttpRequestCommand: CommandBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public override string CommandTitle => ConfiguredValue ? ((HttpRequestSettings)Settings).CommandName : $"HTTP Request Command - {NotConfigured}";

        public override Image TrayIcon => Resources.Lan.ToBitmap();

        protected override Type SettingsType => typeof(HttpRequestSettings);

        public HttpRequestCommand():base(null)
        {
        }        

        private HttpRequestCommand(Guid id) : base(id)
        {
        }

        public override void RunCommand(params object[] parameters)
        {
            var settings = (HttpRequestSettings)Settings;                        
            if (string.IsNullOrEmpty(settings.Url))
            {
                Logger.Warn($"{settings.Id} HTTP Request Command no Url supplied.");
                return;
            }
            string url = settings.Url.StartsWith("/") ? settings.Url.Substring(1) : settings.Url;
            HttpClient client = new HttpClient();
            HttpResponseMessage message = client.GetAsync(url).ConfigureAwait(false).GetAwaiter().GetResult();
            if (message.StatusCode != HttpStatusCode.OK)
            {
                Logger.Error($"{settings.Id} HTTP Request Command is unable to reach {url}");
            }
        }

        public override void ShowSettings()
        {
            var dlg = new HttpRequestSettingsForm((HttpRequestSettings)Settings);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                LibSettings[Id] = dlg.Settings;
                ConfiguredValue = true;
            }
        }

        public override IRemoteCommand Create(Guid id)
        {
            return new HttpRequestCommand(id);
        }
    }
}