using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace JobOfferWebSite_V1._1.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }

        //M:M (Job, user)
        public virtual Jobs jobs { get; set; }
        public virtual ApplicationUser user { get; set; }
    }

}