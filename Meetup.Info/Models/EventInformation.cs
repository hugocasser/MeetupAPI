namespace Meetup.Info.Models;

public class EventInformation
{
   public required Guid Id { get; set; }
   public required string Title { get; set; }
   public required DateOnly EventDate { get; set; }
   public Speaker Speaker { get; set; }
   public Guid SpeakerId { get; set; } 
   public required string Place { get; set; }
}