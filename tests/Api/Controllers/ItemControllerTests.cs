using System.Linq;
using GilGoblin.Api.Controllers;
using GilGoblin.Database.Pocos;
using GilGoblin.Api.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NUnit.Framework;

namespace GilGoblin.Tests.Api.Controllers;

public class ItemControllerTests
{
    private ItemController _controller;
    private IItemRepository _repo;

    [SetUp]
    public void SetUp()
    {
        _repo = Substitute.For<IItemRepository>();
        _controller = new ItemController(
            _repo,
            NullLoggerFactory.Instance.CreateLogger<ItemController>()
        );
        Assert.That(_controller, Is.Not.Null);
    }

    [Test]
    public void GivenAController_WhenWeReceiveAGetAllRequest_ThenAListOfItemsIsReturned()
    {
        var poco1 = CreatePoco();
        var poco2 = CreatePoco(poco1.GetId() + 100);
        _repo.GetAll().Returns([poco1, poco2]);

        var result = _controller.GetAll().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.Count(i => i.GetId() == poco1.GetId()), Is.EqualTo(1));
            Assert.That(result.Count(i => i.GetId() == poco2.GetId()), Is.EqualTo(1));
        });
    }

    [Test]
    public void GivenAController_WhenWeReceiveAGetRequest_ThenAItemIsReturned()
    {
        var poco1 = CreatePoco();
        _repo.Get(poco1.GetId()).Returns(poco1);

        var result = _controller.Get(poco1.GetId());

        Assert.That(result, Is.EqualTo(poco1));
    }

    [Test]
    public void GivenAController_WhenWeReceiveAGetRequestForANonExistentItem_ThenNullIsReturned()
    {
        var result = _controller.Get(42);

        Assert.That(result, Is.Null);
    }

    private static ItemPoco CreatePoco(int id = 200) =>
        new()
        {
            Id = id,
            CanHq = true,
            IconId = 2332,
            Description = "testDesc",
            Level = 83,
            Name = "testItem",
            StackSize = 1,
            PriceLow = 322,
            PriceMid = 4222
        };
}