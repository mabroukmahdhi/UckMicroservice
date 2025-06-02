using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class NullTalkException : Xeption
    {
        public NullTalkException()
            : base(message: "Talk is null.")
        { }
    }
}