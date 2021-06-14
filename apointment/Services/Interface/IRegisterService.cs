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
    public interface IRegisterService : IService<Register>
    {
        IEnumerable<SignupViewModel> GetAllActive(long ID);
        IEnumerable<SignupViewModel> GetAllUsers();
        SignupViewModel GetByEmailID(string EmailID);
        SignupViewModel GetByID(long UserProfileID);
        void imgupload(SignupViewModel model);
        void UpdateUserProfile(SignupViewModel model);
        SignupViewModel GetUserDetailsByEmail(string EmailID);
        bool RemoveUserProfile(long UserProfileID);
        bool DeleteUser(long userProfileID);
        bool SetTempPasswordStatus(long UserID, bool status);
        
    }
}
