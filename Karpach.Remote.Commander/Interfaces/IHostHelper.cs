using System.Threading.Tasks;

namespace Karpach.Remote.Commander.Interfaces
{
    public interface IHostHelper
    {
        Task CreateHostAsync(CommandsManager commandsManager, int port);
        void Cancel();
        string SecretCode { get; set; }        
    }
}