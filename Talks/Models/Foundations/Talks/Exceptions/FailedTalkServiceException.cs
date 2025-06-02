using System;
using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class FailedTalkServiceException : Xeption
    {
        public FailedTalkServiceException(Exception innerException)
            : base(message: "Failed talk service occurred, please contact support", innerException)
        { }
    }
}