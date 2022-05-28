using System;

namespace MyApp
{
    public class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }

        public override bool Equals(object obj) =>
            obj != null &&  obj is Person 
                && obj.GetHashCode() == GetHashCode();

        public override int GetHashCode() =>
            HashCode.Combine(FirstName, LastName);
    }
}
