using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureOnion.Logic.Model;

namespace ArchitectureOnion.Logic.Interface
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> ReadChildren(int parentId);
        Task<int> Create(Person obj);

        Task<IEnumerable<Person>> ReadMany(
            string whereClause = null,
            object parameter = null);

        Task<Person> ReadOneById(int id);
        Task<int> Update(Person person);
        Task<int> Delete(Person obj = null, int? id = null);
    }
}
