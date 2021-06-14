using apointment.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apointment.ViewModels
{
    public class RatingViewModel
    {
        public RatingViewModel()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Rating, RatingViewModel>();

            });
        }
        public int rateid { get; set; }
        public int rating { get; set; }
        public string review { get; set; }
        public  int AppointId { get; set; }

        public int SenderId { get; set; }
        public int SpecialistId { get; set; }
    }
    
}