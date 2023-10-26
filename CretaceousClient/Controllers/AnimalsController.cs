using Microsoft.AspNetCore.Mvc;
using CretaceousClient.Models;
using Newtonsoft.Json;




namespace CretaceousClient.Controllers;

public class AnimalsController : Controller
{
  //   public IActionResult Index()
  //   {
  //     List<Animal> animals = Animal.GetAnimals();
  //     return View(animals);
  //   }
  // In your client-side controller
public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
{
    var apiResponse = await ApiHelper.GetAll(pageIndex, pageSize);
    var paginationData = JsonConvert.DeserializeObject<PaginationModel>(apiResponse);

    var animals = Animal.GetAnimals(pageIndex, pageSize);

    var viewModel = new PaginationModel
    {
        TotalItems = paginationData.TotalItems,
        PageIndex = pageIndex,
        PageSize = pageSize,
        HasPreviousPage = paginationData.HasPreviousPage,
        HasNextPage = paginationData.HasNextPage
    };

    return View(new Tuple<List<Animal>, PaginationModel>(animals, viewModel));
}


  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Animal animal)
  {
    Animal.Post(animal);
    return RedirectToAction("Index");
  }

  public IActionResult Details(int id)
  {
    Animal animal = Animal.GetDetails(id);
    return View(animal);
  }

  public ActionResult Edit(int id)
  {
    Animal animal = Animal.GetDetails(id);
    return View(animal);
  }

  [HttpPost]
  public ActionResult Edit(Animal animal)
  {
    Animal.Put(animal);
    return RedirectToAction("Details", new { id = animal.AnimalId });
  }

  public ActionResult Delete(int id)
  {
    Animal animal = Animal.GetDetails(id);
    return View(animal);
  }

  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Animal.Delete(id);
    return RedirectToAction("Index");
  }
}