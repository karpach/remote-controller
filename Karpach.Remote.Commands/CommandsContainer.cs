using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Karpach.Remote.Commands.Interfaces;
using Karpach.Remote.Commands.WakeOnLan;

namespace Karpach.Remote.Commands
{
    [Export(typeof(IRemoteCommandContainer))]
    public class CommandsContainer: IRemoteCommandContainer
    {
        private readonly IRemoteCommand[] _commands;

        public CommandsContainer()
        {
            _commands = new IRemoteCommand[]
            {
                new WakeOnLanCommand()
            };
        }

        public IEnumerator<IRemoteCommand> GetEnumerator()
        {
            return ((IEnumerable<IRemoteCommand>)_commands).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}