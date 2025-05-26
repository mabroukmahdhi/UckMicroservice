using System;

namespace Attendees.Models.Foundations.Attendees
{
    public class Attendee
    {
        public Guid Id { get; set; }

        // TODO: Add your properties here. 

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

    }
}
