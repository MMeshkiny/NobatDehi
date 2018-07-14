namespace NobatDehi.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<NobatDehi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NobatDehi.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Roles.AddOrUpdate(x => x.Id, new IdentityRole[]
                {
                    new IdentityRole(){Id="1",Name="Admin"},
                    new IdentityRole(){Id="2",Name="Doctor"},
                    new IdentityRole(){Id="3",Name="User"},
                    new IdentityRole(){Id="4",Name="ASistant"},
                });

            context.MedicalCenterTypes.AddOrUpdate(x => x.Title, new Models.MedicalCenterType[]
                {
                    new Models.MedicalCenterType(){Title="مطب"},
                    new Models.MedicalCenterType(){Title="درمانگاه"},
                    new Models.MedicalCenterType(){Title="بیمارستان"},
                    new Models.MedicalCenterType(){Title="بیمارستان تخصصی"},
                    new Models.MedicalCenterType(){Title="بیمارستان فوق تخصصی"},
                });

            context.Specialties.AddOrUpdate(x => x.Title, new Models.Specialty[]
                {
                    new Models.Specialty(){Title="قلب و عروق"},
                    new Models.Specialty(){Title="مغز و اعصاب"},
                    new Models.Specialty(){Title="کلیه و مجاری ادراری"},
                    new Models.Specialty(){Title="گوش و حلق و بینی"},
                    new Models.Specialty(){Title="غدد"},
                    new Models.Specialty(){Title="اطفال"},
                    new Models.Specialty(){Title="عمومی"},
                    new Models.Specialty(){Title="داخلی"},
                });
        }
    }
}
