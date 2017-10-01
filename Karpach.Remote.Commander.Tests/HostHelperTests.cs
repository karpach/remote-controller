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
        
    }
}
