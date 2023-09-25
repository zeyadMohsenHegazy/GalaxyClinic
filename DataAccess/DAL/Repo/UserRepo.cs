using Azure.Core;
using BCrypt.Net;
using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.API.Response.ConfigResponse;
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

        #region Create New User For Registeration
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
        public bool createUserSystem(systemUserRequest request)
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
        #endregion

        #region Insertion into the tables
        private bool registerNewUser(UserRequest request)
        {
            try
            {
                User user = new User();
                user.userName = request.userName;
                user.password = passwordHasher.HashPassword(request.userPassword);
                user.userTypeId = _context.UserTypes.Where(z => z.name == request.userTypeName).FirstOrDefault().typeId;

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
                doctor.name = request.doctorName;
                doctor.mobileNumber = request.doctorMobileNumber;
                doctor.email = request.doctorEmail;
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
                patient.name = request.pateintName;
                patient.mobileNumber = request.patientMobileNumber;
                patient.email = request.patientEmail;
                patient.userId = request.Id;
                patient.dateOfBirth = request.patientDateOfBirth;

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
        private bool addSystemUser(systemUserRequest request)
        {
            try
            {
                systemUser systemUser = new systemUser();
                systemUser.name = request.systemUserName;
                systemUser.mobileNumber = request.systemUserMobileNumber;
                systemUser.email = request.systemUserEmail;
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
                    .Where(z => z.email == request.doctorEmail &&
                            z.mobileNumber == request.doctorMobileNumber)
                    .FirstOrDefault();
                if (doctors == null && 
                    request.doctorMobileNumber.Length == 11 &&
                    Regex.IsMatch(request.doctorEmail,pattern))
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
        private bool validatePhoneNumberAndEmailForSystemUser
            (systemUserRequest request)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            try
            {
                var systemUser = _context.SystemUsers
                    .Where(z => z.email == request.systemUserEmail &&
                            z.mobileNumber == request.systemUserMobileNumber)
                    .FirstOrDefault();
                if (systemUser == null &&
                    request.systemUserMobileNumber.Length == 11 &&
                    Regex.IsMatch(request.systemUserEmail, pattern))
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
                    .Where(z => z.email == request.patientEmail &&
                            z.mobileNumber == request.patientMobileNumber)
                    .FirstOrDefault();
                if (patient == null &&
                    request.patientMobileNumber.Length == 11 &&
                    Regex.IsMatch(request.patientEmail, pattern))
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

            var user = GetUserByUserName(request.userName);
            var doc = GetDoctorByEmailOrMobileNumber(request.userName);
            var systemUser = GetSystemUserByEmailOrMobileNumber(request.userName);
            var patient = GetPatientByEmailOrMobileNumber(request.userName);

            if (user != null && (passwordHasher
                        .VarifyPassword(request.password, user.password) 
                                    || request.password == user.password))
            {
                response = CreateUserLoginResponse(user);
            }
            else if (doc != null && (passwordHasher
                        .VarifyPassword(request.password, doc.user.password) 
                                    || request.password == doc.user.password))
            {
                response = CreateUserLoginResponse(doc.user);
            }
            else if (systemUser != null && (passwordHasher
                        .VarifyPassword(request.password, systemUser.user.password) 
                                    || request.password == systemUser.user.password))
            {
                response = CreateUserLoginResponse(systemUser.user);
            }
            else if (patient != null && (passwordHasher
                        .VarifyPassword(request.password, patient.user.password) 
                                    || request.password == patient.user.password))
            {
                response = CreateUserLoginResponse(patient.user);
            }

            return response;
        }
        #endregion

        #region Login Helper Methods
        private User GetUserByUserName(string userName)
        {
            return _context.Users
                .Where(u => u.userName == userName)
                .Include(u => u.userType)
                .FirstOrDefault();
        }

        private Doctor GetDoctorByEmailOrMobileNumber(string userName)
        {
            return _context.Doctors
                .Where(d => d.email == userName || d.mobileNumber == userName)
                .Include(d => d.user)
                .ThenInclude(u => u.userType)
                .FirstOrDefault();
        }

        private systemUser GetSystemUserByEmailOrMobileNumber(string userName)
        {
            return _context.SystemUsers
                .Where(su => su.email == userName || su.mobileNumber == userName)
                .Include(su => su.user)
                .ThenInclude(u => u.userType)
                .FirstOrDefault();
        }

        private Patient GetPatientByEmailOrMobileNumber(string userName)
        {
            return _context.Patients
                .Where(p => p.email == userName || p.mobileNumber == userName)
                .Include(p => p.user)
                .ThenInclude(u => u.userType)
                .FirstOrDefault();
        }

        private userLoginResponse CreateUserLoginResponse(User user)
        {
            return new userLoginResponse
            {
                userId = user.userId,
                userType = user.userType.name,
                userName = user.userName,
                userToken = user.password
            };
        }
        #endregion

        #region Forget and Reset Password
        public forgetPasswordResponse forgetPassword(forgetPasswordRequest request)
        {
            forgetPasswordResponse response = new forgetPasswordResponse();
            try
            {
                var userType = _context.UserTypes
                    .Where(z => z.name == request.userTypeName)
                    .FirstOrDefault();
                if (userType.name == "doctor")
                {
                    var doctor = _context.Doctors
                        .FirstOrDefault(z => z.email == request.userEmailOrMobile || 
                                        z.mobileNumber == request.userEmailOrMobile);
                    if(doctor != null)
                    {
                        response.userId = doctor.userId;
                    }
                }
                else if (userType.name == "patient")
                {   
                    var patient = _context.Patients
                        .FirstOrDefault(z => z.email == request.userEmailOrMobile ||
                                        z.mobileNumber == request.userEmailOrMobile);
                    if(patient != null)
                    {
                        response.userId = patient.userId;
                    }
                }
                else if(userType.name == "userSystem")
                {
                    var userSystem = _context.SystemUsers
                        .FirstOrDefault(z => z.email == request.userEmailOrMobile ||
                                        z.mobileNumber == request.userEmailOrMobile);
                    if (userSystem != null)
                    {
                        response.userId = userSystem.userId;
                    }
                }
                return response;
            }
            catch { return null; }
        }

        public bool resetPassword(resertPasswordRequest request)
        {
            try
            {
                var user = _context.Users.Find(request.userId);
                // checks that the two passwords is identical 
                // and the password length equal or more than 10 char
                // and the first letter is Uppercase 
                if (request.userPassword == request.confirmPassword && 
                    request.userPassword.Length >= 11 &&
                    char.IsUpper(request.userPassword[0]))
                {
                    user.password = passwordHasher.HashPassword(request.userPassword);
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch { return false; }
        }
        #endregion

        #region Crud Operation
        //public IEnumerable<User> GetAll()
        //{
        //    IEnumerable<User> users = _context.Users
        //        .Where(x => x.IsDeleted == false && x.IsEnabled == true)
        //        .ToList();

        //    return users;
        //}


        //public User GetOne(GeneralRequest request)
        //{
        //    var user = _context.Users.FirstOrDefault(z => z.userId == request.Id);
        //    if (user != null && user.IsDeleted == false && user.IsEnabled == true)
        //    {
        //        return user;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        //public bool Remove(GeneralRequest request)
        //{
        //    try
        //    {
        //        var user = _context.Users.FirstOrDefault(z => z.userId == request.Id);
        //        user.IsDeleted = true;
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}


        //public bool Update(UserRequest request)
        //{
        //    var updatedUser = _context.Users.Find(request.Id);
        //    if (updatedUser != null)
        //    {
        //        updatedUser.userName = request.userName;
        //        updatedUser.password = request.userPassword;

        //        updatedUser.ModifiedAt = DateTime.Now;
        //        updatedUser.ModifiedBy = request.UserId;

        //        try
        //        {
        //            _context.SaveChanges();
        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //        return false;
        //}
        #endregion

    }
}
