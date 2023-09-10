using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;

namespace DataAccess.DAL.Repo
{
    public class UserTypeRepo : IUserTypeRepo
    {
        private readonly ApplicationDbContext _context;
        public UserTypeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(UserTypeRequest request)
        {
            try
            {
                UserType newUserType = new UserType();
                newUserType.name = request.UserTypeName;
               
                newUserType.CreatedBy = request.UserId;
                newUserType.CreatedAt = DateTime.Now;
                newUserType.IsDeleted = false;
                newUserType.IsEnabled = true;

                _context.UserTypes.Add(newUserType);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<UserType> GetAll()
        {
            IEnumerable<UserType> userTypes = _context.UserTypes
                  .Where(x => x.IsDeleted == false && x.IsEnabled == true)
                  .ToList();

            return userTypes;
        }

        public UserType GetOne(GeneralRequest request)
        {
            var userType = _context.UserTypes
               .Where(z => z.IsDeleted == false && z.IsEnabled == true)
               .FirstOrDefault(z => z.typeId == request.Id);

            if (userType != null)
            {
                return userType;
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
                var userType = _context.UserTypes
                    .FirstOrDefault(z => z.typeId == request.Id);
                userType.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(UserTypeRequest request)
        {
            var updatedUserType = _context.UserTypes.Find(request.Id);
            if (updatedUserType != null)
            {
                try
                {
                    updatedUserType.name = request.UserTypeName;

                    updatedUserType.ModifiedAt = DateTime.Now;
                    updatedUserType.ModifiedBy = request.UserId;
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
