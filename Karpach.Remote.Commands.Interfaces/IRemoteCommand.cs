using System;
using System.Drawing;

namespace Karpach.Remote.Commands.Interfaces
{
    public interface IRemoteCommand
    {
        /// <summary>
        /// Unique identifier for the command
        /// </summary>
        Guid Id { get; }        

        /// <summary>
        /// Title of the command
        /// </summary>
        string CommandTitle { get; }

        /// <summary>
        /// True if command was configured
        /// </summary>
        bool Configured { get; }

        /// <summary>
        /// Tray icon
        /// </summary>
        Image TrayIcon { get; }        

        /// <summary>
        /// Assembly version
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Assembly name
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// Executes command
        /// </summary>        
        /// <param name="parameters">Execution parameters</param>
        void RunCommand(params object[] parameters);

        /// <summary>
        /// Shows command settings dialog
        /// </summary>
        void ShowSettings();

        /// <summary>
        /// Creates new command of the same type
        /// </summary> 
        /// <param name="id">Command Id</param>       
        /// <returns>Command</returns>
        IRemoteCommand Create(Guid id);

        /// <summary>
        /// True if it is possible to create another command of the same type
        /// </summary>
        /// <returns></returns>
        bool CanCreate();

        bool Delete();
    }
}