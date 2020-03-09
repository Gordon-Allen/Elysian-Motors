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
    public class OrderController : Controller
    {
        // static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        // static readonly string ApplicationName = "<<ENTER NAME OF PROJECT YOU SPECIFIED IN GOOGLE DEVELOPER CONSOLE>>";
        // static readonly string sheet = "<<ENTER NAME OF SPECFIC SHEET WITHIN YOUR GOOGLE SHEETS SPREADSHEET YOU WISH TO SEND INFO TO (DEFAULT IS 'Sheet 1')>>";
        // static readonly string SpreadsheetId = "<<ENTER GOOGLE SHEETS SPREADSHEET ID, FROUND IN GOOGLE SHEETS URL>>";
        // static SheetsService service;

        // static void GoogleSheets_Init(){

        // GoogleCredential credential;

        // //Reading Credentials File...
        // using (var stream = new FileStream("<<ENTER NAME OF GOOGLE SHEETS CREDENTIALS JSON FILE>>", FileMode.Open, FileAccess.Read))
        // {
        //     credential = GoogleCredential.FromStream(stream)
        //         .CreateScoped(Scopes);
        // }

        // // Creating Google Sheets API service...
        // service = new SheetsService(new BaseClientService.Initializer()
        // {
        //     HttpClientInitializer = credential,
        //     ApplicationName = ApplicationName,
        // });
        // }

        private MyContext dbContext;

        public OrderController(MyContext context)
        {
            dbContext = context;
        }

        [Route("Order/purchase/{id}")]
        [HttpGet]
        public IActionResult PurchaseVehicle(int id)
        {
            Vehicle purchaseV = dbContext.Vehicles
            .FirstOrDefault(pro => pro.VehicleID == id);
            ViewBag.VehicleDetail = purchaseV;
            return View("PurchaseVehicle");
        }

        [Route("Order/btCreateOrder")]
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

                // GoogleSheets_Init();
                // var range = $"{sheet}!A:H";
                // var valueRange = new ValueRange();

                // // Data for Order...
                // var oblist = new List<object>() { newOrder.OrderId, newOrder.VehicleID, newOrder.VehicleName, newOrder.CustomerFirstName, newOrder.CustomerLastName, newOrder.CustomerEmail, newOrder.PurchasePrice, newOrder.OrderDate};
                // valueRange.Values = new List<IList<object>> { oblist };

                // // Append the above record To 'Elysian Motors' G-Sheet..
                // var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
                // appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                // var appendReponse = appendRequest.Execute();
                
                return RedirectToAction("PurchaseConfirmation", new {id = newOrder.OrderId});
            }
            else
            {
                Console.WriteLine("<--------- Error Adding ORDER to DB -----------> ");
                return View("PurchaseVehicle");
            }        
        }

        [Route("Order/purchaseconfirmation/{id}")]
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