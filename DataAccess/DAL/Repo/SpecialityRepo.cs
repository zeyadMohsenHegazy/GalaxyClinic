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
    public class SpecialityRepo : ISpecialityRepo
    {
        private readonly ApplicationDbContext _context;
        public SpecialityRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(SpecialityRequest speciality)
        {
            try
            {
                Speciality newSpeciality = new Speciality();
                newSpeciality.name = speciality.Speciality_Name;
                newSpeciality.CreatedBy = speciality.UserId;
                newSpeciality.CreatedAt = DateTime.Now;
                newSpeciality.IsDeleted = false;
                newSpeciality.IsEnabled = true;
                _context.Specialities.Add(newSpeciality);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public IEnumerable<Speciality> GetAll()
        {
            IEnumerable<Speciality> specialities = _context.Specialities
                 .Where(x => x.IsDeleted == false && x.IsEnabled == true)
                 .ToList();

            return specialities;
        }

        public Speciality GetOne(GeneralRequest request)
        {
            var speciality = _context.Specialities
                .Where(z => z.IsDeleted == false && z.IsEnabled == true)
                .FirstOrDefault(z => z.specialityId == request.Id);

            if (speciality != null)
            {
                return speciality;
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
                var speciality = _context.Specialities
                    .FirstOrDefault(z => z.specialityId == request.Id);
                speciality.IsDeleted = true;
                speciality.ModifiedBy = request.UserId;
                speciality.ModifiedAt = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(SpecialityRequest request)
        {
            var updatedSpecialiity = _context.Specialities.Find(request.Speciality_Code);
            if (updatedSpecialiity != null)
            {
                updatedSpecialiity.name = request.Speciality_Name;

                updatedSpecialiity.ModifiedAt = DateTime.Now;
                updatedSpecialiity.ModifiedBy = request.UserId;

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
