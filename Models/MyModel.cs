using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElysianMotors.Models;
using Microsoft.EntityFrameworkCore;

namespace ElysianMotors.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Customer's First Name has to be at least (2) characters")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Customer's Last Name has to be at least (2) characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a vaid email address or enter an additional email")]
        [EmailAddress]
        public string Email { get; set; }

        public List<Order> CustomerToVehicle { get; set; }

    }
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }
        [Required]
        public int Year { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Your Vehicle's 'Make' must be at least (3) characters long")]
        public string Make { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Your Vehicle's 'Model' must be at least (3) characters long")]
        public string Model { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Your Vehicle's 'Color' must be at least (3) characters long")]
        public string Color { get; set; }

        [Required]
        public string Mileage { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Please select your Vehicles 'Type' from the drop-down list")]
        public string VehicleType { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Your Vehicle's 'Engine-Type' must be at least (2) characters long")]
        public string EngineType { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Price { get; set; }
        public List<Order> VehicleToCustomer { get; set; }
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int CustomerId {get; set;}
        public int VehicleID { get; set; }
        public string VehicleName {get; set;}
        public double Price {get; set;}
        public Customer PurchaseCustomer { get; set; }
        public Vehicle SoldVehicle {get; set;}
    }
}