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
      _db.Add(cuisine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet("/Cuisines/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      return View(thisCuisine);
    }

    [HttpPost("/Cuisines/{id}/delete/confirm")]
    public ActionResult Confirm(int id)
    {
      Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      List<Restaurant> thisRestaurants = _db.Restaurants.ToList();
      foreach (Restaurant delRest in thisRestaurants)
      {
        if (delRest.CuisineId == id)
        {
          _db.Restaurants.Remove(delRest);
        }
      }
      _db.Cuisines.Remove(thisCuisine);
      _db.SaveChanges();
      return Redirect("/Cuisines/");
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