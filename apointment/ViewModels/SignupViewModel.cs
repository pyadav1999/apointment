using apointment.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace apointment.ViewModels
{
    public class SignupViewModel
    {
        public SignupViewModel()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Register, SignupViewModel>();
            });
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email id is not valid.")]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string RegisterAs { get; set; }
        public string occupation { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        public Boolean IsActive { get; set; }
        public string imgurl { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsDelete { get; set; }
    }
}