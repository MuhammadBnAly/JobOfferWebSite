using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOfferWebSite_V1._1.Models
{
    public class JobsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> items { get; set; }

    }
}