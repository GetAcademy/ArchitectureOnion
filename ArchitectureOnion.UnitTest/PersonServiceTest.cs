using System.Threading.Tasks;
using ArchitectureOnion.Logic;
using ArchitectureOnion.Logic.Interface;
using ArchitectureOnion.Logic.Model;
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
        public async Task Test1()
        {
            var mock = new Mock<IPersonRepository>();
            mock.Setup(obj => obj.ReadOneById(1))
                .ReturnsAsync(new Person
                {
                    Id = 1,
                    FirstName = "Dummy Person",
                    Father = new Person() { Id=2}
                });
            mock.Setup(obj => obj.ReadOneById(2))
                .ReturnsAsync(new Person
                {
                    Id = 2,
                    FirstName = "Dummy Father"
                });
            var service = new PersonService(mock.Object);
            var person = await service.GetPerson(1);
            Assert.AreEqual(person.Id,1);
            Assert.AreEqual(person.FirstName,"Dummy Person");
            Assert.NotNull(person.Father);
            Assert.AreEqual(person.Father.Id, 2);
            Assert.AreEqual(person.Father.FirstName, "Dummy Father");
        }
    }
}