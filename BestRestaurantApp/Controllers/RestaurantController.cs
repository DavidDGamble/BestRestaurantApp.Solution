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
        
    }
}