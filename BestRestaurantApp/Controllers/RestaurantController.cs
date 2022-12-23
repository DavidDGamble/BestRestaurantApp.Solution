using Microsoft.AspNetCore.Mvc;
using BestRestaurantApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace BestRestaurantApp.Controllers
{
  public class RestaurantController : Controller
  {
    private readonly BestRestaurantContext _db;

    public RestaurantController(BestRestaurantContext db)
    {
      _db = db;
    }

    [HttpGet("/restaurants/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }

    [HttpGet("/restaurants/details")]
    public ActionResult Details(string search)
    {
      List<Restaurant> thisRestaurant = _db.Restaurants
        .Where(restaurant => restaurant.Name.Contains(search))
        .ToList();
      List<Cuisine> cuisines = _db.Cuisines.ToList();
      ViewBag.Cuisines = cuisines;

      return View(thisRestaurant);
    }

    [HttpPost("/restaurants/{id}/delete/confirm")]
    public ActionResult Confirm(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      _db.Restaurants.Remove(thisRestaurant);
      _db.SaveChanges();
      return Redirect($"/Cuisines/Details/{thisRestaurant.CuisineId}");
    }

    [HttpPost("/restaurants/{id}/create")]
    public ActionResult Create(int id, Restaurant restaurant)
    {
      restaurant.CuisineId = id;
      _db.Restaurants.Add(restaurant);
      _db.SaveChanges();
      return Redirect($"/Cuisines/Details/{id}");
    }
  }
}