using System;
using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class FailedTalkStorageException : Xeption
    {
        public FailedTalkStorageException(Exception innerException)
            : base(message: "Failed talk storage error occurred, contact support.", innerException)
        { }
    }
}