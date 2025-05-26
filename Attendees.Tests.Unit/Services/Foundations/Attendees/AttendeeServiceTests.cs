using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Moq;
using Attendees.Brokers.DateTimes;
using Attendees.Brokers.Loggings;
using Attendees.Brokers.Storages;
using Attendees.Models.Foundations.Attendees; 
using Attendees.Services.Foundations.Attendees;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Attendees.Tests.Unit.Services.Foundations.Attendees
{
    public partial class AttendeeServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly AttendeeService attendeeService;

        public AttendeeServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.attendeeService = new AttendeeService(
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

        private static Attendee CreateRandomAttendee(DateTimeOffset dates) =>
            CreateAttendeeFiller(dates).Create();

        private static Attendee CreateRandomAttendee() =>
            CreateAttendeeFiller(dates: GetRandomDateTimeOffset()).Create();

        private static IQueryable<Attendee> CreateRandomAttendees()
        {
            return CreateAttendeeFiller(GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber())
                    .AsQueryable();
        }

        private static Attendee CreateRandomModifyAttendee(DateTimeOffset dates)
        {
            int randomDaysInPast = GetRandomNumber();
            Attendee randomAttendee = CreateRandomAttendee(dates);

            randomAttendee.CreatedDate =
                randomAttendee.CreatedDate.AddDays(randomDaysInPast);

            return randomAttendee;
        }

        private static Filler<Attendee> CreateAttendeeFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Attendee>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }
    }
}