using Xeptions;

namespace Talks.Models.Foundations.Talks.Exceptions
{
    public class TalkDependencyException : Xeption
    {
        public TalkDependencyException(Xeption innerException) :
            base(message: "Talk dependency error occurred, contact support.", innerException)
        { }
    }
}