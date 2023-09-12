using Microsoft.EntityFrameworkCore;
using Models.DomainModels;

namespace DataAccess.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=NewGalaxyClinic; Integrated Security=true; TrustServerCertificate=true");
            }
        }


        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorShift> DoctorShifts { get; set; }
        public virtual DbSet<doctorShiftDay> DoctorShiftDays { get; set; }
        public virtual DbSet<doctorShiftDayTime> DoctorShiftDayTimes { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Speciality> Specialities { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<systemUser> SystemUsers { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationAttachment> ReservationAttachments { get; set; }
        

    }
}
