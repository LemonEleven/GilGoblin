using System.Linq;
using System.Threading.Tasks;
using GilGoblin.Api.Cache;
using GilGoblin.Database.Pocos;
using GilGoblin.Api.Repository;
using NSubstitute;
using NUnit.Framework;

namespace GilGoblin.Tests.Api.Repository;

public class ItemRepositoryTests : InMemoryTestDb
{
    private IItemCache _cache;

    [Test]
    public void GivenAGetAll_ThenTheRepositoryReturnsAllEntries()
    {
        using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);

        var result = itemRepo.GetAll();

        var allItems = context.Item.ToList();
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(allItems.Count));
            allItems.ForEach(item => Assert.That(result.Any(p => p.Name == item.Name)));
            allItems.ForEach(item => Assert.That(result.Any(p => p.Id == item.Id)));
        });
    }

    [TestCase(1)]
    [TestCase(2)]
    public void GivenAGet_WhenTheIdIsValid_ThenTheRepositoryReturnsTheCorrectEntry(int id)
    {
        using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);

        var result = itemRepo.Get(id);

        Assert.Multiple(() =>
        {
            Assert.That(result?.Name == $"Item {id}");
            Assert.That(result.Id == id);
        });
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(100)]
    public void GivenAGet_WhenIdIsInvalid_ThenTheRepositoryReturnsNull(int id)
    {
        using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);

        var result = itemRepo.Get(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GivenAGetMultiple_WhenIdsAreValid_ThenTheCorrectEntriesAreReturned()
    {
        using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);

        var result = itemRepo.GetMultiple(new[] { 1, 2 });

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(p => p.Id == 1));
            Assert.That(result.Any(p => p.Id == 2));
        });
    }

    [Test]
    public void GivenAGetMultiple_WhenSomeIdsAreValid_ThenTheValidEntriesAreReturned()
    {
        using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);

        var result = itemRepo.GetMultiple(new[] { 1, 99 });

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.Any(p => p.Id == 1));
        });
    }

    [Test]
    public void GivenAGetMultiple_WhenIdsAreInvalid_ThenNoEntriesAreReturned()
    {
        using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);

        var result = itemRepo.GetMultiple(new[] { 33, 99 });

        Assert.That(!result.Any());
    }

    [Test]
    public void GivenAGetMultiple_WhenIdsEmpty_ThenNoEntriesAreReturned()
    {
        using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);

        var result = itemRepo.GetMultiple(new int[] { });

        Assert.That(!result.Any());
    }

    [Test]
    public void GivenAGet_WhenTheIdIsValidAndUncached_ThenWeCacheTheEntry()
    {
        using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);

        _ = itemRepo.Get(2);

        _cache.Received(1).Get(2);
        _cache.Received(1).Add(2, Arg.Is<ItemPoco>(item => item.Id == 2));
    }

    [Test]
    public void GivenAGet_WhenTheIdIsValidAndCached_ThenWeReturnTheCachedEntry()
    {
        using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);
        _cache.Get(2).Returns(null, new ItemPoco() { Id = 2 });
        _ = itemRepo.Get(2);

        itemRepo.Get(2);

        _cache.Received(2).Get(2);
        _cache.Received(1).Add(2, Arg.Is<ItemPoco>(item => item.Id == 2));
    }

    [Test]
    public async Task GivenAFillCache_WhenEntriesExist_ThenWeFillTheCache()
    {
        await using var context = new TestGilGoblinDbContext(_options, _configuration);
        var itemRepo = new ItemRepository(context, _cache);
        var allItems = context.Item.ToList();

        await itemRepo.FillCache();

        allItems.ForEach(item => _cache.Received(1).Add(item.Id, item));
    }

    [Test]
    public async Task GivenAFillCache_WhenEntriesDoNotExist_ThenWeDoNothing()
    {
        await using var context = new TestGilGoblinDbContext(_options, _configuration);
        context.Item.RemoveRange(context.Item);
        await context.SaveChangesAsync();
        var itemRepo = new ItemRepository(context, _cache);

        await itemRepo.FillCache();

        _cache.DidNotReceive().Add(Arg.Any<int>(), Arg.Any<ItemPoco>());
    }

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _cache = Substitute.For<IItemCache>();
    }
}