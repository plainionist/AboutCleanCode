using System;
using System.Collections.Generic;

namespace MyApp
{
    public class FriendsRepository
    {
        private Dictionary<Person, List<Person>> myItems = new();

        public void Add(Person person, Person friend) =>
            throw new NotImplementedException();

        public IReadOnlyCollection<Person> Find(Person person) =>
            throw new NotImplementedException();
    }
}