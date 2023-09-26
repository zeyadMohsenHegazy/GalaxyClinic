using Azure.Core;
using DataAccess.Auto_Mapper;
using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.API.Response;
using Models.DomainModels;

namespace DataAccess.DAL.Repo
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(DoctorRequest doctor)
        {
            try
            {
                CreateUser(doctor);
                Doctor newDoctor = CreateDoctorProps(doctor);
                _context.Doctors.Add(newDoctor);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void CreateUser(DoctorRequest doctor)
        {
            User user = new User();
            user.userName = doctor.Doctor_Name;
            user.password = doctor.mobileNumber;
            int userTypeID = _context.UserTypes.FirstOrDefault(z => z.name == "doctor").typeId;
            user.userTypeId = userTypeID;
            user.CreatedBy = doctor.UserId;
            user.CreatedAt = DateTime.Now;
            user.IsDeleted = false;
            user.IsEnabled = true;
            _context.Users.Add(user);
            _context.SaveChanges();
            doctor.User_Code = _context.Users.FirstOrDefault(z => z.password == user.password).userId;
        }
        private Doctor CreateDoctorProps(DoctorRequest doctor)
        {
            Doctor newDoctor = new Doctor();
                
            newDoctor.specialityId = doctor.Speciality_Code;
            newDoctor.name = doctor.Doctor_Name.ToLower();
            newDoctor.mobileNumber = doctor.mobileNumber;
            newDoctor.userId = doctor.User_Code;
            newDoctor.email = doctor.doctorEmail;

            newDoctor.CreatedBy = doctor.UserId;
            newDoctor.CreatedAt = DateTime.Now;
            newDoctor.IsDeleted = false;
            newDoctor.IsEnabled = true;

            return newDoctor;
        }

        public IEnumerable<Doctor> GetAll()
        {
            IEnumerable<Doctor> doctors = _context.Doctors
                .Include(x => x.speciality)
                .Where(z => z.IsDeleted == false && z.IsEnabled == true)
                .ToList();

            return doctors;
        }

        public Doctor GetOne(GeneralRequest doctor)
        {
            var newDoctor = _context.Doctors.Include(z => z.speciality)
                .Where(z => z.IsDeleted == false && z.IsEnabled == true)
                .FirstOrDefault(z => z.doctorId == doctor.Id);
                
            if (newDoctor != null)
            {
                return newDoctor;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(GeneralRequest request)
        {
            try
            {
                var doctor = _context.Doctors.FirstOrDefault(z => z.doctorId == request.Id);
                doctor.IsDeleted = true;
                doctor.ModifiedAt = DateTime.Now;
                doctor.ModifiedBy = request.UserId; 
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Update(DoctorRequest request)
        {
            var updatedDoc = _context.Doctors.Include(z => z.speciality).FirstOrDefault(z=> z.doctorId == request.Id);
            if (updatedDoc != null)
            {
                updatedDoc.name = request.Doctor_Name;
                updatedDoc.mobileNumber = request.mobileNumber;
                updatedDoc.specialityId = request.Speciality_Code;

                updatedDoc.ModifiedAt = DateTime.Now;
                updatedDoc.ModifiedBy = request.UserId;

                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }


    }
}
