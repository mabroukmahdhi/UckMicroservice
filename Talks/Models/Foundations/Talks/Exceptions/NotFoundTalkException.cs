using System;
using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class NotFoundTalkException : Xeption
    {
        public NotFoundTalkException(Guid talkId)
            : base(message: $"Couldn't find talk with talkId: {talkId}.")
        { }
    }
}