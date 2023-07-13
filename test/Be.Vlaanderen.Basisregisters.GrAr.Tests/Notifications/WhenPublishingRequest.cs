namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Notifications
{
    using System.Collections.Generic;
    using System.Threading;
    using Amazon.SimpleNotificationService;
    using Amazon.SimpleNotificationService.Model;
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.GrAr.Notifications;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class WhenPublishingRequest
    {
        private readonly string? _expectedMessageType;
        private readonly string? _expectedBasisregistersError;
        private readonly string? _expectedService;
        private readonly NotificationSeverity _expectedNotificationSeverity;
        private readonly Mock<IAmazonSimpleNotificationService> _snsMock;
        private PublishRequest? _publishRequest;

        public WhenPublishingRequest()
        {
            var fixture = new Fixture();

            _snsMock = new Mock<IAmazonSimpleNotificationService>();
            var sut = new NotificationService(_snsMock.Object, "topic");

            _publishRequest = null;

            _snsMock.Setup(x => x.PublishAsync(It.IsAny<PublishRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PublishResponse())
                .Callback<PublishRequest, CancellationToken>((pr,
                    _) => _publishRequest = pr);

            _expectedMessageType = fixture.Create<string>();
            _expectedBasisregistersError = fixture.Create<string>();
            _expectedService = fixture.Create<string>();
            _expectedNotificationSeverity = fixture.Create<NotificationSeverity>();

            var notificationMessage = new NotificationMessage(
                _expectedMessageType,
                _expectedBasisregistersError,
                _expectedService,
                _expectedNotificationSeverity);

            sut.PublishToTopicAsync(notificationMessage).GetAwaiter().GetResult();
        }

        [Fact]
        public void ThenVerifyRequestIsPublished()
        {
            _snsMock.Verify(x => x.PublishAsync(It.IsAny<PublishRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void ThenMessageIsExpected()
        {
            _publishRequest.Should().NotBeNull();
            _publishRequest.Message.Should().Be(
                $"{{\"basisregistersError\":\"{_expectedBasisregistersError}\",\"service\":\"{_expectedService}\",\"warning\":\"{_expectedNotificationSeverity.ToString().ToLowerInvariant()}\"}}");
        }

        [Fact]
        public void ThenTopicArnIsExpected()
        {
            _publishRequest.Should().NotBeNull();
            _publishRequest.TopicArn.Should().Be("topic");
        }

        [Fact]
        public void ThenMessageAttributesAreExpected()
        {
            var expectedMessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                { "MessageType", new MessageAttributeValue { DataType = "String", StringValue = _expectedMessageType } },
                { "service", new MessageAttributeValue { DataType = "String", StringValue = _expectedService } },
                { "warning", new MessageAttributeValue { DataType = "String", StringValue = _expectedNotificationSeverity.ToString().ToLowerInvariant() } }
            };

            _publishRequest.Should().NotBeNull();
            _publishRequest.MessageAttributes.Should().BeEquivalentTo(expectedMessageAttributes);
        }
    }
}
