namespace ExampleProject.Application.ExampleDomain.Dtos;

public class ExampleEntityDto
{
    public ExampleEntityDto(Guid id, string name, DateTime registrationDate)
    {
        Id = id;
        Name = name;
        RegistrationDate = registrationDate;
    }

    public Guid Id { get; }
    public string Name { get; }
    public DateTime RegistrationDate { get; }
}