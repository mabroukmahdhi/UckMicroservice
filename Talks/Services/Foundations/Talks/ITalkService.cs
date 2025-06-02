using System;
using System.Linq;
using System.Threading.Tasks;
using Talks.Models.Foundations.Talks;

namespace Talks.Services.Foundations.Talks
{
    public interface ITalkService
    {
        ValueTask<Talk> AddTalkAsync(Talk talk);
        IQueryable<Talk> RetrieveAllTalks();
        ValueTask<Talk> RetrieveTalkByIdAsync(Guid talkId);
        ValueTask<Talk> ModifyTalkAsync(Talk talk);
        ValueTask<Talk> RemoveTalkByIdAsync(Guid talkId);
    }
}