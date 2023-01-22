using WeatherApp.Domain;
using WeatherApp.Mediator;

namespace WeatherApp.Newsletter.UseCases;

internal class RegistrationApplicationService : IApplicationRequestHandler<RegistrationRequest, Unit>
{
    private readonly IApplicationMediator myMediator;

    public RegistrationApplicationService(IApplicationMediator mediator)
    {
        myMediator = mediator;
    }

    public Task<Unit> Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
        // TODO: validate request and store in database

        if (IsValid(request))
        {
            myMediator.Publish(new RegistrationSucceededDomainEvent(request.User));
        }

        return Task.FromResult(Unit.Value);
    }

    private bool IsValid(RegistrationRequest request)
    {
        // TODO: implement
        return true;
    }
}
