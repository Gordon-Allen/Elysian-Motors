using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElysianMotors.Models;
using Microsoft.EntityFrameworkCore;

namespace ElysianMotors.Models
{
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
        [Display(Name = "Vehicle-Type")]
        public string VehicleType { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Your Vehicle's 'Engine-Type' must be at least (2) characters long")]
        [Display(Name = "Engine-Type")]

        public string EngineType { get; set; }

        [Required]
        [Display(Name = "# of Seats")]
        public int NumberOfSeats { get; set; }

        [Required]
        [Display(Name = "URL Image of Your Vehicle")]

        public string ImageUrl { get; set; }

        [Required]
        public string Price { get; set; }
    }
}