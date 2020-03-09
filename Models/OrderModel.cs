using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElysianMotors.Models;
using Microsoft.EntityFrameworkCore;

namespace ElysianMotors.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int VehicleID { get; set; }
        public string VehicleName {get; set;}
        public string PurchasePrice {get; set;}

        [Required]
        [MinLength(2, ErrorMessage = "Your 'First-Name' has to be at least (2) characters")]
        [Display(Name = "First Name")]
        public string CustomerFirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Your 'Last-Name' has to be at least (2) characters")]
        [Display(Name = "Last Name")]
        public string CustomerLastName { get; set; }

        [Required(ErrorMessage = "Please enter a valid Email-Address")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

    }
}