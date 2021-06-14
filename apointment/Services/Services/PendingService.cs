using apointment.Common;
using apointment.Entity;
using apointment.Repository.IRepository;
using apointment.Services.Interface;
using apointment.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apointment.Services.Services
{
    public class PendingService : ServiceBase<PendingAppointment>, IPendingService
    {
        IPendingRepository _pendingRepository;
        public PendingService(IPendingRepository repository) : base(repository)
        {
            _pendingRepository = repository;
        }

        public void Add(AppointmentViewModel model)
        {
            PendingAppointment modelreturn = new PendingAppointment();
            if (model != null)
            {
                modelreturn.AppointmentDate = model.AppointmentDate;
                modelreturn.AppointmentTime = model.AppointmentTime;
                modelreturn.occupation = model.occupation;
                modelreturn.senderAddess = model.senderAddess;
                modelreturn.senderCity = model.senderCity;
                modelreturn.senderEmail = model.senderEmail;
                modelreturn.sendermobile = model.sendermobile;
                modelreturn.senderName = model.senderName;
                modelreturn.senderState = model.senderState;
                modelreturn.specialistAddress = model.specialistAddress;
                modelreturn.specialistcity = model.specialistcity;
                modelreturn.specialistEmail = model.specialistEmail;
                modelreturn.specialistmobile = model.specialistmobile;
                modelreturn.specialistName = model.specialistName;
                modelreturn.specialistState = model.specialistState;
                modelreturn.SenderId = model.SenderId;
                modelreturn.SpecialistId = model.SpecialistId;
                modelreturn.IsActive = model.IsActive;
                modelreturn.IsDelete = model.IsDelete;
                _pendingRepository.Insert(modelreturn);
                _pendingRepository.Save();
            }
        }

        public bool DeleteUser(long AppointID)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = _pendingRepository.DeleteUser(AppointID);
            }
            catch
            {
                isDeleted = false;
            }
            return isDeleted;
        }

        public IEnumerable<AppointmentViewModel> GetAllActive(long AppointID)
        {
            List<PendingAppointment> model = new List<PendingAppointment>();
            List<AppointmentViewModel> modelReturn = new List<AppointmentViewModel>();
            model = _pendingRepository.FindBy(x => x.IsActive == true && x.AppointId == AppointID).ToList();

            if (model.Count > 0)
            {
                modelReturn = model.Select(x => new AppointmentViewModel
                {
                    AppointId = x.AppointId,
                    SenderId = x.SenderId,
                    SpecialistId = x.SpecialistId,
                    IsActive = x.IsActive,
                    RequestDate = x.RequestDate,
                }).ToList();
            }

            return modelReturn;
        }

        public IEnumerable<AppointmentViewModel> GetAllUsers()
        {
            List<PendingAppointment> model = new List<PendingAppointment>();
            List<AppointmentViewModel> modelReturn = new List<AppointmentViewModel>();
            model = _pendingRepository.GetAllAppointments().ToList();
            if (model.Count > 0)
            {
                var x = new AppointmentViewModel();
                modelReturn = Mapper.Map<List<AppointmentViewModel>>(model);
                x = null;
            }
            return modelReturn; ;
        }

        public AppointmentViewModel GetByID(long AppointID)
        {
            PendingAppointment model = new PendingAppointment();
            AppointmentViewModel modelreturn = new AppointmentViewModel();
            model = _pendingRepository.Get(AppointID);
            if (model != null)
                modelreturn = Mapper.Map<PendingAppointment, AppointmentViewModel>(model);
            return modelreturn;
        }

        public bool RemoveUserProfile(long AppointID)
        {
            try
            {
                _pendingRepository.Delete(AppointID);
                _pendingRepository.Save();

                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public void UpdateUserProfile(AppointmentViewModel model)
        {
            PendingAppointment modelreturn = new PendingAppointment();
            modelreturn = _pendingRepository.Get(model.AppointId);
            modelreturn.AppointmentDate = model.AppointmentDate;
            modelreturn.AppointmentTime = model.AppointmentTime;
            modelreturn.occupation = model.occupation;
            modelreturn.senderAddess = model.senderAddess;
            modelreturn.senderCity = model.senderCity;
            modelreturn.senderEmail = model.senderEmail;
            modelreturn.sendermobile = model.sendermobile;
            modelreturn.senderName = model.senderName;
            modelreturn.senderState = model.senderState;
            modelreturn.specialistAddress = model.specialistAddress;
            modelreturn.specialistcity = model.specialistcity;
            modelreturn.specialistEmail = model.specialistEmail;
            modelreturn.specialistmobile = model.specialistmobile;
            modelreturn.specialistName = model.specialistName;
            modelreturn.specialistState = model.specialistState;
            modelreturn.SenderId = model.SenderId;
            modelreturn.SpecialistId = model.SpecialistId;
            modelreturn.IsActive = model.IsActive;
            modelreturn.IsDelete = model.IsDelete;
            _pendingRepository.Edit(modelreturn);
            _pendingRepository.Save();
        }
    }
    public class RatingService : ServiceBase<Rating>, IRatingService
    {
        IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository):base(ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public void Add(RatingViewModel model)
        {
            Rating modelsave = new Rating();
            if (model != null)
            {
                modelsave.AppointId = model.AppointId;
                modelsave.SenderId = model.SenderId;
                modelsave.SpecialistId = model.SpecialistId;
                modelsave.rating = model.rating;
                modelsave.review = model.review;
                _ratingRepository.Add(modelsave);
                _ratingRepository.Save();
            }
        }

        public IEnumerable<RatingViewModel> GetAllRating()
        {
            List<Rating> model = new List<Rating>();
            List<RatingViewModel> modelReturn = new List<RatingViewModel>();
            model = _ratingRepository.GetAllRatings().ToList();
            if (model.Count > 0)
            {
                var x = new RatingViewModel();
                modelReturn = Mapper.Map<List<RatingViewModel>>(model);
                x = null;
            }
            return modelReturn; 
        }


        public RatingViewModel GetByID(long RateID)
        {

            Rating model = new Rating();
            RatingViewModel modelreturn = new RatingViewModel();
            model = _ratingRepository.GetRatingDetails(RateID);
            if (model != null)
                modelreturn = Mapper.Map<Rating, RatingViewModel>(model);
            return modelreturn;
        }

        public bool RemoveRating(long RateID)
        {
            try
            {
                _ratingRepository.Delete(RateID);
                _ratingRepository.Save();

                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
    public class NextService : ServiceBase<Next>, INextService
    {

        INextRepository _nextRepository;
        public NextService(INextRepository repository) : base(repository)
        {
            _nextRepository = repository;
        }
            public void Add(AppointmentViewModel model)
            {
               Next modelreturn = new Next();
                if (model != null)
                {
                modelreturn.AppointId = model.AppointId;
                    modelreturn.AppointmentDate = model.AppointmentDate;
                    modelreturn.AppointmentTime = model.AppointmentTime;
                    modelreturn.occupation = model.occupation;
                    modelreturn.senderAddess = model.senderAddess;
                    modelreturn.senderCity = model.senderCity;
                    modelreturn.senderEmail = model.senderEmail;
                    modelreturn.sendermobile = model.sendermobile;
                    modelreturn.senderName = model.senderName;
                    modelreturn.senderState = model.senderState;
                    modelreturn.specialistAddress = model.specialistAddress;
                    modelreturn.specialistcity = model.specialistcity;
                    modelreturn.specialistEmail = model.specialistEmail;
                    modelreturn.specialistmobile = model.specialistmobile;
                    modelreturn.specialistName = model.specialistName;
                    modelreturn.specialistState = model.specialistState;
                    modelreturn.SenderId = model.SenderId;
                    modelreturn.SpecialistId = model.SpecialistId;
                    modelreturn.IsActive = model.IsActive;
                    modelreturn.IsDelete = model.IsDelete;
                    _nextRepository.Add(modelreturn);
                    _nextRepository.Save();
                }
            }
        public IEnumerable<AppointmentViewModel> GetAllUsers()
        {
            List<Next> model = new List<Next>();
            List<AppointmentViewModel> modelReturn = new List<AppointmentViewModel>();
            model = _nextRepository.GetAllAppointments().ToList();
            if (model.Count > 0)
            {
                var x = new AppointmentViewModel();
                modelReturn = Mapper.Map<List<AppointmentViewModel>>(model);
                x = null;
            }
            return modelReturn; ;
        }

        public AppointmentViewModel GetByID(long AppointID)
        {
           Next model = new Next();
            AppointmentViewModel modelreturn = new AppointmentViewModel();
            model = _nextRepository.GetAppointmentDetails(AppointID);
            if (model != null)
                modelreturn = Mapper.Map<Next, AppointmentViewModel>(model);
            return modelreturn;
        }

        public bool RemoveUserProfile(long AppointID)
        {
            try
            {
                _nextRepository.Delete(AppointID);
                _nextRepository.Save();

                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
    public class PreviousService : ServiceBase<Previous>, IPreviousService
    {

        IPrevious _previousRepository;
        public PreviousService(IPrevious repository) : base(repository)
        {
            _previousRepository = repository;
        }
        public void Add(AppointmentViewModel model)
        {
            Previous modelreturn = new Previous();
            if (model != null)
            {
                modelreturn.AppointId = modelreturn.AppointId;
                modelreturn.AppointmentDate = model.AppointmentDate;
                modelreturn.AppointmentTime = model.AppointmentTime;
                modelreturn.occupation = model.occupation;
                modelreturn.senderAddess = model.senderAddess;
                modelreturn.senderCity = model.senderCity;
                modelreturn.senderEmail = model.senderEmail;
                modelreturn.sendermobile = model.sendermobile;
                modelreturn.senderName = model.senderName;
                modelreturn.senderState = model.senderState;
                modelreturn.specialistAddress = model.specialistAddress;
                modelreturn.specialistcity = model.specialistcity;
                modelreturn.specialistEmail = model.specialistEmail;
                modelreturn.specialistmobile = model.specialistmobile;
                modelreturn.specialistName = model.specialistName;
                modelreturn.specialistState = model.specialistState;
                modelreturn.SenderId = model.SenderId;
                modelreturn.SpecialistId = model.SpecialistId;
                modelreturn.IsActive = model.IsActive;
                modelreturn.IsDelete = model.IsDelete;
                _previousRepository.Add(modelreturn);
                _previousRepository.Save();
            }
        }
        public IEnumerable<AppointmentViewModel> GetAllUsers()
        {
            List<Previous> model = new List<Previous>();
            List<AppointmentViewModel> modelReturn = new List<AppointmentViewModel>();
            model = _previousRepository.GetAllAppointments().ToList();
            if (model.Count > 0)
            {
                var x = new AppointmentViewModel();
                modelReturn = Mapper.Map<List<AppointmentViewModel>>(model);
                x = null;
            }
            return modelReturn; ;
        }

        public AppointmentViewModel GetByID(long AppointID)
        {
            Previous model = new Previous();
            AppointmentViewModel modelreturn = new AppointmentViewModel();
            model = _previousRepository.Get(AppointID);
            if (model != null)
                modelreturn = Mapper.Map<Previous, AppointmentViewModel>(model);
            return modelreturn;
        }

        public bool RemoveUserProfile(long AppointID)
        {
            try
            {
                _previousRepository.Delete(AppointID);
                _previousRepository.Save();

                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}