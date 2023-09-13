using Azure.Core;
using BCrypt.Net;
using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;
using System.Numerics;
using System.Text.RegularExpressions;

namespace DataAccess.DAL.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        //Create New User For Registeration
        public bool createUserDoctor(userDoctorRequest request)
        {
            try
            {
                if (validateUserNotExists(request) && 
                    validatePassword(request) &&
                    validatePhoneNumberAndEmailForDoctor(request))
                {
                    if (registerNewUser(request) && addDoctor(request))
                        return true;
                    else
                        return false;
                }
                //creating the User and the doctor
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool createUserSystem(userSystemRequest request)
        {
            try
            {
                if (validateUserNotExists(request) &&
                    validatePassword(request) &&
                    validatePhoneNumberAndEmailForSystemUser(request))
                {
                    if (registerNewUser(request) && addSystemUser(request))
                        return true;
                    else
                        return false;
                }
                else { return false; }
            }
            catch { return false; }
        }
        public bool createUserPatient(userPatientRequest request)
        {
            try
            {
                if (validateUserNotExists(request) &&
                    validatePassword(request) &&
                    validatePhoneNumberAndEmailForPatient(request))
                {
                    if (registerNewUser(request) && addPatient(request))
                        return true;
                    else
                        return false;
                }
                else { return false; }
                
            }
            catch { return false; }
        }

        #region Insertion into the tables
        private bool registerNewUser(UserRequest request)
        {
            try
            {
                User user = new User();
                user.userName = request.userName;
                user.password = passwordHasher.HashPassword(request.userPassword);
                user.userTypeId = _context.UserTypes
                    .FirstOrDefault(z => z.name == request.userType).typeId;

                user.CreatedAt = DateTime.Now;
                user.CreatedBy = request.UserId;
                user.IsEnabled = true;
                user.IsDeleted = false;

                _context.Users.Add(user);
                _context.SaveChanges();
                request.Id = user.userId;
                return true;
            }
            catch
            { return false; }
        }
        private bool addDoctor(userDoctorRequest request)
        {
            try
            {
                Doctor doctor = new Doctor();
                doctor.name = request.fullName;
                doctor.mobileNumber = request.mobileNumber;
                doctor.email = request.email;
                doctor.specialityId = request.doctorSpeciality;
                doctor.userId = request.Id;

                doctor.CreatedAt = DateTime.Now;
                doctor.CreatedBy = request.UserId;
                doctor.IsEnabled = true;
                doctor.IsDeleted = false;

                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        private bool addPatient(userPatientRequest request)
        {
            try
            {
                Patient patient = new Patient();
                patient.name = request.fullName;
                patient.mobileNumber = request.mobileNumber;
                patient.email = request.email;
                patient.userId = request.Id;

                patient.CreatedAt = DateTime.Now;
                patient.CreatedBy = request.UserId;
                patient.IsEnabled = true;
                patient.IsDeleted = false;

                _context.Patients.Add(patient);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        private bool addSystemUser(userSystemRequest request)
        {
            try
            {
                systemUser systemUser = new systemUser();
                systemUser.name = request.fullName;
                systemUser.mobileNumber = request.mobileNumber;
                systemUser.email = request.email;
                systemUser.userId = request.Id;

                systemUser.CreatedAt = DateTime.Now;
                systemUser.CreatedBy = request.UserId;
                systemUser.IsEnabled = true;
                systemUser.IsDeleted = false;

                _context.SystemUsers.Add(systemUser);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region validation in the creating on the users 
        //Validation on the mobile Number 
        private bool validateUserNotExists(UserRequest request)
        {
            try
            {
                var users = _context.Users
                    .Where(z => z.userName == request.userName)
                    .FirstOrDefault();
                if (users == null)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
        //Validation On the Password
        private bool validatePassword(UserRequest request)
        {
            try
            {
                if(request.userPassword.Length >= 11 &&
                    char.IsUpper(request.userPassword[0]))
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
        //Validation on the userName and the mobile number and the email is not existing
        private bool validatePhoneNumberAndEmailForDoctor(userDoctorRequest request)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            try
            {
                var doctors = _context.Doctors
                    .Where(z => z.email == request.email &&
                            z.mobileNumber == request.mobileNumber)
                    .FirstOrDefault();
                if (doctors == null && 
                    request.mobileNumber.Length == 11 &&
                    Regex.IsMatch(request.email,pattern))
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
        private bool validatePhoneNumberAndEmailForSystemUser
            (userSystemRequest request)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            try
            {
                var systemUser = _context.SystemUsers
                    .Where(z => z.email == request.email &&
                            z.mobileNumber == request.mobileNumber)
                    .FirstOrDefault();
                if (systemUser == null &&
                    request.mobileNumber.Length == 11 &&
                    Regex.IsMatch(request.email, pattern))
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
        private bool validatePhoneNumberAndEmailForPatient(userPatientRequest request)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            try
            {
                var patient = _context.Patients
                    .Where(z => z.email == request.email &&
                            z.mobileNumber == request.mobileNumber)
                    .FirstOrDefault();
                if (patient == null &&
                    request.mobileNumber.Length == 11 &&
                    Regex.IsMatch(request.email, pattern))
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
        #endregion


        #region Login
        public userLoginResponse userLogin(userLoginRequest request)
        {
            userLoginResponse response = new userLoginResponse();
            var user = _context.Users
                .Where(z => z.userName == request.userName)
                .Include(z => z.userType)
                .FirstOrDefault();
            var doc = _context.Doctors
                .Where(z => z.email == request.userName ||
                       z.mobileNumber == request.userName)
                .Include(z => z.user)
                    .ThenInclude(z => z.userType)
                .FirstOrDefault();
            var systemUser = _context.SystemUsers
               .Where(z => z.email == request.userName ||
                      z.mobileNumber == request.userName)
               .Include(z => z.user)
                   .ThenInclude(z => z.userType)
               .FirstOrDefault();
             var patient = _context.Patients
                .Where(z => z.email == request.userName ||
                       z.mobileNumber == request.userName)
                .Include(z => z.user)
                    .ThenInclude(z => z.userType)
                .FirstOrDefault();
            if (user != null && passwordHasher.VarifyPassword
                                    (request.password, user.password))
            {
                response.userType =
                    user.userType.name;
            }
            else if(doc != null && passwordHasher.VarifyPassword
                                    (request.password ,doc.user.password))
            {
                response.userType = doc.user.userType.name;
            }
            else if (systemUser != null && passwordHasher.VarifyPassword
                                    ( request.password,systemUser.user.password))
            {
                response.userType = doc.user.userType.name;
            }
            else if (patient != null && passwordHasher.VarifyPassword
                                    (request.password, patient.user.password))
            {
                response.userType = doc.user.userType.name;
            }
            else { response = null; }

            return response;
        }
        #endregion

        #region Reset Password
        
        #endregion
        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> users = _context.Users
                .Where(x => x.IsDeleted == false && x.IsEnabled == true)
                .ToList();

            return users;
        }


        public User GetOne(GeneralRequest request)
        {
            var user = _context.Users.FirstOrDefault(z => z.userId == request.Id);
            if (user != null && user.IsDeleted == false && user.IsEnabled == true)
            {
                return user;
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
                var user = _context.Users.FirstOrDefault(z => z.userId == request.Id);
                user.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Update(UserRequest request)
        {
            var updatedUser = _context.Users.Find(request.Id);
            if (updatedUser != null)
            {
                updatedUser.userName = request.userName;
                updatedUser.password = request.userPassword;

                updatedUser.ModifiedAt = DateTime.Now;
                updatedUser.ModifiedBy = request.UserId;

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
