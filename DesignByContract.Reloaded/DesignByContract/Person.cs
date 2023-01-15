using System;

namespace DesignByContract;

public class Person
{
    public Person(string firstName, string lastName, Address address)
    {
        Contract.RequiresNotNullNotEmpty(firstName, nameof(firstName));
        Contract.RequiresNotNullNotEmpty(lastName, nameof(lastName));
        Contract.RequiresNotNull(address);

        Contract.Requires(lastName.Length <= 20);

        FirstName = firstName;
        LastName = lastName;
        Address = address;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public Address Address { get; }
}
