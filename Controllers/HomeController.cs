using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElysianMotors.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace ElysianMotors.Controllers
{
    public class HomeController : Controller
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "Elysian Motors";
        static readonly string sheet = "EM_Orders";
        static readonly string SpreadsheetId = "18BCAtEdGgRsiMoHtOjBZsfr3tNFiKUG2TI97KSH0Eyw";
        static SheetsService service;

        static void Init(){

        GoogleCredential credential;
        //Reading Credentials File...
        using (var stream = new FileStream("app_client_secret.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
        }

        // Creating Google Sheets API service...
        service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });
        }

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

        [Route("detail/{id}")]
        [HttpGet]
        public IActionResult VehicleDetail(int id)
        {
            Vehicle v = dbContext.Vehicles
            .FirstOrDefault(pro => pro.VehicleID == id);

            TempData["pVehicleID"] = id;
            ViewBag.VehicleDetail = v;
            return View("VehicleDetail");
        }

        [Route("detail/purchase/{id}")]
        [HttpGet]
        public IActionResult PurchaseVehicle(int id)
        {
            Vehicle purchaseV = dbContext.Vehicles
            .FirstOrDefault(pro => pro.VehicleID == id);
            ViewBag.VehicleDetail = purchaseV;
            return View("PurchaseVehicle");
        }

        [Route("btCreateOrder")]
        [HttpPost]
        public IActionResult btCreateOrder(int? id, Order newOrder)
        {
            Vehicle confirmpurchaseV = dbContext.Vehicles
            .FirstOrDefault(pro => pro.VehicleID == id);
            newOrder.VehicleID = confirmpurchaseV.VehicleID;
            newOrder.VehicleName = confirmpurchaseV.Year + " " + confirmpurchaseV.Make + " " + confirmpurchaseV.Model;
            newOrder.PurchasePrice = confirmpurchaseV.Price;

            Console.WriteLine(newOrder.OrderId);
            Console.WriteLine(newOrder.VehicleID);
            Console.WriteLine(newOrder.VehicleName);
            Console.WriteLine(newOrder.CustomerFirstName);
            Console.WriteLine(newOrder.CustomerLastName);
            Console.WriteLine(newOrder.CustomerEmail);
            Console.WriteLine(newOrder.PurchasePrice);
            Console.WriteLine(newOrder.OrderDate);

            if (ModelState.IsValid){
                dbContext.Add(newOrder);
                Vehicle removePurchaseV = dbContext.Vehicles.FirstOrDefault(web => web.VehicleID == newOrder.VehicleID);
                dbContext.Remove(removePurchaseV);
                dbContext.SaveChanges();

                Init();
                var range = $"{sheet}!A:H";
                var valueRange = new ValueRange();

                // Data for Order...
                var oblist = new List<object>() { newOrder.OrderId, newOrder.VehicleID, newOrder.VehicleName, newOrder.CustomerFirstName, newOrder.CustomerLastName, newOrder.CustomerEmail, newOrder.PurchasePrice, newOrder.OrderDate};
                valueRange.Values = new List<IList<object>> { oblist };

                // Append the above record To 'Elysian Motors' G-Sheet..
                var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
                appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                var appendReponse = appendRequest.Execute();
                return RedirectToAction("PurchaseConfirmation", new {id = newOrder.OrderId});
            }
            else
            {
                Console.WriteLine("<--------- Error Adding ORDER to DB -----------> ");
                return View("PurchaseVehicle");
            }        
        }

        [Route("purchaseconfirmation/{id}")]
        [HttpGet]
        public IActionResult PurchaseConfirmation(int id)
        {
            Order confirmOrder = dbContext.Orders
            .FirstOrDefault(pro => pro.OrderId == id);
            ViewBag.OrderDetail = confirmOrder;
            return View("PurchaseConfirmation");
        }

    }
}
