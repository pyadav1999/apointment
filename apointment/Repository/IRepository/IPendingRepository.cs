using apointment.Common;
using apointment.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apointment.Repository.IRepository
{
    public interface IPendingRepository:IRepository<PendingAppointment>
    {
        IEnumerable<PendingAppointment> GetAllAppointments();
        PendingAppointment GetAppointmentDetails(long Id);
        void Insert(PendingAppointment model);
        bool DeleteUser(long ID);
    }
    public interface IRatingRepository : IRepository<Rating>
    {
        IEnumerable<Rating> GetAllRatings();
        Rating GetRatingDetails(long Id);
        bool DeleteRating(long ID);

    }

    public interface IPrevious : IRepository<Previous>
    {
        IEnumerable<Previous> GetAllAppointments();
        Previous GetAppointmentDetails(long Id);
    }
}
