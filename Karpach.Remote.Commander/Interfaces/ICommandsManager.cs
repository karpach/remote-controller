using System;
using System.ComponentModel;

namespace Karpach.Remote.Commander.Interfaces
{
    public interface ICommandsManager: IBindingList
    {
        void RunCommand(Guid id);
        void ResetItem(int index);
    }
}