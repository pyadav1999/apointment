using apointment.Common;
using apointment.Context;
using apointment.Entity;
using apointment.Models;
using apointment.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace apointment.Repository.Repository
{
    public class PendingAppointmentRepository : RepositoryBase<PendingAppointment, AppointDbContext>, IPendingRepository
    {
        public PendingAppointmentRepository(AppointDbContext context) : base(context)
        {

        }

        public bool DeleteUser(long ID)
        {
            bool isDeleted = false;
            try
            {
                SqlParameter param1 = new SqlParameter("@AppointId", ID);
                var result = Context.Database.SqlQuery<int>("DeleteUserData @AppointId", param1).ToList();
                isDeleted = true;
            }
            catch
            {
                isDeleted = false;
            }
            return isDeleted;
        }

        public IEnumerable<PendingAppointment> GetAllAppointments()
        {
            return Context.PendingAppointments.ToList();
        }

        public PendingAppointment GetAppointmentDetails(long Id)
        {
            PendingAppointment result = null;
            using (var context = new AppointDbContext())
            {
                result = context.PendingAppointments.Where(x => x.AppointId == Id).FirstOrDefault();
                if (result == null)
                    result = new PendingAppointment();
            }
            return result;
        }

        public void Insert(PendingAppointment model)
        {
            if(model!=null)
            {
                Context.PendingAppointments.Add(model);
            }
            
        }
    }
    public class RatingRepository:RepositoryBase<Rating, AppointDbContext>, IRatingRepository
    {
        public RatingRepository(AppointDbContext context) : base(context)
        {

        }

        public bool DeleteRating(long ID)
        {

            bool isDeleted = false;
            try
            {
                SqlParameter param1 = new SqlParameter("@rateid", ID);
                var result = Context.Database.SqlQuery<int>("DeleteUserData @rateid", param1).ToList();
                isDeleted = true;
            }
            catch
            {
                isDeleted = false;
            }
            return isDeleted;
        }

        public IEnumerable<Rating> GetAllRatings()
        {
            return Context.Ratings.ToList();
        }

        public Rating GetRatingDetails(long Id)
        {
            Rating result = null;
            using (var context = new AppointDbContext())
            {
                result = context.Ratings.Where(x => x.rateid == Id).FirstOrDefault();
                if (result == null)
                    result = new Rating();
            }
            return result;
        }
    }
    public class NextReository : RepositoryBase<Next, AppointDbContext>, INextRepository
    {
        public NextReository(AppointDbContext context) : base(context)
        {

        }

        public void deletenext(long Id)
        {
            Next result = null;
            using (var context = new AppointDbContext())
            {
                result = context.Nexts.Where(x => x.AppointId == Id).FirstOrDefault();
                if (result == null)
                    result = new Next();
            }
            Context.Nexts.Remove(result);
        }

        public IEnumerable<Next> GetAllAppointments()
        {
            return Context.Nexts.ToList();
        }

        public Next GetAppointmentDetails(long Id)
        {
            Next result = null;
            using (var context = new AppointDbContext())
            {
                result = context.Nexts.Where(x => x.AppointId == Id).FirstOrDefault();
                if (result == null)
                    result = new Next();
            }
            return result;
        }
    }
    public class PreviousReository : RepositoryBase<Previous, AppointDbContext>, IPrevious
    {
        public PreviousReository(AppointDbContext context) : base(context)
        {

        }

        public IEnumerable<Previous> GetAllAppointments()
        {
            return Context.Previous.ToList();
        }

        public Previous GetAppointmentDetails(long Id)
        {
            Previous result = null;
            using (var context = new AppointDbContext())
            {
                result = context.Previous.Where(x => x.AppointId == Id).FirstOrDefault();
                if (result == null)
                    result = new Previous();
            }
            return result;
        }
    }
}