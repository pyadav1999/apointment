using apointment.Common;
using apointment.Entity;
using apointment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apointment.Services.Interface
{
    public interface IPendingService : IService<PendingAppointment>
    {
        IEnumerable<AppointmentViewModel> GetAllActive(long AppointID);
        IEnumerable<AppointmentViewModel> GetAllUsers();
        AppointmentViewModel GetByID(long AppointID);
        void UpdateUserProfile(AppointmentViewModel model);
        bool RemoveUserProfile(long AppointID);
        bool DeleteUser(long AppointID);
        void Add(AppointmentViewModel model);
    }
    public interface INextService : IService<Next>
    {
        IEnumerable<AppointmentViewModel> GetAllUsers();
        AppointmentViewModel GetByID(long AppointID);
        bool RemoveUserProfile(long AppointID);
        void Add(AppointmentViewModel model);
    }
    public interface IPreviousService : IService<Previous>
    {
        IEnumerable<AppointmentViewModel> GetAllUsers();
        AppointmentViewModel GetByID(long AppointID);
        bool RemoveUserProfile(long AppointID);
        void Add(AppointmentViewModel model);
    }
    public interface IRatingService:IService<Rating>
    {
        IEnumerable<RatingViewModel> GetAllRating();
        RatingViewModel GetByID(long RateID);
        bool RemoveRating(long RateID);
        void Add(RatingViewModel model);
    }

    }

