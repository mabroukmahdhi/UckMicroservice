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
        public async Task ShouldAddTalkAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTimeOffset();

            Talk randomTalk = CreateRandomTalk(randomDateTimeOffset);
            Talk inputTalk = randomTalk;
            Talk storageTalk = inputTalk;
            Talk expectedTalk = storageTalk.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertTalkAsync(inputTalk))
                    .ReturnsAsync(storageTalk);

            // when
            Talk actualTalk = await this.talkService
                .AddTalkAsync(inputTalk);

            // then
            actualTalk.Should().BeEquivalentTo(expectedTalk);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTalkAsync(inputTalk),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}