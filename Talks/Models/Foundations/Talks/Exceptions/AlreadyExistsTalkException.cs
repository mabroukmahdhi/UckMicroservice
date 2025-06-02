using System;
using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class AlreadyExistsTalkException : Xeption
    {
        public AlreadyExistsTalkException(Exception innerException)
            : base(message: "Talk with the same Id already exists.", innerException)
        { }
    }
}