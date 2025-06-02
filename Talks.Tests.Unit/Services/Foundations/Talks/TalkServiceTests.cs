using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Moq;
using Talks.Brokers.DateTimes;
using Talks.Brokers.Loggings;
using Talks.Brokers.Storages;
using Talks.Models.Foundations.Talks; 
using Talks.Services.Foundations.Talks;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Talks.Tests.Unit.Services.Foundations.Talks
{
    public partial class TalkServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly TalkService talkService;

        public TalkServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.talkService = new TalkService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<int> MinutesBeforeOrAfter()
        {
            int randomNumber = GetRandomNumber();
            int randomNegativeNumber = GetRandomNegativeNumber();

            return new TheoryData<int>
            {
                randomNumber,
                randomNegativeNumber
            };
        }

        private static SqlException CreateSqlException() =>
            (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static int GetRandomNegativeNumber() =>
            -1 * new IntRange(min: 2, max: 10).GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Exception expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Talk CreateRandomTalk(DateTimeOffset dates) =>
            CreateTalkFiller(dates).Create();

        private static Talk CreateRandomTalk() =>
            CreateTalkFiller(dates: GetRandomDateTimeOffset()).Create();

        private static IQueryable<Talk> CreateRandomTalks()
        {
            return CreateTalkFiller(GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber())
                    .AsQueryable();
        }

        private static Talk CreateRandomModifyTalk(DateTimeOffset dates)
        {
            int randomDaysInPast = GetRandomNumber();
            Talk randomTalk = CreateRandomTalk(dates);

            randomTalk.CreatedDate =
                randomTalk.CreatedDate.AddDays(randomDaysInPast);

            return randomTalk;
        }

        private static Filler<Talk> CreateTalkFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Talk>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }
    }
}