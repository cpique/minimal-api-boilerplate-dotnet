using AutoFixture;
using MinimalApiBoilerplate.Application.Requests;
using MinimalApiBoilerplate.Application.Services;
using MinimalApiBoilerplate.Domain;
using MinimalApiBoilerplate.Infrastructure;
using Moq;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace MinimalApiBoilerplate.UnitTests;

public class ItemServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IItemRepository> _repoMock;
    private readonly ItemService _service;

    public ItemServiceTests()
    {
        _fixture = new Fixture();
        _repoMock = new Mock<IItemRepository>();
        _service = new ItemService(_repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_UserNull_ReturnsAllMappedResponses()
    {
        // Arrange
        var count = 3;
        var items = _fixture
            .Build<Item>()
            .CreateMany(count)
            .ToList();

        _repoMock.Setup(r => r.GetAllAsync(null)).ReturnsAsync(items);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(count, result.Count);
    }

    [Fact]
    public async Task GetAllAsync_WithUserId_ReturnsUserIdMappedResponses()
    {
        // Arrange
        var count = 5;
        var userId = Guid.NewGuid();
        var itemsWithUser = _fixture
            .Build<Item>()
            .With(x => x.UserId, userId)
            .CreateMany(count)
            .ToList();

        var itemsWithNoUser = _fixture
            .Build<Item>()
            .With(x => x.UserId, userId)
            .CreateMany(count)
            .ToList();

        var items = itemsWithUser.Concat(itemsWithNoUser).ToList();

        _repoMock.Setup(r => r.GetAllAsync(userId.ToString())).ReturnsAsync(itemsWithUser);

        // Act
        var result = await _service.GetAllAsync(userId.ToString());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(itemsWithUser.Count, result.Count);
        Assert.All(result, x => Assert.Equal(userId, x.UserId));
    }

    [Fact]
    public async Task GetAllAsync_WithNoItems_ReturnsEmptyList()
    {
        // Arrange
        _repoMock.Setup(r => r.GetAllAsync(null)).ReturnsAsync(new List<Item>());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenItemExists_ReturnsResponse()
    {
        var id = Guid.NewGuid();
        var item = _fixture
            .Build<Item>()
            .With(x => x.Id, id)
            .Create();

        _repoMock.Setup(r => r.GetByIdAsync(item.Id)).ReturnsAsync(item);

        var result = await _service.GetByIdAsync(item.Id);

        Assert.NotNull(result);
        Assert.Equal(item.Id, result!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_WhenItemNotFound_ReturnsNull()
    {
        var item = _fixture
            .Build<Item>()
            .Create();

        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Item?)null);

        var result = await _service.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_CallsRepositoryWithMappedEntity()
    {
        var request = new CreateItemRequest { Name = "NewItem" };

        await _service.AddAsync(request);

        _repoMock.Verify(r => r.AddAsync(It.Is<Item>(i => i.Name == "NewItem")), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsRepositoryResult()
    {
        var request = new UpdateItemRequest { Id = Guid.NewGuid(), Name = "Updated" };
        _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Item>())).ReturnsAsync(true);

        var result = await _service.UpdateAsync(request);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_CallsRepositoryDelete()
    {
        var id = Guid.NewGuid();
        _repoMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

        var result = await _service.DeleteAsync(id);

        Assert.True(result);
    }
}