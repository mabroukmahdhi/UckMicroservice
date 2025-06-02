using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class InvalidTalkException : Xeption
    {
        public InvalidTalkException()
            : base(message: "Invalid talk. Please correct the errors and try again.")
        { }
    }
}