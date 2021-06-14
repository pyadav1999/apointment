using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace apointment.Entity
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string RegisterAs { get; set; }
        public string occupation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Boolean IsActive { get; set; }
        public string imgurl { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsDelete { get; set; }

        public Register()
        {
            this.RequestDate = DateTime.UtcNow;
        }
    }
}