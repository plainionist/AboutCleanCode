namespace AutoCtor;

[AutoConstructor]
public partial class Person
{
    public string FirstName { get; }
    public string LastName { get; }
}

//  [AutoConstructor]
//  public partial record Person(string FirstName, string LastName);


