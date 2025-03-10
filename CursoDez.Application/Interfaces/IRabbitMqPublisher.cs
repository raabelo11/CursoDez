namespace CursoDez.Application.Interfaces
{
    public interface IRabbitMqPublisher
    {
        Task PublishMessage(string message);
    }
}
