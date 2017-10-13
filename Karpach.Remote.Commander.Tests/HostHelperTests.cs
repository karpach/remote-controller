using System;
using System.Threading.Tasks;
using Karpach.Remote.Commander.Helpers;
using Karpach.Remote.Commander.Interfaces;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace Karpach.Remote.Commander.Tests
{
    [TestFixture]
    public class HostHelperTests
    {
        private AutoMocker _mocker;
        private HostHelper _hostHelper;        

        [SetUp]
        public void Init()
        {
            _mocker = new AutoMocker();
            _hostHelper = _mocker.CreateInstance<HostHelper>();                        
        }

        [Test]        
        public async Task ProcessRequestAsync_Ignore_Secret()
        {
            // Arrange                        
            Guid commandId = Guid.NewGuid();            

            // Act
            await _hostHelper.ProcessRequestAsync($"/someCode/{commandId}").ConfigureAwait(false);

            // Assert
            _mocker.Verify<ICommandsManager>(x => x.RunCommand(commandId), Times.Once);
        }

        [Test]
        public async Task ProcessRequestAsync_With_Secret()
        {
            // Arrange                        
            Guid commandId = Guid.NewGuid();
            string secret = "someCode";
            _hostHelper.SecretCode = secret;

            // Act
            await _hostHelper.ProcessRequestAsync($"/{secret}/{commandId}").ConfigureAwait(false);

            // Assert
            _mocker.Verify<ICommandsManager>(x => x.RunCommand(commandId), Times.Once);
        }

        [Test]
        public async Task ProcessRequestAsync_Invalid_Secret()
        {
            // Arrange                        
            Guid commandId = Guid.NewGuid();
            string secret = "someCode";
            _hostHelper.SecretCode = secret;

            // Act
            await _hostHelper.ProcessRequestAsync($"/invalid/{commandId}").ConfigureAwait(false);

            // Assert
            _mocker.Verify<ICommandsManager>(x => x.RunCommand(commandId), Times.Never);
        }

        [Test]
        public async Task ProcessRequestAsync_No_Secret()
        {
            // Arrange            
            Guid commandId = Guid.NewGuid();

            // Act
            await _hostHelper.ProcessRequestAsync($"/{commandId}").ConfigureAwait(false);

            // Assert
            _mocker.Verify<ICommandsManager>(x => x.RunCommand(commandId), Times.Once);
        }

        [Test]
        public async Task ProcessRequestAsync_Invalid_Command()
        {
            // Arrange            
            Guid commandId = Guid.NewGuid();

            // Act
            await _hostHelper.ProcessRequestAsync("/somecommand").ConfigureAwait(false);

            // Assert
            _mocker.Verify<ICommandsManager>(x => x.RunCommand(commandId), Times.Never);
        }
    }
}
