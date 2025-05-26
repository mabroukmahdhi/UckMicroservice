using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class AttendeeDependencyException : Xeption
    {
        public AttendeeDependencyException(Xeption innerException) :
            base(message: "Attendee dependency error occurred, contact support.", innerException)
        { }
    }
}