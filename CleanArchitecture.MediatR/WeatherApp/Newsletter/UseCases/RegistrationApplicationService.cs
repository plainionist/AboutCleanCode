using MediatR;
using WeatherApp.Domain;

namespace WeatherApp.Newsletter.UseCases;

internal class RegistrationApplicationService : IRequestHandler<RegistrationRequest, Unit>
{
    private readonly IMediator myMediator;

    public RegistrationApplicationService(IMediator mediator)
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
