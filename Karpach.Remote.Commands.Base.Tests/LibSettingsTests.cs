using System;
using System.IO;
using System.Linq;
using Karpach.Remote.Commands.HttpRequest;
using Karpach.Remote.Commands.WakeOnLan;
using NUnit.Framework;

namespace Karpach.Remote.Commands.Base.Tests
{
    [TestFixture]
    public class LibSettingsTests
    {
        private WakeOnLanCommandSettings[] _wakeOnLanCommandSettings;
        private HttpRequestSettings[] _localNetworkHttpRequestSettings;

        [SetUp]
        public void Setup()
        {
            _wakeOnLanCommandSettings = new [] {
                new WakeOnLanCommandSettings
                {
                    Id = Guid.Parse("00000001-6D4F-4B9F-94FA-0FEE6C8A5F4A"),
                    PcName = "John"
                },
                new WakeOnLanCommandSettings
                {
                    Id = Guid.Parse("00000002-6D4F-4B9F-94FA-0FEE6C8A5F4A"),
                    PcName = "Jean"                  
                },
                new WakeOnLanCommandSettings
                {
                    Id = Guid.Parse("00000003-6D4F-4B9F-94FA-0FEE6C8A5F4A"),
                    PcName = "Michael"                
                },
            };

            _localNetworkHttpRequestSettings = new [] {
                new HttpRequestSettings
                {
                    Id = Guid.Parse("10000001-6D4F-4B9F-94FA-0FEE6C8A5F4A"),
                    Url = "Test1"
                },
                new HttpRequestSettings
                {
                    Id = Guid.Parse("10000002-6D4F-4B9F-94FA-0FEE6C8A5F4A"),
                    Url = "Test2"
                }
            };
        }

        [Test]        
        public void Mixed_Settings_Test()
        {
            // Arrange
            string tempPath = Path.GetTempFileName();

            // Act
            var settings = new LibSettings(typeof(WakeOnLanCommandSettings).Assembly)
            {
                [_wakeOnLanCommandSettings[0].Id] = _wakeOnLanCommandSettings[0],
                [_wakeOnLanCommandSettings[1].Id] = _wakeOnLanCommandSettings[1],
                [_localNetworkHttpRequestSettings[0].Id] = _localNetworkHttpRequestSettings[0],
                [_wakeOnLanCommandSettings[2].Id] = _wakeOnLanCommandSettings[2],
                [_localNetworkHttpRequestSettings[1].Id] = _localNetworkHttpRequestSettings[1]
            };

            // Assert
            Assert.AreEqual(settings.GetValues(typeof(WakeOnLanCommandSettings)).Length, 3);
            Assert.AreEqual(settings.GetValues(typeof(HttpRequestSettings)).Length, 2);
            Assert.AreEqual(settings.Length, 5);
            Console.WriteLine(File.ReadAllText(tempPath));

        }

        [Test]
        public void Mixed_Settings_Read_Test()
        {
            // Arrange
            string tempPath = Path.GetTempFileName();
            var initSettings = new LibSettings(typeof(WakeOnLanCommandSettings).Assembly)
            {
                [_wakeOnLanCommandSettings[0].Id] = _wakeOnLanCommandSettings[0],
                [_wakeOnLanCommandSettings[1].Id] = _wakeOnLanCommandSettings[1],
                [_localNetworkHttpRequestSettings[0].Id] = _localNetworkHttpRequestSettings[0],
                [_wakeOnLanCommandSettings[2].Id] = _wakeOnLanCommandSettings[2],
                [_localNetworkHttpRequestSettings[1].Id] = _localNetworkHttpRequestSettings[1]
            };

            // Act
            var settings = new LibSettings(typeof(WakeOnLanCommandSettings).Assembly);
            WakeOnLanCommandSettings[] wakeOnLanCommandSettings = settings.GetValues(typeof(WakeOnLanCommandSettings)).Cast<WakeOnLanCommandSettings>().ToArray();
            HttpRequestSettings[] localNetworkHttpRequestSettings = settings.GetValues(typeof(HttpRequestSettings)).Cast<HttpRequestSettings>().ToArray();

            // Assert
            Assert.AreEqual(initSettings.Length, 5);
            Assert.AreEqual(wakeOnLanCommandSettings.Length, 3);
            Assert.AreEqual(localNetworkHttpRequestSettings.Length, 2);
            Assert.AreEqual(settings.Length, 5);
            Assert.True(wakeOnLanCommandSettings.All(p=>!string.IsNullOrEmpty(p.PcName)));            
            Assert.True(localNetworkHttpRequestSettings.All(p => !string.IsNullOrEmpty(p.Url)));
            WakeOnLanCommandSettings wakeOnLanCommandSetting = settings[wakeOnLanCommandSettings[0].Id] as WakeOnLanCommandSettings;
            Assert.IsNotNull(wakeOnLanCommandSetting);
            Assert.AreEqual(wakeOnLanCommandSettings[0].Id, wakeOnLanCommandSetting.Id);
            Console.WriteLine(File.ReadAllText(tempPath));
        }

        [Test]
        public void Settings_Remove_Test()
        {
            // Arrange
            string tempPath = Path.GetTempFileName();
            var initSettings = new LibSettings(typeof(WakeOnLanCommandSettings).Assembly)
            {
                [_wakeOnLanCommandSettings[0].Id] = _wakeOnLanCommandSettings[0],
                [_wakeOnLanCommandSettings[1].Id] = _wakeOnLanCommandSettings[1],
                [_localNetworkHttpRequestSettings[0].Id] = _localNetworkHttpRequestSettings[0],
                [_wakeOnLanCommandSettings[2].Id] = _wakeOnLanCommandSettings[2],
                [_localNetworkHttpRequestSettings[1].Id] = _localNetworkHttpRequestSettings[1]
            };

            // Act
            initSettings.Remove(_wakeOnLanCommandSettings[1].Id);
            var settings = new LibSettings(typeof(WakeOnLanCommandSettings).Assembly);            
            WakeOnLanCommandSettings[] wakeOnLanCommandSettings = settings.GetValues(typeof(WakeOnLanCommandSettings)).Cast<WakeOnLanCommandSettings>().ToArray();
            HttpRequestSettings[] localNetworkHttpRequestSettings = settings.GetValues(typeof(HttpRequestSettings)).Cast<HttpRequestSettings>().ToArray();

            // Assert
            Assert.AreEqual(initSettings.Length, 4);
            Assert.AreEqual(wakeOnLanCommandSettings.Length, 2);
            Assert.AreEqual(localNetworkHttpRequestSettings.Length, 2);
            Assert.AreEqual(settings.Length, 4);
            Assert.True(wakeOnLanCommandSettings.All(p => !string.IsNullOrEmpty(p.PcName)));            
            Assert.True(localNetworkHttpRequestSettings.All(p => !string.IsNullOrEmpty(p.Url)));
            WakeOnLanCommandSettings wakeOnLanCommandSetting = settings[wakeOnLanCommandSettings[1].Id] as WakeOnLanCommandSettings;
            Assert.IsNotNull(wakeOnLanCommandSetting);
            Assert.AreEqual(wakeOnLanCommandSettings[1].Id, wakeOnLanCommandSetting.Id);
            Console.WriteLine(File.ReadAllText(tempPath));
        }        
    }
}