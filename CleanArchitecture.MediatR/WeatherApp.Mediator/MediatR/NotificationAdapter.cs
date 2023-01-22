using MediatR;

namespace WeatherApp.Mediator.MediatR
{
    public class NotificationAdapter<TNotification> : INotification
    {
        public NotificationAdapter(TNotification value)
        {
            Value = value;
        }

        public TNotification Value {get;}
    }
}