using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class AttendeeValidationException : Xeption
    {
        public AttendeeValidationException(Xeption innerException)
            : base(message: "Attendee validation errors occurred, please try again.",
                  innerException)
        { }
    }
}