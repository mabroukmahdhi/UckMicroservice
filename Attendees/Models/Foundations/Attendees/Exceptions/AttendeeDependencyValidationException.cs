using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class AttendeeDependencyValidationException : Xeption
    {
        public AttendeeDependencyValidationException(Xeption innerException)
            : base(message: "Attendee dependency validation occurred, please try again.", innerException)
        { }
    }
}