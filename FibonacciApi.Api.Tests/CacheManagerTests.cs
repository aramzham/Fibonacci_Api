namespace FibonacciApi.Api.Tests;

public class CacheManagerTests
{
    private readonly Mock<IConfiguration> _mockConfiguration = new();
    private readonly Fixture _fixture = new();
    private readonly CacheManager _sut;

    public CacheManagerTests()
    {
        _sut = new CacheManager(_mockConfiguration.Object);
    }

    [Fact]
    public void CacheManager_WhenCreated_HasTwoRecords()
    {
        // arrange

        // act

        // assert
        _sut.Contains(0).Should().BeTrue();
        _sut.Contains(1).Should().BeTrue();

        _sut.Get(0).Should().Be(0);
        _sut.Get(1).Should().Be(1);
    }

    [Theory, AutoData]
    public void Contains_WhenValueExists_ReturnsTrue(int index, ulong value)
    {
        // arrange

        // act
        _sut.Set(value, index);

        // assert
        _sut.Contains(index).Should().BeTrue();
    }

    [Fact]
    public void Contains_WhenValueDoesNotExist_ReturnsFalse()
    {
        // arrange
        var index = _fixture.Create<int>() + 2;

        // act

        // assert
        _sut.Contains(index).Should().BeFalse();
    }

    [Theory, AutoData]
    public void Get_WhenIndexExists_ReturnsValue(int index, ulong value)
    {
        // arrange

        // act
        _sut.Set(value, index);

        // assert
        _sut.Get(index).Should().Be(value);
    }

    [Fact]
    public void Get_WhenIndexDoesNotExist_ThrowsException()
    {
        // arrange
        var index = _fixture.Create<int>() + 2;

        // act
        var get = () => _sut.Get(index);

        // assert
        get.Should().Throw<Exception>();
    }

    [Theory, AutoData]
    public void Set_WhenValueSet_ShouldExistInCache(int index, ulong value)
    {
        // arrange

        // act
        _sut.Set(value, index);

        // assert
        _sut.Contains(index).Should().BeTrue();
        _sut.Get(index).Should().Be(value);
    }

    [Theory, AutoData]
    public async Task CacheManager_WhenInvalidationTimerExpires_CacheIsClearedToInitialValues(int index, ulong value)
    {
        // arrange
        const int defaultTimeInMs = 1 * 60 * 1000;

        // act
        _sut.Set(value, index);
        await Task.Delay(defaultTimeInMs + 100);

        // assert
        _sut.Get(0).Should().Be(0);
        _sut.Get(1).Should().Be(1);
        _sut.Contains(index).Should().BeFalse();
    }
}