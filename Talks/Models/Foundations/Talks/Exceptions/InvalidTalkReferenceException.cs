using System;
using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class InvalidTalkReferenceException : Xeption
    {
        public InvalidTalkReferenceException(Exception innerException)
            : base(message: "Invalid talk reference error occurred.", innerException) { }
    }
}