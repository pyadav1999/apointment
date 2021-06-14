using apointment.Common;
using apointment.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apointment.Repository.IRepository
{
    public interface INextRepository : IRepository<Next>
    {
        IEnumerable<Next> GetAllAppointments();
        Next GetAppointmentDetails(long Id);
        void deletenext(long Id);
    }
}
