using System;
using AutoInject.Attributes.TransientAttributes;

namespace Talks.Brokers.DateTimes
{
    [Transient(typeof(IDateTimeBroker))]
    public class DateTimeBroker : IDateTimeBroker
    {
        public DateTimeOffset GetCurrentDateTimeOffset() =>
            DateTimeOffset.UtcNow;
    }
}