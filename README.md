# Petstore API Tests

This project contains API tests for the Petstore application using RestSharp, NUnit, and other .NET testing tools. The tests cover operations like getting inventory, creating, ordering, retrieving, and deleting pets.

## Prerequisites

Before running the tests, ensure you have the following installed:

- .NET 6.0 or later
- NUnit Test Adapter
- RestSharp
- Newtonsoft.Json
- A running Petstore API server (e.g., `http://localhost/v2`)

## Project Setup

1. Clone the repository or download the source files.
2. Open the project in Visual Studio or your preferred C# IDE.
3. Restore the NuGet packages:

dotnet restore

arduino
Copy code

## Running Tests

You can run the tests using NUnit or via your IDE's test runner.

To run tests from the command line:
dotnet test



## Test Cases

### 1. Inventory Tests

#### `GetInventoryShouldBeOk`
Verifies that the `/store/inventory` endpoint returns a successful response (HTTP 200).

```csharp
  [Test]
  public void GetInventoryShouldBeOk()
  {
      var request = new RestRequest("/store/inventory");
      var result = client.GetAsync(request).GetAwaiter().GetResult();
      Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
      Assert.That(result.Content, Is.Not.Null);
  }


#### 'GetInventoryShouldReturnInventory'
Checks that the inventory returned contains expected values.


2. Pet Tests
GetPetThatExists
Verifies that an existing pet can be retrieved using its ID.


GetPetThatDoesNotExist
Verifies that a non-existing pet returns null.


AddPetTest
Adds a new pet and verifies that it is created correctly.


AddRemovePetTest
Adds a pet and then removes it, ensuring that the pet no longer exists.

3. Order Tests
OrderPetTest
Creates a new pet and orders it, then verifies the order status and pet ID.





### Code Structure
BaseTest
The base test class provides common functionality for interacting with the Petstore API, including methods for creating pets, retrieving pets, placing orders, and more.

InventoryTests, PetTests, OrderTests
These are the test classes containing individual test cases for inventory, pets, and orders.

*** Dependencies ***
The project uses the following NuGet packages:

    - RestSharp for making HTTP requests to the Petstore API.
    - NUnit for running unit tests.
    - Newtonsoft.Json for JSON serialization and deserialization.
    - QaVerification for grading tests.

### License
This project is licensed under the MIT License - see the LICENSE file for details.


