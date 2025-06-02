using System;
using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class TalkServiceException : Xeption
    {
        public TalkServiceException(Exception innerException)
            : base(message: "Talk service error occurred, contact support.", innerException)
        { }
    }
}