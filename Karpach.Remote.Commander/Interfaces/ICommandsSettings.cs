using System;

namespace Karpach.Remote.Commander.Interfaces
{
    public interface ICommandsSettings
    {
        Guid[] GetCommandIds(Type type);
        void Add(Type type, Guid id);
        bool Remove(Type type, Guid id);        
    }
}