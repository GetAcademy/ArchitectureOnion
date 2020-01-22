using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectureNTier.Logic.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }

        public Person Father { get; set; }
        public Person Mother { get; set; }

        public Person[] Children { get; set; }
    }
}
