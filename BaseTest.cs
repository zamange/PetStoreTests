using RestSharp;
using NUnit.Framework;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace PetstoreTests;


public class Inventory                              
{
    public int Sold { get; set; }
    public int Pending { get; set; }
    public int Available { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Pet
{
    public int Id { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public List<string> PhotoUrls { get; set; } = new List<string>();
    public List<Tag> Tags { get; set; } = new List<Tag>();
    public string Status { get; set; }
}

    public class Order
    {
        public int Id { get; set; }           
        public int PetId { get; set; }        
        public int Quantity { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ShipDate { get; set; } 
        public string Status { get; set; }    
        public bool Complete { get; set; }    
    }

public class BaseTest
{
    protected RestClient client;

    [SetUp]
    public void Initialise()
    {
        client = new RestClient("http://localhost/v2");
    }

    // This method can used to generate random strings based on the prefix provided.
    // For example, var petName = randomName("Cat");
    // You may find it useful in your tests.
    protected static string RandomName(string prefix)
    {
        return prefix + new Random().Next(1, 10000).ToString();
    }

    protected Inventory? GetInventory()                
    {
        var request = new RestRequest("/store/inventory");
        return client.GetAsync<Inventory>((request)).GetAwaiter().GetResult(); 
    }
    
        protected Pet? CreatePet(Pet pet)
        {
            var request = new RestRequest("/pet", Method.Post);
            request.AddJsonBody(pet);  // Add the pet object as JSON to the request body
            var response = client.ExecuteAsync<Pet>(request).GetAwaiter().GetResult();

            return response.StatusCode == HttpStatusCode.OK ? response.Data : null;
        }
    
        protected void RemovePet(int petId)
        {
            var request = new RestRequest($"/pet/{petId}", Method.Delete);
            client.ExecuteAsync(request).GetAwaiter().GetResult();
        }
        protected Pet? GetPet(int petId)
        {
            var request = new RestRequest($"/pet/{petId}", Method.Get);
            var response = client.ExecuteAsync<Pet>(request).GetAwaiter().GetResult();
            return response.StatusCode == HttpStatusCode.OK ? response.Data : null;
        }


        protected Order? OrderPet(Pet pet, int quantity)
        {
            var order = new Order
            {
                PetId = pet.Id,
                Quantity = quantity,
                ShipDate = DateTime.Now,
                Status = "placed",
                Complete = true
            };

            var request = new RestRequest("/store/order", Method.Post);
            request.AddJsonBody(order);

            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();

            Console.WriteLine("Response Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);

            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Order>(response.Content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deserialization failed: " + ex.Message);
                    return null;
                }
            }

            return null;
        }





}