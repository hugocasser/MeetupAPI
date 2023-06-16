namespace Meetup.Info.Services.RabbitMQProcessing;

public enum EventType
{
    AuthorPublished,
    AuthorUpdated,
    AuthorDeleted,
    Undetermined
}