using Karpach.Remote.Commands.WakeOnLan;
using NUnit.Framework;

namespace Karpach.Remote.Commander.Tests
{
    [TestFixture]
    public class SettingsFormTests
    {        

        [Test]    
        //[Ignore("UI test")]
        public void IPTest()
        {
            // Arrange
            var dlg = new WakeOnLanSettingsForm(new WakeOnLanCommandSettings());

            // Act
            dlg.ShowDialog();
        }
        
    }
}
