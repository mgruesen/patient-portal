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
    }
}
