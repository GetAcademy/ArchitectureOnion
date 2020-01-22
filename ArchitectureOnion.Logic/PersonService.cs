using System.Threading.Tasks;
using ArchitectureOnion.Logic.Model;

namespace ArchitectureNTier.Logic
{
    public class PersonService
    {
        public async Task<Person> GetPerson(int id)
        {
            var connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ArchitectureDemo;Integrated Security=True;";
            var repo = new PersonRepository(connStr);
            var dbPerson = await repo.ReadOneById(id);
            var person = LogicPersonFromDbPerson(dbPerson);
            person.Father = await ReadParent(dbPerson.Father, repo);
            person.Mother = await ReadParent(dbPerson.Mother, repo);
            var dbChildren = await repo.ReadChildren(id);
            person.Children = dbChildren.Select(LogicPersonFromDbPerson).ToArray();
            return person;
        }

        private static async Task<Person> ReadParent(int? parentId, PersonRepository repo)
        {
            if (parentId == null) return null;
            var dbParent = await repo.ReadOneById(parentId.Value);
            return LogicPersonFromDbPerson(dbParent);
        }

        private static Person LogicPersonFromDbPerson(Data.Model.Person dbPerson)
        {
            return new Person
            {
                FirstName = dbPerson.FirstName,
                LastName = dbPerson.LastName,
                BirthDate = dbPerson.BirthDate,
                DeathDate = dbPerson.DeathDate
            };
        }
    }
}
