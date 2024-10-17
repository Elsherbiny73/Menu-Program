namespace ClassLibrary1;

public class human
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Class1.Gender Gender { get; set; }

    public human(string name, int age, Class1.Gender gender)
    {
        Name = name;
        Age = age;
        Gender = gender;
    }

    // Override 
    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}, Gender: {Gender}";
    }
}