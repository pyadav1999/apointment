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
    public class RegisterRepository : RepositoryBase<Register, AppointDbContext>, IRegisterRepository
    {
        public RegisterRepository(AppointDbContext context) : base(context)
        {
        }
        public IEnumerable<Register> GetAll()
        {

            return Context.Registers.ToList();
        }
        public Register GetUserDetails(string emailID)
        {
            Register result = null;
            using (var context = new AppointDbContext())
            {
                result = context.Registers.Where(x => x.Email == emailID).FirstOrDefault();
                if (result == null)
                    result = new Register();
            }
            return result;
        }
        public bool DeleteUser(long userProfileID)
        {
            bool isDeleted = false;
            try
            {
                SqlParameter param1 = new SqlParameter("@Id", userProfileID);
                var result = Context.Database.SqlQuery<int>("DeleteUserData @Id", param1).ToList();
                isDeleted = true;
            }
            catch
            {
                isDeleted = false;
            }
            return isDeleted;
        }

        public bool SetTempPasswordStatus(long UserID, bool status)
        {
            bool returnFlag = false;
            try
            {
                //SqlParameter param1 = new SqlParameter("@UserID", UserID);                
                //var result = Context.Database.SqlQuery<int>("exec proc_SetTempPassword @UserID", param1);

                var existingEntity = Context.Registers.Where(x => x.Id == UserID).FirstOrDefault();
                if (existingEntity != null)
                {
                    existingEntity.IsDelete = status;
                    Context.SaveChanges();
                    returnFlag = true;
                }
            }
            catch
            {
                returnFlag = false;
            }
            return returnFlag;
        }
    }
}