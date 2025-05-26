using System;
using Attendees.Models.Foundations.Attendees;
using Attendees.Models.Foundations.Attendees.Exceptions;

namespace Attendees.Services.Foundations.Attendees
{
    public partial class AttendeeService
    {
        private void ValidateAttendeeOnAdd(Attendee attendee)
        {
            ValidateAttendeeIsNotNull(attendee);

            Validate(
                (Rule: IsInvalid(attendee.Id), Parameter: nameof(Attendee.Id)),

                // TODO: Add any other required validation rules

                (Rule: IsInvalid(attendee.CreatedDate), Parameter: nameof(Attendee.CreatedDate)), 
                (Rule: IsInvalid(attendee.UpdatedDate), Parameter: nameof(Attendee.UpdatedDate)), 

                (Rule: IsNotSame(
                    firstDate: attendee.UpdatedDate,
                    secondDate: attendee.CreatedDate,
                    secondDateName: nameof(Attendee.CreatedDate)),
                Parameter: nameof(Attendee.UpdatedDate)),

                (Rule: IsNotRecent(attendee.CreatedDate), Parameter: nameof(Attendee.CreatedDate)));
        }

        private void ValidateAttendeeOnModify(Attendee attendee)
        {
            ValidateAttendeeIsNotNull(attendee);

            Validate(
                (Rule: IsInvalid(attendee.Id), Parameter: nameof(Attendee.Id)),

                // TODO: Add any other required validation rules

                (Rule: IsInvalid(attendee.CreatedDate), Parameter: nameof(Attendee.CreatedDate)), 
                (Rule: IsInvalid(attendee.UpdatedDate), Parameter: nameof(Attendee.UpdatedDate)),

                (Rule: IsSame(
                    firstDate: attendee.UpdatedDate,
                    secondDate: attendee.CreatedDate,
                    secondDateName: nameof(Attendee.CreatedDate)),
                Parameter: nameof(Attendee.UpdatedDate)),

                (Rule: IsNotRecent(attendee.UpdatedDate), Parameter: nameof(attendee.UpdatedDate)));
        }

        public void ValidateAttendeeId(Guid attendeeId) =>
            Validate((Rule: IsInvalid(attendeeId), Parameter: nameof(Attendee.Id)));

        private static void ValidateStorageAttendee(Attendee maybeAttendee, Guid attendeeId)
        {
            if (maybeAttendee is null)
            {
                throw new NotFoundAttendeeException(attendeeId);
            }
        }

        private static void ValidateAttendeeIsNotNull(Attendee attendee)
        {
            if (attendee is null)
            {
                throw new NullAttendeeException();
            }
        } 

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not the same as {secondIdName}"
            };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                this.dateTimeBroker.GetCurrentDateTimeOffset();

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            return timeDifference.Duration() > oneMinute;
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAttendeeException = new InvalidAttendeeException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAttendeeException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAttendeeException.ThrowIfContainsErrors();
        }
    }
}