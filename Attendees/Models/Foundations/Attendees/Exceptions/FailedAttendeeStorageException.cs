using System;
using Xeptions;

namespace Attendees.Models.Foundations.Attendees.Exceptions
{
    public class FailedAttendeeStorageException : Xeption
    {
        public FailedAttendeeStorageException(Exception innerException)
            : base(message: "Failed attendee storage error occurred, contact support.", innerException)
        { }
    }
}