using System;
using System.Linq;
using System.Threading.Tasks;
using Attendees.Models.Foundations.Attendees;

namespace Attendees.Services.Foundations.Attendees
{
    public interface IAttendeeService
    {
        ValueTask<Attendee> AddAttendeeAsync(Attendee attendee);
        IQueryable<Attendee> RetrieveAllAttendees();
        ValueTask<Attendee> RetrieveAttendeeByIdAsync(Guid attendeeId);
        ValueTask<Attendee> ModifyAttendeeAsync(Attendee attendee);
        ValueTask<Attendee> RemoveAttendeeByIdAsync(Guid attendeeId);
    }
}