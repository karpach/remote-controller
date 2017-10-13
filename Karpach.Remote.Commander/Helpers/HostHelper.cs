using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Karpach.Remote.Commander.Interfaces;
using Karpach.Remote.Commands.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karpach.Remote.Commander.Helpers
{
    public class HostHelper : IHostHelper
    {     
        private CancellationTokenSource _cancellationTokenSource;
        private Task _hostTask;
        private ICommandsManager _commandsManager;

        public string SecretCode { get; set; }

        public delegate HostHelper Factory(ICommandsManager commandsManager);

        public HostHelper(ICommandsManager commandsManager)
        {
            _commandsManager = commandsManager;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task CreateHostAsync(int port)
        {            
            if (_hostTask != null)
            {
                _cancellationTokenSource.Cancel();
                await _hostTask.ConfigureAwait(false);
                _cancellationTokenSource = new CancellationTokenSource();
            }
            _hostTask = Task.Run(() =>
            {
                var host = new WebHostBuilder()
                    .UseUrls($"http://+:{port}")
                    .UseKestrel()
                    .Configure(app =>
                    {
                        app.Run(async context =>
                        {
#pragma warning disable 4014
                            ProcessRequestAsync(context.Request.Path.Value);
#pragma warning restore 4014
                            await context.Response.WriteAsync("Ok");
                        });
                    })
                    .Build();
                host.Run(_cancellationTokenSource.Token);                
            }, _cancellationTokenSource.Token);
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        internal async Task ProcessRequestAsync(string url)
        {
            await Task.Delay(1000).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(SecretCode) && !url.StartsWith($"/{SecretCode}/"))
            {                
                return;
            }
            int lastSlashPosition = url.LastIndexOf("/", StringComparison.Ordinal);
            string commandId = String.Empty;
            if (lastSlashPosition >= 0 && url.Length > 1)
            {
                commandId = url.Substring(lastSlashPosition + 1);
            }
            Guid id;
            if (Guid.TryParse(commandId, out id))
            {
                _commandsManager.RunCommand(id);
            }
        }
    }
}