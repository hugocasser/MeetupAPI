namespace Meetup.Info.Services.RabbitMQProcessing;

public enum EventType
{
    SpeakerPublished,
    SpeakerUpdated,
    SpeakerDeleted,
    Undetermined
}