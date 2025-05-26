using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class NullAttendeeException : Xeption
    {
        public NullAttendeeException()
            : base(message: "Attendee is null.")
        { }
    }
}