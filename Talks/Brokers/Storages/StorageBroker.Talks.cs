using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talks.Models.Foundations.Talks;

namespace Talks.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Talk> Talks { get; set; }

        public async ValueTask<Talk> InsertTalkAsync(Talk talk) =>
            await InsertAsync(talk);

        public IQueryable<Talk> SelectAllTalks() =>
            SelectAll<Talk>();

        public async ValueTask<Talk> SelectTalkByIdAsync(Guid talkId) =>
            await SelectAsync<Talk>(talkId);

        public async ValueTask<Talk> UpdateTalkAsync(Talk talk) =>
            await UpdateAsync(talk);

        public async ValueTask<Talk> DeleteTalkAsync(Talk talk) =>
            await DeleteAsync(talk);

        internal void ConfigureTalks(EntityTypeBuilder<Talk> builder)
        {
            // TO DO: Configure the Talk entity
        }
    }
}
