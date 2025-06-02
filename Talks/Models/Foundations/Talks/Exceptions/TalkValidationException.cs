using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class TalkValidationException : Xeption
    {
        public TalkValidationException(Xeption innerException)
            : base(message: "Talk validation errors occurred, please try again.",
                  innerException)
        { }
    }
}