using System.Threading.Tasks;
using RestSharp;

namespace CretaceousClient.Models
{
  public class ApiHelper
  {
    // public static async Task<string> GetAll()
    // {
    //   RestClient client = new RestClient("http://localhost:5000/");
    //   RestRequest request = new RestRequest($"api/animals", Method.Get);
    //   RestResponse response = await client.GetAsync(request);
    //   return response.Content;
    // }
    public static async Task<string> GetAll(int pageIndex, int pageSize)
    {
      RestClient client = new RestClient("http://localhost:5000/");
      RestRequest request = new RestRequest($"api/animals?pageIndex={pageIndex}&pageSize={pageSize}", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async Task<string> Get(int id)
    {
      RestClient client = new RestClient("http://localhost:5000/");
      RestRequest request = new RestRequest($"api/animals/{id}", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async Task Post(string newAnimal)
    {
      RestClient client = new RestClient("http://localhost:5000/");
      RestRequest request = new RestRequest($"api/animals", Method.Post);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newAnimal);
      await client.PostAsync(request);
    }

    public static async Task Put(int id, string animalToEdit) // Changed "string newAnimal" to more appropriate name.
    {
      RestClient client = new RestClient("http://localhost:5000/");
      RestRequest request = new RestRequest($"api/animals/{id}", Method.Put);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(animalToEdit);
      await client.PutAsync(request);
    }

    public static async Task Delete(int id)
    {
      RestClient client = new RestClient("http://localhost:5000/");
      RestRequest request = new RestRequest($"api/animals/{id}", Method.Delete);
      request.AddHeader("Content-Type", "application/json");
      await client.DeleteAsync(request);
    }
  }
}