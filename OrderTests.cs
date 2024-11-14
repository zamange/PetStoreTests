using QaVerification;
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace PetstoreTests
{
    [TestFixture]
    public class OrderTests : BaseTest
    {
[Test]
    public void OrderPetTest()
    {
        // Arrange: Create a new pet
        var newPet = new Pet
        {
            Name = RandomName("Dog"),
            Category = new Category { Id = 1, Name = "Dog" },
            Status = "available",
            Tags = new List<Tag> { new Tag { Id = 1, Name = "friendly" } }
        };

        var createdPet = CreatePet(newPet);
        Assert.That(createdPet, Is.Not.Null, "Failed to create pet");

        // Act: Create an order for the newly created pet
        var order = OrderPet(createdPet, 1);

        // Assert: Check that the order is not null
        Assert.That(order, Is.Not.Null, "Order data is null");

        // Assert: Verify that the order status is "placed" and pet ID matches
        Assert.That(order.Status, Is.EqualTo("placed"));
        Assert.That(order.PetId, Is.EqualTo(createdPet.Id));
    }

}}
