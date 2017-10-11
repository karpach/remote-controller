using System;
using System.Windows.Forms;
using Autofac;
using Karpach.Remote.Commander.Helpers;
using Karpach.Remote.Commander.Interfaces;

namespace Karpach.Remote.Commander
{
    static class Program
    {
        public static IContainer Container { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();            
            builder.RegisterType<ControllerApplicationContext>().AsSelf();
            builder.RegisterType<SettingsForm>().AsSelf();
            builder.RegisterType<HostHelper>().As<IHostHelper>();
            builder.RegisterType<CommandsSettings>().As<ICommandsSettings>();
            Container = builder.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<ControllerApplicationContext>());
        }
    }
}
