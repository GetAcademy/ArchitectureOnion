
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureOnion.Logic.Interface;
using ArchitectureOnion.Logic.Model;

namespace ArchitectureOnion.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        private readonly Repository<DataAccess.Model.Person> _dbRepository;

        public PersonRepository()
        {
            var connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ArchitectureDemo;Integrated Security=True;";
            _dbRepository = new Repository<Model.Person>(connStr);
        }

        public async Task<IEnumerable<Person>> ReadChildren(int parentId)
        {
            var whereClause = " Father = @Id OR Mother = @Id ";
            var children = await _dbRepository.ReadMany(whereClause, new { Id = parentId });
            return children.Select(DomainPersonFromDbPerson);
        }

        private Person DomainPersonFromDbPerson(Model.Person dbPerson)
        {
            return new Person(
                dbPerson.Id, 
                dbPerson.FirstName, 
                dbPerson.LastName, 
                dbPerson.BirthDate, 
                dbPerson.DeathDate, 
                !dbPerson.Father.HasValue ? null : new Person(dbPerson.Father.Value), 
                !dbPerson.Mother.HasValue ? null : new Person(dbPerson.Mother.Value), 
                null);
        }

        private DataAccess.Model.Person DbPersonFromDomainPerson(Person person)
        {
            return new DataAccess.Model.Person(
                person.Id, 
                person.FirstName,
                person.LastName,
                person.BirthDate,
                person.DeathDate,
                person.Father?.Id,
                person.Mother?.Id
                );
        }

        public async Task<int> Create(Person obj)
        {
            return await _dbRepository.Create(DbPersonFromDomainPerson(obj));
        }

        public async Task<IEnumerable<Person>> ReadMany(string whereClause = null, object parameter = null)
        {
            var dbPersons = await _dbRepository.ReadMany(whereClause, parameter);
            return dbPersons.Select(DomainPersonFromDbPerson);
        }

        public async Task<Person> ReadOneById(int id)
        {
            var dbPerson = await _dbRepository.ReadOneById(id);
            return DomainPersonFromDbPerson(dbPerson);
        }

        public async Task<int> Update(Person person)
        {
            return await _dbRepository.Update(DbPersonFromDomainPerson(person));
        }

        public async Task<int> Delete(Person obj = null, int? id = null)
        {
            return await _dbRepository.Delete(null, obj?.Id);
        }
    }
}
