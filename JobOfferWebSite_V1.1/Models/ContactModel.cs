using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOfferWebSite_V1._1.Models
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }
        
        [Required]
        public string Message { get; set; }
        
        [Required]
        public string Country { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }

    }
}