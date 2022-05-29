using System;

namespace MyApp
{
    public class ImmutablePerson
    {
        public ImmutablePerson(string firstName, string secondName, string lastName)
        {
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string SecondName { get; }
        public string LastName { get; }

        public override bool Equals(object obj) =>
            obj != null && obj is Person
                && obj.GetHashCode() == GetHashCode();

        public override int GetHashCode() =>
            HashCode.Combine(FirstName, LastName);
    }
}
