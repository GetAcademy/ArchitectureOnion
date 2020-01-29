using System;

namespace ArchitectureOnion.Logic.Model
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

        public Person[] Children { get; set; } = new Person[0];

        public Person()
        {
        }

        public Person(int id)
        {
            Id = id;
        }
        public Person(int id, string firstName, string lastName, DateTime? birthDate, DateTime? deathDate, Person father, Person mother, Person[] children)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            DeathDate = deathDate;
            Father = father;
            Mother = mother;
            if (children != null)
            {
                Children = children;
            }
        }
    }
}
