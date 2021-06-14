using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace apointment.Entity
{
    public class PendingAppointment
    {
        [Key]
        public int AppointId { get; set; }
        public int SenderId { get; set; }
        public string senderName { get; set; }
        public string senderAddess { get; set; }
        public string senderState{get;set;}
        public string senderEmail { get; set; }
        public string sendermobile { get; set; }
        public string senderCity { get; set; }
        public int SpecialistId { get; set; }
        public string specialistName { get; set; }
        public string occupation { get; set; }
        public string specialistAddress { get; set; }
        public string specialistState { get; set; }
        public string specialistEmail { get; set; }
        public string specialistmobile { get; set; }
        public string specialistcity { get; set; }
        public DateTime AppointmentDate { get; set; } 
        public DateTime AppointmentTime { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsDelete { get; set; }
        public PendingAppointment()
        {
            this.RequestDate = DateTime.UtcNow;
        }
    }

    public class Rating
    {
        [Key]
        public int rateid { get; set; }
        public int rating { get; set; }
        public string review { get; set; }
        public int AppointId { get; set; }
        public int SenderId { get; set; }
        public int SpecialistId { get; set; }



    }
}