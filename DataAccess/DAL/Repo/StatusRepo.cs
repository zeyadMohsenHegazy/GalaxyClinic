using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;

namespace DataAccess.DAL.Repo
{
    public class StatusRepo : IStatusRepo
    {
        private readonly ApplicationDbContext _context;
        public StatusRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(StatusRequest request)
        {
            try
            {
                Status newStatus = new Status();
                newStatus.name = request.StatusName;

                newStatus.CreatedBy = request.UserId;
                newStatus.CreatedAt = DateTime.Now;
                newStatus.IsDeleted = false;
                newStatus.IsEnabled = true;

                _context.Statuses.Add(newStatus);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Status> GetAll()
        {
            IEnumerable<Status> statuses = _context.Statuses
                      .Where(x => x.IsDeleted == false && x.IsEnabled == true)
                      .ToList();

            return statuses;
        }

        public Status GetOne(GeneralRequest request)
        {
            var status = _context.Statuses
               .Where(z => z.IsDeleted == false && z.IsEnabled == true)
               .FirstOrDefault(z => z.statusId == request.Id);

            if (status != null)
            {
                return status;
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
                var status = _context.Statuses
                    .FirstOrDefault(z => z.statusId == request.Id);
                status.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(StatusRequest request)
        {
            var updatedStatus = _context.Statuses.Find(request.Id);
            if (updatedStatus != null)
            {
                try
                {
                    updatedStatus.name = request.StatusName;

                    updatedStatus.ModifiedAt = DateTime.Now;
                    updatedStatus.ModifiedBy = request.UserId;

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
