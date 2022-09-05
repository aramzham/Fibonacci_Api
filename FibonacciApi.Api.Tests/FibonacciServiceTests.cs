using System.ComponentModel.DataAnnotations;
using FibonacciApi.Api.Infrastructure.Models;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Tests;

public class FibonacciServiceTests
{
    private readonly Mock<IMemoryChecker> _mockMemoryChecker = new();
    private readonly Mock<IExecutionTimeChecker> _mockTimeChecker = new();
    private readonly Mock<ICacheManger> _mockCacheManager = new();
    private readonly Fixture _fixture = new();
    private readonly FibonacciService _sut;

    public FibonacciServiceTests()
    {
        _sut = new FibonacciService(_mockMemoryChecker.Object, _mockTimeChecker.Object, _mockCacheManager.Object);
    }

    [Fact]
    public async Task GetSubsequence_WhenFirstIndexIsNegative_ThrowsException()
    {
        // arrange
        var firstIndex = Math.Abs(_fixture.Create<int>()) * -1;
        _mockTimeChecker.Setup(_ => _.Run());

        // act
        Func<Task> func = async () =>
            await _sut.GetSubsequence(firstIndex, It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>(),
                It.IsAny<double>());

        // assert
        await func.Should().ThrowAsync<Exception>().WithMessage("indexes cannot be negative");
    }

    [Fact]
    public async Task GetSubsequence_WhenLastIndexIsNegative_ThrowsException()
    {
        // arrange
        var firstIndex = Math.Abs(_fixture.Create<int>());
        var lastIndex = Math.Abs(_fixture.Create<int>()) * -1;
        _mockTimeChecker.Setup(_ => _.Run());

        // act
        Func<Task> func = async () =>
            await _sut.GetSubsequence(firstIndex, lastIndex, It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<double>());

        // assert
        await func.Should().ThrowAsync<Exception>().WithMessage("indexes cannot be negative");
    }

    [Fact]
    public async Task GetSubsequence_WhenFirstIndexIsGraterThanLast_ThrowsException()
    {
        // arrange
        var lastIndex = Math.Abs(_fixture.Create<int>());
        var firstIndex = lastIndex + 1;
        _mockTimeChecker.Setup(_ => _.Run());

        // act
        Func<Task> func = async () =>
            await _sut.GetSubsequence(firstIndex, lastIndex, It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<double>());

        // assert
        await func.Should().ThrowAsync<Exception>().WithMessage("first index cannot be grater than last index");
    }

    [Theory, AutoData]
    public async Task GetSubsequence_WhenMemoryThresholdMetFromFirstIteration_ThrowsException(
        [Range(1, 5)] int firstIndex, [Range(6, 10)] int lastIndex, double memory)
    {
        // arrange
        _mockMemoryChecker.Setup(_ => _.IsThresholdReached(memory)).Returns(true);
        _mockMemoryChecker.Setup(_ => _.GetMemory()).Returns(memory);
        var message = $"We have reached the memory threshold of {memory}";

        // act
        Func<Task> func = async () =>
            await _sut.GetSubsequence(firstIndex, lastIndex, It.IsAny<bool>(), It.IsAny<int>(), memory);

        // assert
        await func.Should().ThrowAsync<Exception>()
            .WithMessage($"No elements were generated{Environment.NewLine}Reason: {message}");
    }

    [Theory, AutoData]
    public async Task GetSubsequence_WhenIsTimeoutFirstIteration_ThrowsException([Range(1, 5)] int firstIndex,
        [Range(6, 10)] int lastIndex, int timeToRun, double memory)
    {
        // arrange
        _mockMemoryChecker.Setup(_ => _.IsThresholdReached(memory)).Returns(false);
        _mockTimeChecker.Setup(_ => _.IsTimeElapsed(timeToRun)).Returns(true);

        // act
        Func<Task> func = async () =>
            await _sut.GetSubsequence(firstIndex, lastIndex, It.IsAny<bool>(), timeToRun, memory);

        // assert
        await func.Should().ThrowAsync<Exception>()
            .WithMessage($"No elements were generated{Environment.NewLine}Reason: Time has elapsed");

        _mockMemoryChecker.Verify(_ => _.IsThresholdReached(memory), Times.Once);
        _mockTimeChecker.Verify(_ => _.IsTimeElapsed(timeToRun), Times.Once);
    }

    [Theory, AutoData]
    public async Task GetSubsequence_WhenSequenceIsBuilt_ReturnsSequenceWithoutMessage([Range(1, 10)] int firstIndex,
        [Range(10, 20)] int lastIndex)
    {
        // arrange
        _mockMemoryChecker.Setup(_ => _.IsThresholdReached(It.IsAny<double>())).Returns(false);
        _mockTimeChecker.Setup(_ => _.IsTimeElapsed(It.IsAny<int>())).Returns(false);

        // act
        var result = await _sut.GetSubsequence(firstIndex, lastIndex, It.IsAny<bool>(), It.IsAny<int>(),
            It.IsAny<double>());

        // assert
        result.Should().NotBeNull();
        result.Sequence.Should().NotBeEmpty().And.HaveCount(lastIndex - firstIndex + 1);
        result.Message.Should().BeNull();

        _mockMemoryChecker.Verify(_ => _.IsThresholdReached(It.IsAny<double>()), Times.Exactly(lastIndex + 1));
        _mockTimeChecker.Verify(_ => _.IsTimeElapsed(It.IsAny<int>()), Times.Exactly(lastIndex + 1));
        _mockCacheManager.Verify(_=>_.Set(It.IsAny<ulong>(), It.IsAny<int>()), Times.AtLeast(2 * firstIndex - 1));
    }
    
    [Theory, AutoData]
    public async Task GetSubsequence_WhenUseCacheIsFalse_CacheManagerContainsIsNotCalled([Range(1, 10)] int firstIndex,
        [Range(10, 20)] int lastIndex)
    {
        // arrange
        const bool useCache = false;
        _mockMemoryChecker.Setup(_ => _.IsThresholdReached(It.IsAny<double>())).Returns(false);
        _mockTimeChecker.Setup(_ => _.IsTimeElapsed(It.IsAny<int>())).Returns(false);

        // act
        var result = await _sut.GetSubsequence(firstIndex, lastIndex, useCache, It.IsAny<int>(),
            It.IsAny<double>());

        // assert
        result.Should().NotBeNull();
        
        _mockCacheManager.Verify(_=>_.Contains(It.IsAny<int>()), Times.Never);
    }
    
    [Theory, AutoData]
    public async Task GetSubsequence_WhenUseCacheIsTrue_CacheManagerIsCalled([Range(1, 10)] int firstIndex,
        [Range(10, 20)] int lastIndex)
    {
        // arrange
        const bool useCache = true;
        _mockMemoryChecker.Setup(_ => _.IsThresholdReached(It.IsAny<double>())).Returns(false);
        _mockTimeChecker.Setup(_ => _.IsTimeElapsed(It.IsAny<int>())).Returns(false);

        // act
        var result = await _sut.GetSubsequence(firstIndex, lastIndex, useCache, It.IsAny<int>(),
            It.IsAny<double>());

        // assert
        result.Should().NotBeNull();
        
        _mockCacheManager.Verify(_=>_.Contains(It.IsAny<int>()), Times.AtLeastOnce);
        _mockCacheManager.Verify(_=>_.Set(It.IsAny<ulong>(), It.IsAny<int>()), Times.AtLeastOnce);
    }
}