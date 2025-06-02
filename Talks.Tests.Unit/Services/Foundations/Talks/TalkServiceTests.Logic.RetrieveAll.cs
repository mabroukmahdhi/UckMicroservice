using System.Linq;
using FluentAssertions;
using Moq;
using Talks.Models.Foundations.Talks;
using Xunit;

namespace Talks.Tests.Unit.Services.Foundations.Talks
{
    public partial class TalkServiceTests
    {
        [Fact]
        public void ShouldReturnTalks()
        {
            // given
            IQueryable<Talk> randomTalks = CreateRandomTalks();
            IQueryable<Talk> storageTalks = randomTalks;
            IQueryable<Talk> expectedTalks = storageTalks;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllTalks())
                    .Returns(storageTalks);

            // when
            IQueryable<Talk> actualTalks =
                this.talkService.RetrieveAllTalks();

            // then
            actualTalks.Should().BeEquivalentTo(expectedTalks);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllTalks(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}