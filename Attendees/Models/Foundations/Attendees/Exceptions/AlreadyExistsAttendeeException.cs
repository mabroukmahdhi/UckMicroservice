using System;
using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class AlreadyExistsAttendeeException : Xeption
    {
        public AlreadyExistsAttendeeException(Exception innerException)
            : base(message: "Attendee with the same Id already exists.", innerException)
        { }
    }
}