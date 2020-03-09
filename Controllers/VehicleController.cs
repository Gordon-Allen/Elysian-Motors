using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElysianMotors.Models;
using Microsoft.EntityFrameworkCore;

namespace ElysianMotors.Controllers
{
    public class VehicleController : Controller
    {
        private MyContext dbContext;

        public VehicleController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("Vehicle/AddVehicle")]
        public IActionResult AddVehicle()
        {
            return View();
        }

        [HttpGet("Vehicle/SellerList")]
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
            Console.WriteLine(newVehicle.VehicleID);
            Console.WriteLine(newVehicle.Year);
            Console.WriteLine(newVehicle.Make);
            Console.WriteLine(newVehicle.Model);
            Console.WriteLine(newVehicle.Color);
            Console.WriteLine(newVehicle.Mileage);
            Console.WriteLine(newVehicle.VehicleType);
            Console.WriteLine(newVehicle.EngineType);
            Console.WriteLine(newVehicle.NumberOfSeats);
            Console.WriteLine(newVehicle.ImageUrl);
            Console.WriteLine(newVehicle.Price);

            if (ModelState.IsValid){
                dbContext.Add(newVehicle);
                dbContext.SaveChanges();
                return RedirectToAction("SellerList");
            }
            else
            {
                Console.WriteLine("<--------- Error Adding VEHICLE to DB -----------> ");
                return View("AddVehicle");
            }        
        }

        [Route("Vehicle/detail/{id}")]
        [HttpGet]
        public IActionResult VehicleDetail(int id)
        {
            Vehicle v = dbContext.Vehicles
            .FirstOrDefault(pro => pro.VehicleID == id);

            TempData["pVehicleID"] = id;
            ViewBag.VehicleDetail = v;
            return View("VehicleDetail");
        }
    }
}