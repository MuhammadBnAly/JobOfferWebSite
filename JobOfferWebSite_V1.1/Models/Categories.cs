using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOfferWebSite_V1._1.Models
{
    public class Categories
    {
        public int ID { get; set; }

        [Required]
        [Display(Name ="مجال الوظيفة")]
        public string Category { get; set; }

        [Required]
        [Display(Name ="تفاصيل أخرى")]
        public string Description { get; set; }

        //Connection 2 tables
        public virtual ICollection<Jobs> jobs { get; set; }
    }
}