using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace JobOfferWebSite_V1._1.Models
{
    public class Jobs
    {
        public int Id { get; set; }

        [Display(Name = "اسم الوظيفة")]
        public string JobTitle { get; set; }
        
        [Display(Name = "وصف الوظيفة")]
        [AllowHtml]
        public string JobContent { get; set; }

        [Display(Name = "صورة الوظيفة")]
        public string JobImage { get; set; }

        public string UserId { get; set; }

        //Connect 2 table with others
        [DisplayName("نوع الوظيفة")]
        public int CategoryId { get; set; }
        // 1:M ==> 1:Category ==> M:Jobs 
        
        public virtual Categories Categories { get; set; }
        
        public virtual ApplicationUser User { get; set; }
    }
}