using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attendees.Models.Foundations.Attendees;

namespace Attendees.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Attendee> Attendees { get; set; }

        public async ValueTask<Attendee> InsertAttendeeAsync(Attendee attendee) =>
            await InsertAsync(attendee);

        public IQueryable<Attendee> SelectAllAttendees() =>
            SelectAll<Attendee>();

        public async ValueTask<Attendee> SelectAttendeeByIdAsync(Guid attendeeId) =>
            await SelectAsync<Attendee>(attendeeId);

        public async ValueTask<Attendee> UpdateAttendeeAsync(Attendee attendee) =>
            await UpdateAsync(attendee);

        public async ValueTask<Attendee> DeleteAttendeeAsync(Attendee attendee) =>
            await DeleteAsync(attendee);

        internal void ConfigureAttendees(EntityTypeBuilder<Attendee> builder)
        {
            // TO DO: Configure the Attendee entity
        }
    }
}
