namespace Meetup.Info.Application.Commons.Exceptions;

public class EventRecentlyAddedException:  Exception
{
public EventRecentlyAddedException(string name, DateOnly date, object key)
    :base($"Event \"{name}\" ({key}) recently added at this time:{date}."){}
}