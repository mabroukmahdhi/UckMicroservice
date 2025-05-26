using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class InvalidAttendeeException : Xeption
    {
        public InvalidAttendeeException()
            : base(message: "Invalid attendee. Please correct the errors and try again.")
        { }
    }
}