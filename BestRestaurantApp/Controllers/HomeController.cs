using Microsoft.AspNetCore.Mvc;
using BestRestaurantApp.Models;

namespace BestRestaurantApp.Controllers
{
  public class HomeController : Controller
  {
    [Route("/")]
    public ActionResult Index() {
      return View();
    }
  }
}