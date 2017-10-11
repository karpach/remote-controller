using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Karpach.Remote.Commander.Interfaces;
using Karpach.Remote.Commands.Interfaces;
using System.ComponentModel;

namespace Karpach.Remote.Commander
{
    public class CommandsManager: IBindingList
    {
        private readonly ICommandsSettings _commandsSettings;
        private readonly List<IRemoteCommand> _commands;       

        public CommandsManager(IEnumerable<IRemoteCommand> commands, ICommandsSettings commandsSettings)
        {
            _commandsSettings = commandsSettings;
            IEnumerable<IRemoteCommand> remoteCommands = commands as IRemoteCommand[] ?? commands.ToArray();
            List<IRemoteCommand> clonedCommands = new List<IRemoteCommand>();
            foreach (IRemoteCommand command in remoteCommands)
            {
                _commandsSettings.Add(command.GetType(), command.Id);
                Guid[] ids = commandsSettings.GetCommandIds(command.GetType());
                foreach (Guid id in ids)
                {
                    if (id != command.Id)
                    {
                        clonedCommands.Add(command.Create(id));
                    }                    
                }
            }            
            _commands = new List<IRemoteCommand>(remoteCommands);
            _commands.AddRange(clonedCommands);
        }

        public IEnumerator<IRemoteCommand> GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _commands.GetEnumerator();
        }        

        public int Add(object item)
        {
            if (!(item is IRemoteCommand))
            {
                return -1;
            }
            var command = (IRemoteCommand)item;
            _commands.Add(command);
            _commandsSettings.Add(item.GetType(), command.Id);
            ListChanged?.Invoke(this, new ListChangedEventArgs(ListChangedType.ItemAdded, _commands.Count-1));
            return _commands.Count - 1;
        }

        public bool Contains(object item)
        {
            if (!(item is IRemoteCommand))
            {
                return false;
            }
            var command = (IRemoteCommand)item;
            return _commands.Any(c => c.Id == command.Id);
        }

        public void Clear()
        {
            _commands.Clear();
        }

        public int IndexOf(object item)
        {
            if (!(item is IRemoteCommand))
            {
                return -1;
            }
            var command = (IRemoteCommand)item;
            return _commands.FindIndex(c => c.Id == command.Id);
        }

        public void Insert(int index, object item)
        {
            if (!(item is IRemoteCommand))
            {
                return;
            }
            var command = (IRemoteCommand)item;
            _commandsSettings.Add(command.GetType(), command.Id);
            _commands.Insert(index, command);
            ListChanged?.Invoke(this, new ListChangedEventArgs(ListChangedType.ItemAdded, index));
        }

        public void Remove(object item)
        {
            if (!(item is IRemoteCommand))
            {
                return;
            }
            var command = (IRemoteCommand)item;
            command.Delete();
            int index = _commands.FindIndex(c => c.Id == command.Id);
            _commandsSettings.Remove(command.GetType(), command.Id);
            _commands.RemoveAt(index);
            ListChanged?.Invoke(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }              

        public void CopyTo(Array array, int index)
        {
            List<IRemoteCommand> commands = new List<IRemoteCommand>();
            foreach (object obj in array)
            {
                if (!(obj is IRemoteCommand))
                {
                    return;
                }
                commands.Add((IRemoteCommand)obj);
            }
            _commands.CopyTo(commands.ToArray(), index);
        }

        public int Count => _commands.Count;
        public object SyncRoot => false;
        public bool IsSynchronized => true;

        public bool IsReadOnly => false;
        public bool IsFixedSize => false;                

        public void RemoveAt(int index)
        {
            _commandsSettings.Remove(_commands[index].GetType(), _commands[index].Id);
            _commands.RemoveAt(index);
            ListChanged?.Invoke(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }

        object IList.this[int index]
        {
            get => _commands[index];
            set => _commands[index] = (IRemoteCommand) value;
        }

        public IRemoteCommand this[int index]
        {
            get => _commands[index];
            set => _commands[index] = (IRemoteCommand)value;
        }

        public void ResetItem(int index)
        {
            ListChanged?.Invoke(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
        }

        public object AddNew()
        {
            throw new NotImplementedException();
        }

        public void AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotImplementedException();
        }

        public int Find(PropertyDescriptor property, object key)
        {
            throw new NotImplementedException();
        }
        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void RemoveSort()
        {
            throw new NotImplementedException();
        }
        public bool AllowNew => true;
        public bool AllowEdit => false;
        public bool AllowRemove => true;
        public bool SupportsChangeNotification => true;
        public bool SupportsSearching => false;
        public bool SupportsSorting => false;
        public bool IsSorted => false;
        public PropertyDescriptor SortProperty => null;
        public ListSortDirection SortDirection => ListSortDirection.Ascending;
        public event ListChangedEventHandler ListChanged;
    }
}