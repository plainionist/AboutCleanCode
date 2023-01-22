using MediatR;

namespace WeatherApp.Mediator.MediatR
{
    public class RequestAdapter<TRequest, TResponse> : IRequest<TResponse>
    {
        public RequestAdapter(TRequest value)
        {
            Value = value;
        }

        public TRequest Value { get; }
    }
}