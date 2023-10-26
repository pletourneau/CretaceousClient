using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CretaceousClient.Models
{
  public class Animal
  {
    public int AnimalId { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }

    // We need two very similar "Get" methods because they return two different types of data, one a list, and one a string.
    // public static List<Animal> GetAnimals()
    // {
    //   var apiCallTask = ApiHelper.GetAll(); // uses "GetAll" ApiHelper method
    //   var result = apiCallTask.Result;

    //   JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result); // Results in a JSON array
    //   List<Animal> animalList = JsonConvert.DeserializeObject<List<Animal>>(jsonResponse.ToString());

    //   return animalList;
    // }

    public static List<Animal> GetAnimals(int pageIndex, int pageSize)
    {
      // Modify your API request to include the 'pageIndex' parameter
      var apiCallTask = ApiHelper.GetAll(pageIndex, pageSize);

      var result = apiCallTask.Result;

      // Deserialize the JSON response as a JObject
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);

      if (jsonResponse != null && jsonResponse["data"] != null && jsonResponse["data"].Type == JTokenType.Array)
      {
        JArray animalsArray = jsonResponse["data"].ToObject<JArray>();
        List<Animal> animalList = animalsArray.ToObject < List<Animal>>();

        return animalList;
      }
      else
      {
        return new List<Animal>();
      }
    }




    public static Animal GetDetails(int id) // requires id parameter to get a specific animal
    {
      var apiCallTask = ApiHelper.Get(id); // uses "Get" ApiHelper method
      var result = apiCallTask.Result;

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result); // Results in a JSON object
      Animal animal = JsonConvert.DeserializeObject<Animal>(jsonResponse.ToString());

      return animal;
    }

    public static void Post(Animal animal)
    {
      string jsonAnimal = JsonConvert.SerializeObject(animal);
      ApiHelper.Post(jsonAnimal);
    }

    public static void Put(Animal animal)
    {
      string jsonAnimal = JsonConvert.SerializeObject(animal);
      ApiHelper.Put(animal.AnimalId, jsonAnimal);
    }

    public static void Delete(int id)
    {
      ApiHelper.Delete(id);
    }
    
  }
}

