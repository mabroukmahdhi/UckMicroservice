using System;
using System.Linq;
using System.Threading.Tasks;
using AutoInject.Attributes.TransientAttributes;
using Attendees.Brokers.DateTimes;
using Attendees.Brokers.Loggings;
using Attendees.Brokers.Storages;
using Attendees.Models.Foundations.Attendees;

namespace Attendees.Services.Foundations.Attendees
{
    [Transient(typeof(IAttendeeService))]
    public partial class AttendeeService : IAttendeeService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public AttendeeService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Attendee> AddAttendeeAsync(Attendee attendee) =>
            TryCatch(async () =>
            {
                ValidateAttendeeOnAdd(attendee);

                return await this.storageBroker.InsertAttendeeAsync(attendee);
            });

        public IQueryable<Attendee> RetrieveAllAttendees() =>
            TryCatch(() => this.storageBroker.SelectAllAttendees());

        public ValueTask<Attendee> RetrieveAttendeeByIdAsync(Guid attendeeId) =>
            TryCatch(async () =>
            {
                ValidateAttendeeId(attendeeId);

                Attendee maybeAttendee = await this.storageBroker
                    .SelectAttendeeByIdAsync(attendeeId);

                ValidateStorageAttendee(maybeAttendee, attendeeId);

                return maybeAttendee;
            });

        public ValueTask<Attendee> ModifyAttendeeAsync(Attendee attendee) =>
            TryCatch(async () =>
            {
                ValidateAttendeeOnModify(attendee);

                Attendee maybeAttendee =
                    await this.storageBroker.SelectAttendeeByIdAsync(attendee.Id);

                ValidateStorageAttendee(maybeAttendee, attendee.Id);
                 
                return await this.storageBroker.UpdateAttendeeAsync(attendee);
            });

        public ValueTask<Attendee> RemoveAttendeeByIdAsync(Guid attendeeId) =>
            TryCatch(async () =>
            {
                ValidateAttendeeId(attendeeId);

                Attendee maybeAttendee = await this.storageBroker
                    .SelectAttendeeByIdAsync(attendeeId);

                ValidateStorageAttendee(maybeAttendee, attendeeId);

                return await this.storageBroker.DeleteAttendeeAsync(maybeAttendee);
            });
    }
}