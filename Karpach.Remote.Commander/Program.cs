using System;
using System.Windows.Forms;
using Autofac;
using Karpach.Remote.Commander.Helpers;
using Karpach.Remote.Commander.Interfaces;
using NLog;

namespace Karpach.Remote.Commander
{
	static class Program
	{
		public static IContainer Container { get; set; }
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				var builder = new ContainerBuilder();
				builder.RegisterType<ControllerApplicationContext>().AsSelf();
				builder.RegisterType<SettingsForm>().AsSelf();
				builder.RegisterType<HostHelper>();
				builder.RegisterType<CommandsSettings>().As<ICommandsSettings>();
				builder.RegisterType<CommandsManager>();
				builder.RegisterType<SettingsForm>();
				Container = builder.Build();
				Application.EnableVisualStyles();
				Application.SetHighDpiMode(HighDpiMode.SystemAware);
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(Container.Resolve<ControllerApplicationContext>());
			}
			catch (Exception ex)
			{
				Logger.Log(LogLevel.Fatal, ex);
				MessageBox.Show("Encountered a fatal error, please see log file for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
