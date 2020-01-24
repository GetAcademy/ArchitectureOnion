using ArchitectureOnion.Logic;
using ArchitectureOnion.Logic.Interface;
using Moq;
using NUnit.Framework;

namespace ArchitectureOnion.UnitTest
{
    public class PersonServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();
            var service = new PersonService(mockPersonRepository.Object);

            
        }
    }
}