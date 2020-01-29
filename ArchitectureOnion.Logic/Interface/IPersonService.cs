using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArchitectureOnion.Logic.Model;

namespace ArchitectureOnion.Logic.Interface
{
    public interface IPersonService
    {
        Task<Person> GetPerson(int id);
    }
}
