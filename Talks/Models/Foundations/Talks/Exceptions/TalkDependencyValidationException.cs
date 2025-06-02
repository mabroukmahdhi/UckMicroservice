using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class TalkDependencyValidationException : Xeption
    {
        public TalkDependencyValidationException(Xeption innerException)
            : base(message: "Talk dependency validation occurred, please try again.", innerException)
        { }
    }
}