using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using JobOfferWebSite_V1._1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace WebApplication2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Jobs> jobs { get; set; }
        public string Governorate { get; set; }
        public string UserType { get; set; }
        //public string PhoneNumber2 { get; internal set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<JobOfferWebSite_V1._1.Models.Categories> Categories { get; set; }

        public System.Data.Entity.DbSet<JobOfferWebSite_V1._1.Models.Jobs> Jobs { get; set; }

        public System.Data.Entity.DbSet<JobOfferWebSite_V1._1.Models.ApplyForJob> ApplyForJobs { get; set; }

        //public System.Data.Entity.DbSet<WebApplication2.Models.EditProfileViewModel> EditProfileViewModels { get; set; }

        //public System.Data.Entity.DbSet<WebApplication2.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}