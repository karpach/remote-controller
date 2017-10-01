using Karpach.Remote.Commands;
using Moq;
using Moq.AutoMock;
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
