using MediatR;

namespace Meetup.Info.Application.Commands.DeleteEvent;

public class DeleteEventCommand: IRequest
{
    public required Guid Id { get; set; }
}