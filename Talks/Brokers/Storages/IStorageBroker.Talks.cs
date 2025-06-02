using System;
using System.Linq;
using System.Threading.Tasks;
using Talks.Models.Foundations.Talks;

namespace Talks.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Talk> InsertTalkAsync(Talk talk);
        IQueryable<Talk> SelectAllTalks();
        ValueTask<Talk> SelectTalkByIdAsync(Guid talkId);
        ValueTask<Talk> UpdateTalkAsync(Talk talk);
        ValueTask<Talk> DeleteTalkAsync(Talk talk);
    }
}
