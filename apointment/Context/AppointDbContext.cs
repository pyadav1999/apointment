using apointment.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace apointment.Context
{
    public class AppointDbContext:DbContext
    {
        public DbSet<Register> Registers { get; set; }
        public DbSet<PendingAppointment> PendingAppointments { get; set; }
        public DbSet<Previous> Previous { get; set; }
        public DbSet<Next> Nexts { get; set; }

        public DbSet<Rating> Ratings { get; set; }
        public static AppointDbContext Create()
        {
            return new AppointDbContext();
        }
        public AppointDbContext()
            : base("name=ApointConn")
        {


            Debug.WriteLine("Creating SalesTrackDBContext: Instance - ");

            // show queries in Debug Window. 
            Database.Log = s => Debug.WriteLine(s);

            // Don't allow Lazy Loading
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new AppointDbInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().Property(h => h.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<ApplicationUser>().Property(x => x.UserName).HasMaxLength(32);

            //Map entity to table
            //Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.UserId, r.RoleId }).ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId }).ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }

    }
}