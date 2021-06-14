using apointment.Common;
using apointment.Entity;
using apointment.Repository.IRepository;
using apointment.Repository.Repository;
using apointment.Services.Interface;
using apointment.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apointment.Services
{
    public class RegisterService : ServiceBase<Register>, IRegisterService
    {
       IRegisterRepository _repository;
        public RegisterService(IRegisterRepository repository) :base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<SignupViewModel> GetAllUsers()
        {
            List<Register> model = new List<Register>();
            List<SignupViewModel> modelreturn = new List<SignupViewModel>();
            model = _repository.GetAll().ToList();
            if (model.Count > 0)
            {
                var x = new SignupViewModel();
                modelreturn = Mapper.Map<List<SignupViewModel>>(model);
                x = null;
            }
            return modelreturn; ;
        }
        public bool DeleteUser(long userProfileID)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = _repository.DeleteUser(userProfileID);
            }
            catch
            {
                isDeleted = false;
            }
            return isDeleted;
        }

        public IEnumerable<SignupViewModel> GetAllActive(long ID)
        {
            List<Register> model = new List<Register>();
            List<SignupViewModel> modelReturn = new List<SignupViewModel>();
            model = _repository.FindBy(x => x.IsActive == true && x.Id == ID).ToList();

            if (model.Count > 0)
            {
                modelReturn = model.Select(x => new SignupViewModel
                {
                   Id=x.Id,
                   Name=x.Name,
                   Email=x.Email,
                   Phone=x.Phone,
                   Address=x.Address,
                   City=x.City,
                   State=x.State,
                   RegisterAs=x.RegisterAs,
                   occupation=x.occupation,
                   IsActive=x.IsActive,
                   RequestDate=x.RequestDate,
                   imgurl=x.imgurl,
                }).ToList();
            }

            return modelReturn;
        }

        public SignupViewModel GetByEmailID(string EmailID)
        {
            Register model = new Register();
            SignupViewModel modelReturn = new SignupViewModel();
            model = _repository.FindBy(x => x.Email == EmailID).FirstOrDefault();
           

            if (model != null)
            {
                modelReturn.Email = model.Email;
                modelReturn.Address = model.Address;
                modelReturn.City = model.City;
                modelReturn.Id = model.Id;
                modelReturn.Name = model.Name;
                modelReturn.IsActive = model.IsActive;
                modelReturn.State = model.State;
                modelReturn.RegisterAs = model.RegisterAs;
                modelReturn.occupation = model.occupation;
                modelReturn.Phone = model.Phone;
                modelReturn.RequestDate = model.RequestDate;
                modelReturn.imgurl = model.imgurl;
            }

            return modelReturn;
        }

        public SignupViewModel GetByID(long UserProfileID)
        {
            Register model = new Register();
            SignupViewModel modelreturn = new SignupViewModel();
            model =_repository.Get(UserProfileID);
            if(model!=null)
                modelreturn = Mapper.Map<Register, SignupViewModel>(model);
            return modelreturn;
        }

        public SignupViewModel GetUserDetailsByEmail(string EmailID)
        {

            Register model = new Register();
            SignupViewModel modelReturn = new SignupViewModel();
            model = _repository.GetUserDetails(EmailID);


            if (model != null)
            {
                modelReturn.Email = model.Email;
                modelReturn.Address = model.Address;
                modelReturn.City = model.City;
                modelReturn.Id = model.Id;
                modelReturn.Name = model.Name;
                modelReturn.IsActive = model.IsActive;
                modelReturn.State = model.State;
                modelReturn.RegisterAs = model.RegisterAs;
                modelReturn.occupation = model.occupation;
                modelReturn.Phone = model.Phone;
                modelReturn.RequestDate = model.RequestDate;
                modelReturn.imgurl = model.imgurl;
            }

            return modelReturn;
        }

        public bool RemoveUserProfile(long UserProfileID)
        {
            try
            {
                    _repository.Delete(UserProfileID);
                    _repository.Save();

                return true;
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        public bool SetTempPasswordStatus(long UserID, bool status)
        {
            var flag = false;
            try
            {
                flag = _repository.SetTempPasswordStatus(UserID, status);
                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void imgupload(SignupViewModel model)
        {
            Register modelSave = new Register();
            modelSave = _repository.Get(model.Id);
            modelSave.imgurl = model.imgurl;

            _repository.Edit(modelSave);
            _repository.Save();
        }
        public void UpdateUserProfile(SignupViewModel model)
        {
            Register modelSave = new Register();
            modelSave = _repository.Get(model.Id);
            modelSave.Name = model.Name;
            modelSave.Phone = model.Phone;
            modelSave.Email = model.Email;
            modelSave.Address = model.Address;
            modelSave.City = model.City;
            modelSave.State = model.State;
            modelSave.Password = model.Password;
            modelSave.imgurl = model.imgurl;
            
            _repository.Edit(modelSave);
            _repository.Save();
        }
    }
}