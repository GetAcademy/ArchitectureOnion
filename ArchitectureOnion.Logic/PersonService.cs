using System.Linq;
using System.Threading.Tasks;
using ArchitectureOnion.Logic.Interface;
using ArchitectureOnion.Logic.Model;

namespace ArchitectureOnion.Logic
{
    public class PersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> GetPerson(int id)
        {
            var person = await _personRepository.ReadOneById(id);
            if (person.Father != null)
            {
                person.Father = await _personRepository.ReadOneById(person.Father.Id);
            }
            if (person.Mother != null)
            {
                person.Mother = await _personRepository.ReadOneById(person.Mother.Id);
            }
            var children = await _personRepository.ReadChildren(id);
            person.Children = children.ToArray();
            return person;
        }
    }
}
