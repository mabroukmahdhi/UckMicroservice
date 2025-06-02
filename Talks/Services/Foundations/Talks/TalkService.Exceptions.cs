using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Talks.Models.Foundations.Talks;
using Talks.Models.Foundations.Talks.Exceptions;
using Xeptions;

namespace Talks.Services.Foundations.Talks
{
    public partial class TalkService
    {
        private delegate ValueTask<Talk> ReturningTalkFunction();
        private delegate IQueryable<Talk> ReturningTalksFunction();

        private async ValueTask<Talk> TryCatch(ReturningTalkFunction returningTalkFunction)
        {
            try
            {
                return await returningTalkFunction();
            }
            catch (NullTalkException nullTalkException)
            {
                throw CreateAndLogValidationException(nullTalkException);
            }
            catch (InvalidTalkException invalidTalkException)
            {
                throw CreateAndLogValidationException(invalidTalkException);
            }
            catch (SqlException sqlException)
            {
                var failedTalkStorageException =
                    new FailedTalkStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedTalkStorageException);
            }
            catch (NotFoundTalkException notFoundTalkException)
            {
                throw CreateAndLogValidationException(notFoundTalkException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsTalkException =
                    new AlreadyExistsTalkException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsTalkException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidTalkReferenceException =
                    new InvalidTalkReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidTalkReferenceException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedTalkException = new LockedTalkException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedTalkException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedTalkStorageException =
                    new FailedTalkStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedTalkStorageException);
            }
            catch (Exception exception)
            {
                var failedTalkServiceException =
                    new FailedTalkServiceException(exception);

                throw CreateAndLogServiceException(failedTalkServiceException);
            }
        }

        private IQueryable<Talk> TryCatch(ReturningTalksFunction returningTalksFunction)
        {
            try
            {
                return returningTalksFunction();
            }
            catch (SqlException sqlException)
            {
                var failedTalkStorageException =
                    new FailedTalkStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedTalkStorageException);
            }
            catch (Exception exception)
            {
                var failedTalkServiceException =
                    new FailedTalkServiceException(exception);

                throw CreateAndLogServiceException(failedTalkServiceException);
            }
        }

        private TalkValidationException CreateAndLogValidationException(Xeption exception)
        {
            var talkValidationException =
                new TalkValidationException(exception);

            this.loggingBroker.LogError(talkValidationException);

            return talkValidationException;
        }

        private TalkDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var talkDependencyException = new TalkDependencyException(exception);
            this.loggingBroker.LogCritical(talkDependencyException);

            return talkDependencyException;
        }

        private TalkDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var talkDependencyValidationException =
                new TalkDependencyValidationException(exception);

            this.loggingBroker.LogError(talkDependencyValidationException);

            return talkDependencyValidationException;
        }

        private TalkDependencyException CreateAndLogDependencyException(
            Xeption exception)
        {
            var talkDependencyException = new TalkDependencyException(exception);
            this.loggingBroker.LogError(talkDependencyException);

            return talkDependencyException;
        }

        private TalkServiceException CreateAndLogServiceException(
            Xeption exception)
        {
            var talkServiceException = new TalkServiceException(exception);
            this.loggingBroker.LogError(talkServiceException);

            return talkServiceException;
        }
    }
}