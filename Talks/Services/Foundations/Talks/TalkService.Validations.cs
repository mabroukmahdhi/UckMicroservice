using System;
using Talks.Models.Foundations.Talks;
using Talks.Models.Foundations.Talks.Exceptions;

namespace Talks.Services.Foundations.Talks
{
    public partial class TalkService
    {
        private void ValidateTalkOnAdd(Talk talk)
        {
            ValidateTalkIsNotNull(talk);

            Validate(
                (Rule: IsInvalid(talk.Id), Parameter: nameof(Talk.Id)),

                // TODO: Add any other required validation rules

                (Rule: IsInvalid(talk.CreatedDate), Parameter: nameof(Talk.CreatedDate)), 
                (Rule: IsInvalid(talk.UpdatedDate), Parameter: nameof(Talk.UpdatedDate)), 

                (Rule: IsNotSame(
                    firstDate: talk.UpdatedDate,
                    secondDate: talk.CreatedDate,
                    secondDateName: nameof(Talk.CreatedDate)),
                Parameter: nameof(Talk.UpdatedDate)),

                (Rule: IsNotRecent(talk.CreatedDate), Parameter: nameof(Talk.CreatedDate)));
        }

        private void ValidateTalkOnModify(Talk talk)
        {
            ValidateTalkIsNotNull(talk);

            Validate(
                (Rule: IsInvalid(talk.Id), Parameter: nameof(Talk.Id)),

                // TODO: Add any other required validation rules

                (Rule: IsInvalid(talk.CreatedDate), Parameter: nameof(Talk.CreatedDate)), 
                (Rule: IsInvalid(talk.UpdatedDate), Parameter: nameof(Talk.UpdatedDate)),

                (Rule: IsSame(
                    firstDate: talk.UpdatedDate,
                    secondDate: talk.CreatedDate,
                    secondDateName: nameof(Talk.CreatedDate)),
                Parameter: nameof(Talk.UpdatedDate)),

                (Rule: IsNotRecent(talk.UpdatedDate), Parameter: nameof(talk.UpdatedDate)));
        }

        public void ValidateTalkId(Guid talkId) =>
            Validate((Rule: IsInvalid(talkId), Parameter: nameof(Talk.Id)));

        private static void ValidateStorageTalk(Talk maybeTalk, Guid talkId)
        {
            if (maybeTalk is null)
            {
                throw new NotFoundTalkException(talkId);
            }
        }

        private static void ValidateTalkIsNotNull(Talk talk)
        {
            if (talk is null)
            {
                throw new NullTalkException();
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
            var invalidTalkException = new InvalidTalkException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidTalkException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidTalkException.ThrowIfContainsErrors();
        }
    }
}