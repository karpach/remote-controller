using System;
using System.Collections.Generic;
using System.Linq;
using Karpach.Remote.Commander.Interfaces;
using Karpach.Remote.Commands.Interfaces;
using Moq;
using NUnit.Framework;

namespace Karpach.Remote.Commander.Tests
{
    [TestFixture]
    public class CommandManagerTests
    {        
        private CommandsManager _commandManager;
        private List<IRemoteCommand> _commands;
        private Mock<ICommandsSettings> _commandSettings;

        [SetUp]
        public void Init()
        {            
            _commands = new List<IRemoteCommand>();
            _commandSettings = new Mock<ICommandsSettings>();            
        }

        [Test]
        public void Default_Creation_Test()
        {
            // Arrange                                    
            Mock<IRemoteCommand> command = new Mock<IRemoteCommand>();
            _commands.AddRange(new []
            {
                command.Object
            });

            // Act               
            _commandManager = new CommandsManager(_commands, _commandSettings.Object);

            // Assert            
            Assert.AreEqual(1, _commandManager.Count);
        }

        [Test]
        public void Two_Not_Configured_Creation_Test()
        {
            // Arrange     
            Mock<IRemoteCommand> command = new Mock<IRemoteCommand>();
            Guid[] ids = {Guid.NewGuid(), Guid.NewGuid()};            
            command.Setup(c => c.Id).Returns(ids[0]);
            _commands.AddRange(new[]
            {
                command.Object
            });
            _commandSettings.Setup(s => s.GetCommandIds(It.IsAny<Type>())).Returns(ids);

            // Act               
            _commandManager = new CommandsManager(_commands, _commandSettings.Object);

            // Assert            
            Assert.AreEqual(ids.Length, _commandManager.Count);
        }

        [Test]
        public void Add_Test()
        {
            // Arrange     
            Mock<IRemoteCommand> command = new Mock<IRemoteCommand>();
            Guid[] ids = { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            command.Setup(c => c.Id).Returns(ids[0]);
            _commands.AddRange(new[]
            {
                command.Object
            });
            _commandSettings.Setup(s => s.GetCommandIds(It.IsAny<Type>())).Returns(ids.Take(2).ToArray);
            Mock<IRemoteCommand> newCommand = new Mock<IRemoteCommand>();
            newCommand.Setup(c => c.Id).Returns(ids[ids.Length-1]);

            // Act               
            _commandManager = new CommandsManager(_commands, _commandSettings.Object);
            _commandManager.Add(newCommand.Object);

            // Assert            
            Assert.AreEqual(ids.Length, _commandManager.Count);
        }
    }
}