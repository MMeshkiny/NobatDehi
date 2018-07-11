using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NobatDehi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.RemoveClaim(userIdentity.FindFirst(ClaimTypes.Name));
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, FirstName + " " + LastName));

            return userIdentity;
        }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public ICollection<CancelTime> CancelTimes { get; internal set; }
        //public ICollection<Doctor> Doctors { get; internal set; }
        public ICollection<Factor> Factors { get; internal set; }
        public ICollection<VisitTime> VisitTimes { get; internal set; }
        public virtual ICollection<DoctorMedicalCenter> DoctorMedicalCenters { get; set; }
        public virtual ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }
    }

    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<NobatDehi.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<NobatDehi.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}