using System;
using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class FailedAttendeeServiceException : Xeption
    {
        public FailedAttendeeServiceException(Exception innerException)
            : base(message: "Failed attendee service occurred, please contact support", innerException)
        { }
    }
}