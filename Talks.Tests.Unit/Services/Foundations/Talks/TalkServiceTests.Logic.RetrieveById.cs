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
        public async Task ShouldRetrieveTalkByIdAsync()
        {
            // given
            Talk randomTalk = CreateRandomTalk();
            Talk inputTalk = randomTalk;
            Talk storageTalk = randomTalk;
            Talk expectedTalk = storageTalk.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectTalkByIdAsync(inputTalk.Id))
                    .ReturnsAsync(storageTalk);

            // when
            Talk actualTalk =
                await this.talkService.RetrieveTalkByIdAsync(inputTalk.Id);

            // then
            actualTalk.Should().BeEquivalentTo(expectedTalk);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectTalkByIdAsync(inputTalk.Id),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}