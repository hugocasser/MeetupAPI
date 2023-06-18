namespace Meetup.SpeakerService.Application.Common.CustomExceptions;

public class NotFoundException<T> : Exception
{
    public  NotFoundException(object key) : base($"Entity {typeof(T).FullName}, key:{key} was not found"){}
}