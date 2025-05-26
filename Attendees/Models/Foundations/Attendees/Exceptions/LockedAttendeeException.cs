using System;
using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class LockedAttendeeException : Xeption
    {
        public LockedAttendeeException(Exception innerException)
            : base(message: "Locked attendee record exception, please try again later", innerException)
        {
        }
    }
}