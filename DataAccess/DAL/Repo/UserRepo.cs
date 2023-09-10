using Azure.Core;
using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;
using System.Numerics;

namespace DataAccess.DAL.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }


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
                updatedUser.userName = request.User_Name;
                updatedUser.password = request.User_Password;

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
