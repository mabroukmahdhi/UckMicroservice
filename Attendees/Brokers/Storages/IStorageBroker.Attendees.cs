using System;
using System.Linq;
using System.Threading.Tasks;
using Attendees.Models.Foundations.Attendees;

namespace Attendees.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Attendee> InsertAttendeeAsync(Attendee attendee);
        IQueryable<Attendee> SelectAllAttendees();
        ValueTask<Attendee> SelectAttendeeByIdAsync(Guid attendeeId);
        ValueTask<Attendee> UpdateAttendeeAsync(Attendee attendee);
        ValueTask<Attendee> DeleteAttendeeAsync(Attendee attendee);
    }
}
