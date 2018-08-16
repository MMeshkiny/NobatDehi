namespace NobatDehi.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Your context has been configured to use a 'MyModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'NobatDehi.Models.MyModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MyModel' 
        // connection string in the application configuration file.
        //public MyModel()
        //    : base("name=DefaultConnection")
        //{
        //}

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        //public virtual DbSet<ApplicationUser> AspNetUsers { get; set; }
        public virtual DbSet<CancelTime> CancelTimes { get; set; }
        //public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorMedicalCenter> DoctorMedicalCenters { get; set; }
        public virtual DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public virtual DbSet<Factor> Factors { get; set; }
        public virtual DbSet<MedicalCenter> MedicalCenters { get; set; }
        public virtual DbSet<MedicalCenterType> MedicalCenterTypes { get; set; }
        public virtual DbSet<PayFactor> PayFactors { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<VisitTime> VisitTimes { get; set; }
        public virtual DbSet<VisitRecord> VisitRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.CancelTimes)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.PatientId);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.Doctors)
            //    .WithRequired(e => e.User)
            //    .HasForeignKey(e => e.UserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.DoctorMedicalCenters)
            //    .WithOptional(e => e.User)
            //    .HasForeignKey(e => e.UserFormalId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Factors)
                .WithOptional(e => e.PatientUser)
                .HasForeignKey(e => e.PatientUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.VisitTimes)
                .WithOptional(e => e.PatientUser)
                .HasForeignKey(e => e.PatientUserId);

            modelBuilder.Entity<MedicalCenterType>()
                .HasMany(e => e.MedicalCenters)
                .WithOptional(e => e.MedicalCenterType)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.DoctorMedicalCenters)
                .WithRequired(e => e.Doctor)
                .HasForeignKey(e => e.DoctorId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.VisitRecords)
                .WithRequired(e => e.Doctor)
                .HasForeignKey(e => e.DoctorId);

            modelBuilder.Entity<MedicalCenter>()
                .HasMany(e => e.VisitRecords)
                .WithRequired(e => e.MedicalCenter)
                .HasForeignKey(e => e.MedicalCenterId);

            modelBuilder.Entity<Specialty>()
                .HasMany(e => e.VisitRecords)
                .WithOptional(e => e.Specialty)
                .HasForeignKey(e => e.SpecialtyId);

            modelBuilder.Entity<VisitRecord>()
                .HasMany(e => e.VisitTimes)
                .WithRequired(e => e.VisitRecord)
                .HasForeignKey(e => e.VisitRecordId);

            modelBuilder.Entity<Specialty>()
                .HasMany(x => x.ChildSpetialities)
                .WithOptional(x => x.ParentSpecialty)
                .HasForeignKey(x => x.ParentId);
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}