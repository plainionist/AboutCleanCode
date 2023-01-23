using WeatherApp.Domain;

namespace WeatherApp.Newsletter.UseCases;

public interface IRegistrationHandler
{
    void Handle(RegistrationSucceededDomainEvent evt);
}

internal class RegistrationApplicationService
{
    private readonly IEnumerable<IRegistrationHandler> myHandlers;

    public RegistrationApplicationService(IEnumerable<IRegistrationHandler> handlers)
    {
        myHandlers = handlers;
    }

    public void Process(RegistrationRequest request, CancellationToken cancellationToken)
    {
        // TODO: validate request and store in database

        if (IsValid(request))
        {
            foreach (var handler in myHandlers)
            {
                handler.Handle(new RegistrationSucceededDomainEvent(request.User));
            }
        }
    }

    private bool IsValid(RegistrationRequest request)
    {
        // TODO: implement
        return true;
    }
}
