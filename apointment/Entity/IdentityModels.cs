using System;
using System.Security.Claims;
using System.Threading.Tasks;
using apointment.Context;
using apointment.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace apointment.Entity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual Register Register { get; set; }
        
    }

}