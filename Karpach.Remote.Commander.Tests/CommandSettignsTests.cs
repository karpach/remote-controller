using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Karpach.Remote.Commander.Tests
{
    [TestFixture]
    public class CommandSettignsTests
    {
        private Guid[] _persons;
        private Guid[] _entities;

        [SetUp]
        public void Setup()
        {
            _persons = new[]
            {
                Guid.Parse("00000001-AD91-4C5A-AF4C-B120008671F0"),
                Guid.Parse("00000002-AD91-4C5A-AF4C-B120008671F0"),
                Guid.Parse("00000003-AD91-4C5A-AF4C-B120008671F0")
            };
            _entities = new[]
            {
                Guid.Parse("10000001-AD91-4C5A-AF4C-B120008671F0"),
                Guid.Parse("10000002-AD91-4C5A-AF4C-B120008671F0")
            };
        }

        [Test]
        public void Adds_Test()
        {
            // Arrange
            string tempPath = Path.GetTempFileName();

            // Act
            var settings = new CommandsSettings(tempPath);
            settings.Add(typeof(Person), _persons[0]);
            settings.Add(typeof(Person), _persons[1]);
            settings.Add(typeof(Entity), _entities[0]);
            settings.Add(typeof(Person), _persons[2]);
            settings.Add(typeof(Entity), _entities[1]);

            // Arrange
            Assert.AreEqual(settings.GetCommandIds(typeof(Person)).Length, _persons.Length);
            Assert.AreEqual(settings.GetCommandIds(typeof(Entity)).Length, _entities.Length);
            Console.WriteLine(File.ReadAllText(tempPath));
        }

        [Test]
        public void Read_Test()
        {
            // Arrange
            string tempPath = Path.GetTempFileName();
            var initSettings = new CommandsSettings(tempPath);
            initSettings.Add(typeof(Person), _persons[0]);
            initSettings.Add(typeof(Person), _persons[1]);
            initSettings.Add(typeof(Entity), _entities[0]);
            initSettings.Add(typeof(Person), _persons[2]);
            initSettings.Add(typeof(Entity), _entities[1]);

            // Act      
            var settings = new CommandsSettings(tempPath);

            // Arrange
            Guid[] persons = settings.GetCommandIds(typeof(Person));
            Assert.AreEqual(persons.Length, _persons.Length);
            Assert.IsTrue(persons.All(p=>_persons.Contains(p)));
            Guid[] entities = settings.GetCommandIds(typeof(Entity));
            Assert.AreEqual(entities.Length, _entities.Length);
            Assert.IsTrue(entities.All(e => _entities.Contains(e)));
        }

        [Test]
        public void Remove_Test()
        {
            // Arrange
            string tempPath = Path.GetTempFileName();
            var settings = new CommandsSettings(tempPath);
            settings.Add(typeof(Person), _persons[0]);
            settings.Add(typeof(Person), _persons[1]);
            settings.Add(typeof(Entity), _entities[0]);
            settings.Add(typeof(Person), _persons[2]);
            settings.Add(typeof(Entity), _entities[1]);

            // Act
            settings.Remove(typeof(Person), _persons[1]);            

            // Arrange
            Assert.AreEqual(settings.GetCommandIds(typeof(Person)).Length, _persons.Length - 1);
            Assert.AreEqual(settings.GetCommandIds(typeof(Entity)).Length, _entities.Length);
            Console.WriteLine(File.ReadAllText(tempPath));
        }

        private class Person
        {
        }

        private class Entity
        {
        }
    }
}