using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BestRestaurantApp.Models;

namespace BestRestaurantApp.Controllers
{
  public class CuisinesController : Controller
  {
    private readonly BestRestaurantContext _db;

    public CuisinesController(BestRestaurantContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Cuisine> model = _db.Cuisines.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Cuisine cuisine)
    {
      // Cuisine newC = new Cuisine();
      // newC.Type = type;
      // newC.setId();
      _db.Add(cuisine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Cuisine thisCuisine = _db.Cuisines
        .Include(cuisine => cuisine.Restaurants) 
        .FirstOrDefault(cuisine => cuisine.CuisineId == id); 
      List<Restaurant> dbRests = _db.Restaurants.ToList();
      thisCuisine.Restaurants = new List<Restaurant>{};
      foreach (Restaurant rest in dbRests)
      {
        if (rest.CuisineId == thisCuisine.CuisineId) 
        {
          thisCuisine.Restaurants.Add(rest);
        }
      }
        return View(thisCuisine);
    }
  }
}