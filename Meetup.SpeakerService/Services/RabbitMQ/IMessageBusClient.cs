using Meetup.SpeakerService.Application.DTOs;

namespace Meetup.SpeakerService.Services.RabbitMQ;

public interface IMessageBusClient
{
    public void PublishSpeaker(SpeakerPublishDTO speakerPublishDTO);
    public void PublishUpdateSpeaker(SpeakerPublishDTO speakerPublishDTO);
    public void PublishDeleteSpeaker(SpeakerDeleteDTO speakerDeleteDTO);
}