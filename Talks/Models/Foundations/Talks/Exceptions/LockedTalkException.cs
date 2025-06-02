using System;
using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class LockedTalkException : Xeption
    {
        public LockedTalkException(Exception innerException)
            : base(message: "Locked talk record exception, please try again later", innerException)
        {
        }
    }
}