using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectureOnion.DataAccess.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }

        public int? Father { get; set; }
        public int? Mother { get; set; }

        public Person(int id, string firstName, string lastName, DateTime? birthDate, DateTime? deathDate, int? father, int? mother)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            DeathDate = deathDate;
            Father = father;
            Mother = mother;
        }
    }
}
