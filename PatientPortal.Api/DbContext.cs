using System;
using Microsoft.EntityFrameworkCore;
using PatientPortal.Api.Domain;

#nullable disable

namespace PatientPortal.Api
{
    public partial class PatientPortalDbContext : DbContext
    {
        public PatientPortalDbContext()
        { }

        public PatientPortalDbContext(DbContextOptions<PatientPortalDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Username);
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "bob",
                        // password1
                        Password = "10000.WqcaDoKgiNEloduj0hELNA==.P7r8Tq20te86gr8ZpwJRs5JmvMo2wiHxsTA95C3ZyqM="
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "bill",
                        // password2
                        Password = "10000.iLl3E45elWO0WEJp4EVc9Q==.3a/Bo6vx2NXweAodzPhLTSiFmJYInMRYXPYMA4eLYfE="
                    }
                )
        }
    }
}
