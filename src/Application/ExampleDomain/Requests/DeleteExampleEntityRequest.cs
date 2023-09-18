namespace PayGateMicroService.Application.ExampleDomain.Requests;

public class DeleteExampleEntityRequest
{
    public DeleteExampleEntityRequest(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}