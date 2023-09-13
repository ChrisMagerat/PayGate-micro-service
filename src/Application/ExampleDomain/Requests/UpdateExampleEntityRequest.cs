namespace ExampleProject.Application.ExampleDomain.Requests;

public class UpdateExampleEntityRequest
{
    public UpdateExampleEntityRequest(string entityName, DateTime registrationDate)
    {
        EntityName = entityName;
        RegistrationDate = registrationDate;
    }
    public string EntityName { get; set; }
    public DateTime RegistrationDate { get; set; }
}