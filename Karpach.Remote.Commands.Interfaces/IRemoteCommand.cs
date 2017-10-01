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
        /// Index of the same type command
        /// </summary>
        int Index { get; set; }

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
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        void RunCommand(object sender, EventArgs e);

        /// <summary>
        /// Shows command settings dialog
        /// </summary>
        void ShowSettings();

        /// <summary>
        /// Creates new command of the same type
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IRemoteCommand Create(int index);

        /// <summary>
        /// True if it is possible to create another command of the same type
        /// </summary>
        /// <returns></returns>
        bool CanCreate();
    }
}