using apointment.Common;
using apointment.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apointment.Repository.IRepository
{
   public interface IRegisterRepository:IRepository<Register>
    {
        IEnumerable<Register> GetAll();
        Register GetUserDetails(string UserName);
        bool DeleteUser(long userProfileID);
        bool SetTempPasswordStatus(long UserID, bool status);


    }
}
