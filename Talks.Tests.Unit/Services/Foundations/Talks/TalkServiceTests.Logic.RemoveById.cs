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
        public async Task ShouldRemoveTalkByIdAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputTalkId = randomId;
            Talk randomTalk = CreateRandomTalk();
            Talk storageTalk = randomTalk;
            Talk expectedInputTalk = storageTalk;
            Talk deletedTalk = expectedInputTalk;
            Talk expectedTalk = deletedTalk.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectTalkByIdAsync(inputTalkId))
                    .ReturnsAsync(storageTalk);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteTalkAsync(expectedInputTalk))
                    .ReturnsAsync(deletedTalk);

            // when
            Talk actualTalk = await this.talkService
                .RemoveTalkByIdAsync(inputTalkId);

            // then
            actualTalk.Should().BeEquivalentTo(expectedTalk);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectTalkByIdAsync(inputTalkId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteTalkAsync(expectedInputTalk),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}