using System;
using System.Linq;
using System.Threading.Tasks;
using AutoInject.Attributes.TransientAttributes;
using Talks.Brokers.DateTimes;
using Talks.Brokers.Loggings;
using Talks.Brokers.Storages;
using Talks.Models.Foundations.Talks;

namespace Talks.Services.Foundations.Talks
{
    [Transient(typeof(ITalkService))]
    public partial class TalkService : ITalkService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public TalkService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Talk> AddTalkAsync(Talk talk) =>
            TryCatch(async () =>
            {
                ValidateTalkOnAdd(talk);

                return await this.storageBroker.InsertTalkAsync(talk);
            });

        public IQueryable<Talk> RetrieveAllTalks() =>
            TryCatch(() => this.storageBroker.SelectAllTalks());

        public ValueTask<Talk> RetrieveTalkByIdAsync(Guid talkId) =>
            TryCatch(async () =>
            {
                ValidateTalkId(talkId);

                Talk maybeTalk = await this.storageBroker
                    .SelectTalkByIdAsync(talkId);

                ValidateStorageTalk(maybeTalk, talkId);

                return maybeTalk;
            });

        public ValueTask<Talk> ModifyTalkAsync(Talk talk) =>
            TryCatch(async () =>
            {
                ValidateTalkOnModify(talk);

                Talk maybeTalk =
                    await this.storageBroker.SelectTalkByIdAsync(talk.Id);

                ValidateStorageTalk(maybeTalk, talk.Id);
                 
                return await this.storageBroker.UpdateTalkAsync(talk);
            });

        public ValueTask<Talk> RemoveTalkByIdAsync(Guid talkId) =>
            TryCatch(async () =>
            {
                ValidateTalkId(talkId);

                Talk maybeTalk = await this.storageBroker
                    .SelectTalkByIdAsync(talkId);

                ValidateStorageTalk(maybeTalk, talkId);

                return await this.storageBroker.DeleteTalkAsync(maybeTalk);
            });
    }
}