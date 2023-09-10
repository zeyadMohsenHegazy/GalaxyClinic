using DataAccess.DAL.IRepo;
using DataAccess.DAL.Repo;
using DataAccess.DatabaseContext;

namespace DataProvider.DataProvider.Configuration
{
    public class ConfigDataProvider : IConfigDataProvider
    {
        private readonly ApplicationDbContext _context;
        public ConfigDataProvider(ApplicationDbContext context)
        {
            _context = context;
            doctorRepo = new DoctorRepo(context);
            patientRepo = new PatientRepo(context);
            specialityRepo = new SpecialityRepo(context);
            userRepo = new UserRepo(context);
            userTypeRepo = new UserTypeRepo(context);
            specialityRepo = new SpecialityRepo(context);
            statusRepo = new StatusRepo(context);
        }

        public IDoctorRepo doctorRepo { get; private set; }

        public IPatientRepo patientRepo { get; private set; }

        public ISpecialityRepo specialityRepo { get; private set; }

        public IUserTypeRepo userTypeRepo { get; private set; }

        public IUserRepo userRepo { get; private set; }

        public IStatusRepo statusRepo { get; private set; }

        public IDoctorShiftRepo doctorShiftRepo { get; private set; }
    }
}
