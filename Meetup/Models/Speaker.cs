namespace Meetup.Models;

public class Speaker
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Presentation { get; set; }
}