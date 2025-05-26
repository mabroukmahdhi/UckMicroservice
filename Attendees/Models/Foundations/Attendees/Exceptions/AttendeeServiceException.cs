using System;
using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class AttendeeServiceException : Xeption
    {
        public AttendeeServiceException(Exception innerException)
            : base(message: "Attendee service error occurred, contact support.", innerException)
        { }
    }
}