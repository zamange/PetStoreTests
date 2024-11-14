// PetTests.cs
using QaVerification;

namespace PetstoreTests
{
    [TestFixture]
    public class PetTests : BaseTest
    {
        [Grading]
        [Test]
        public void GetPetThatExists()
        {
            // Arrange
            int petId = 1; // Assuming pet with ID 1 exists
            
            // Act
            var pet = GetPet(petId);
            
            // Assert
            Assert.That(pet, Is.Not.Null);
            Assert.That(pet!.Id, Is.EqualTo(petId));
            Assert.That(pet.Name, Is.Not.Null);
            Assert.That(pet.Category, Is.Not.Null);
            Assert.That(pet.PhotoUrls, Is.Not.Empty);
            Assert.That(pet.Tags, Is.Not.Empty);
        }

        [Grading]
        [Test]
        public void GetPetThatDoesNotExist()
        {
            // Arrange
            int petId = 99; // Assuming pet with ID 99 does not exist
            
            // Act
            var pet = GetPet(petId);
            
            // Assert
            Assert.That(pet, Is.Null); // Expecting null since the pet does not exist
        }

        [Grading]
        [Test]
        public void AddPetTest()
        {
            // Arrange
            var newPet = new Pet
            {
                Id = 10, // or leave blank if auto-generated
                Name = "Fluffy",
                Category = new Category { Id = 1, Name = "Dogs" },
                PhotoUrls = new List<string> { "http://example.com/photo.jpg" },
                Tags = new List<Tag> { new Tag { Id = 1, Name = "cute" } }
            };

            // Act
            var addedPet = CreatePet(newPet);

            // Assert
            Assert.That(addedPet, Is.Not.Null);
            Assert.That(addedPet!.Name, Is.EqualTo("Fluffy"));
            Assert.That(addedPet.Category?.Name, Is.EqualTo("Dogs"));
        }

        [Grading]
        [Test]
        public void AddRemovePetTest()
        {
            // Arrange: Add a pet
            var newPet = new Pet
            {
                Name = "Fido",
                Category = new Category { Id = 1, Name = "Dogs" },
                PhotoUrls = new List<string> { "http://example.com/photo.jpg" },
                Tags = new List<Tag> { new Tag { Id = 1, Name = "active" } }
            };
            
            var addedPet = CreatePet(newPet);
            Assert.That(addedPet, Is.Not.Null);

            // Act: Remove the pet
            RemovePet(addedPet.Id);

            // Assert: Verify the pet no longer exists
            var deletedPet = GetPet(addedPet.Id);
            Assert.That(deletedPet, Is.Null); // Pet should no longer exist
        }
    }
}
