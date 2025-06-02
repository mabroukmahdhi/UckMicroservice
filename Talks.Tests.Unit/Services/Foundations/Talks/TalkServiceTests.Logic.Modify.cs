using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Talks.Models.Foundations.Talks;
using Xunit;

namespace Talks.Tests.Unit.Services.Foundations.Talks
{
    public partial class TalkServiceTests
    {
        [Fact]
        public async Task ShouldModifyTalkAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            Talk randomTalk = CreateRandomModifyTalk(randomDateTimeOffset);
            Talk inputTalk = randomTalk;
            Talk storageTalk = inputTalk.DeepClone();
            storageTalk.UpdatedDate = randomTalk.CreatedDate;
            Talk updatedTalk = inputTalk;
            Talk expectedTalk = updatedTalk.DeepClone();
            Guid talkId = inputTalk.Id;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectTalkByIdAsync(talkId))
                    .ReturnsAsync(storageTalk);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateTalkAsync(inputTalk))
                    .ReturnsAsync(updatedTalk);

            // when
            Talk actualTalk =
                await this.talkService.ModifyTalkAsync(inputTalk);

            // then
            actualTalk.Should().BeEquivalentTo(expectedTalk);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectTalkByIdAsync(inputTalk.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateTalkAsync(inputTalk),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}