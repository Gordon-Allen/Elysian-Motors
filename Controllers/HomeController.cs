using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElysianMotors.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ElysianMotors.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("About")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("AddVehicle")]
        public IActionResult AddVehicle()
        {
            return View();
        }

        [HttpGet("SellerList")]
        public IActionResult SellerList()
        {
            List<Vehicle> allVehicles = dbContext.Vehicles
            .OrderBy(v => v.Year)
            .ToList();
            ViewBag.AllVehicles = allVehicles;
            return View();

        }

        [Route("btAddVehicle")]
        [HttpPost]
        public IActionResult btAddVehicle(Vehicle newVehicle)
        {
            Console.WriteLine(newVehicle.Year);
            Console.WriteLine(newVehicle.Make);
            Console.WriteLine(newVehicle.Model);
            Console.WriteLine(newVehicle.Color);
            Console.WriteLine(newVehicle.Mileage);
            Console.WriteLine(newVehicle.VehicleType);
            Console.WriteLine(newVehicle.EngineType);
            Console.WriteLine(newVehicle.NumberOfSeats);
            Console.WriteLine(newVehicle.Price);

            if (ModelState.IsValid){
                dbContext.Add(newVehicle);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("<--------- Error Adding Vehicle to DB -----------> ");
                return View("AddVehicle");
            }        
        }

        [Route("detail/{id}")]
        [HttpGet]
        public IActionResult VehicleDetail(int id)
        {
            Vehicle v = dbContext.Vehicles
            .FirstOrDefault(pro => pro.VehicleID == id);

            ViewBag.VehicleDetail = v;
            return View("VehicleDetail");
        }
    }
}
