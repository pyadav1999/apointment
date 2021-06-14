using apointment.Entity;
using apointment.Services.Interface;
using apointment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using apointment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;
using System.Net;
using apointment.Repository.IRepository;
using System.Dynamic;

namespace apointment.Controllers
{
    public class UserManagerController : Controller
    {
        IRegisterService _serviceRegister;
        IPendingService _pendingService;
        INextService _nextService;
        IPreviousService _previousService;
        IRatingService _ratingService;
        public UserManagerController(IRegisterService serviceRegister, IPendingService pendingService
            , INextService nextService, IPreviousService previousService, IRatingService ratingService)
        {
            _serviceRegister = serviceRegister;
            _pendingService = pendingService;
            _nextService = nextService;
            _previousService = previousService;
            _ratingService = ratingService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            if (!User.Identity.IsAuthenticated)
            { return RedirectToAction("Login", "Account"); }
            List<SignupViewModel> model = new List<SignupViewModel>();
            List<RatingViewModel> modelrate = new List<RatingViewModel>();
            dynamic modelreturn = new object();
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string username = identity.Name;
            SignupViewModel modeluserprofile = _serviceRegister.GetUserDetailsByEmail(username);
            model = _serviceRegister.GetAllActive(modeluserprofile.Id).ToList();
            modelrate = _ratingService.GetAllRating().ToList();
            dynamic modelsave = new ExpandoObject();
            modelsave.rating = modelrate;
            modelsave.user = model;
            ViewBag.userid = modeluserprofile.Id;
            ViewBag.username = modeluserprofile.Name;
            ViewBag.imgurl = "~/Uploads/" + modeluserprofile.imgurl;
            return View(modelsave);
        }

        public ActionResult Appointments()
        {
            if (!User.Identity.IsAuthenticated)
            { return RedirectToAction("Login", "Account"); }
            List<AppointmentViewModel> model = new List<AppointmentViewModel>();
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string username = identity.Name;
            SignupViewModel modeluserprofile = _serviceRegister.GetUserDetailsByEmail(username);
            ViewBag.userid = modeluserprofile.Id;
            ViewBag.username = modeluserprofile.Name;
            ViewBag.imgurl = "~/Uploads/" + modeluserprofile.imgurl;
            model = _pendingService.GetAllUsers().ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult uploadimg(HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("", "Please select an image");
                return RedirectToAction("UserProfile", "UserManager");
            }
            else
            {
                string path = Server.MapPath("~/Uploads/");
                string filename = Path.GetFileName(file.FileName);
                string fullpath = Path.Combine(path, filename);
                file.SaveAs(fullpath);
                SignupViewModel model = new SignupViewModel();
                var identity = (ClaimsIdentity)HttpContext.User.Identity;
                string username = identity.Name;
                SignupViewModel modeluserprofile = _serviceRegister.GetUserDetailsByEmail(username);
                model = _serviceRegister.GetByID(modeluserprofile.Id);
                model.imgurl = filename;

                _serviceRegister.UpdateUserProfile(model);

            }
            return RedirectToAction("UserProfile", "UserManager");
        }
        [HttpPost]
        public ActionResult Edit(long id)
        {
            var status = false;
            var message = string.Empty;
            SignupViewModel model = new SignupViewModel();
            try
            {

                model = _serviceRegister.GetByID(id);
                status = true;
                message = "Sucesss";
            }
            catch (Exception ex)
            {
                status = false;
                message = "failure";

            }
            return Json(new { Status = status, Message = message, Tempmodel = model });
        }
        [HttpPost]
        public ActionResult SaveChanges(SignupViewModel model)
        {
            var status = false;
            var message = "";

            try
            {
                if (model.Id > 0)
                {
                    _serviceRegister.UpdateUserProfile(model);
                    message = "Successfully updated the user profile.";
                    status = true;
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = "Failure";
                ModelState.AddModelError(message, ex);
            }
            return Json(new { Status = status, Message = message });
        }
        public ActionResult fixAppointment()
        {
            if (!User.Identity.IsAuthenticated)
            { return RedirectToAction("Login", "Account"); }
            List<SignupViewModel> model = new List<SignupViewModel>();
            List<RatingViewModel> ratemodel = new List<RatingViewModel>();

            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string username = identity.Name;
            SignupViewModel modeluserprofile = _serviceRegister.GetUserDetailsByEmail(username);
            model = _serviceRegister.GetAllActive(modeluserprofile.Id).ToList();
            ViewBag.Name = model[0].Name;
            ViewBag.Id = model[0].Id;
            ViewBag.ImgUrl = "~/Uploads/" + model[0].imgurl;
            List<SignupViewModel> model2 = new List<SignupViewModel>();
            ratemodel = _ratingService.GetAllRating().ToList();
            model2 = _serviceRegister.GetAllUsers().ToList();
            dynamic modelsave = new ExpandoObject();
            modelsave.ratings = ratemodel;
            modelsave.users = model2;

            return View(modelsave);
        }
        [HttpPost]
        public ActionResult Modal(long Id)
        {
            var status = false;
            var message = string.Empty;
            SignupViewModel model = new SignupViewModel();
            try
            {
                model = _serviceRegister.GetByID(Id);
                status = true;
                message = "Sucesss";
            }
            catch (Exception ex)
            {
                status = false;
                message = "failure";
            }
            return Json(new { Status = status, Message = message, Tempmodel = model });
        }

        [HttpPost]
        public ActionResult SendAppointment(AppointmentViewModel model)
        {
            var status = false;
            var message = "";

            SignupViewModel modelsender = new SignupViewModel();
            SignupViewModel modelspecialist = new SignupViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.AppointId > 0)
                    {
                        ModelState.AddModelError("", "Appointment Request Failed error()");
                    }
                    else
                    {
                        modelsender = _serviceRegister.GetByID(model.SenderId);
                        modelspecialist = _serviceRegister.GetByID(model.SpecialistId);
                        model.senderName = modelsender.Name;
                        model.senderEmail = modelsender.Email;
                        model.sendermobile = modelsender.Phone;
                        model.senderAddess = modelsender.Address;
                        model.senderCity = modelsender.City;
                        model.senderState = modelsender.State;
                        model.specialistName = modelspecialist.Name;
                        model.specialistEmail = modelspecialist.Email;
                        model.specialistmobile = modelspecialist.Phone;
                        model.specialistState = modelspecialist.State;
                        model.specialistAddress = modelspecialist.Address;
                        model.specialistcity = modelspecialist.City;
                        model.occupation = modelspecialist.occupation;
                        model.IsActive = true;
                        model.IsDelete = false;
                        if (model != null)
                        {
                            _pendingService.Add(model);
                            _pendingService.Save();

                            if (model.SenderId == modelsender.Id)
                            {
                                MailMessage msg = new MailMessage("19134@iiitu.ac.in", modelsender.Email);
                                msg.Subject = "Appointment,Request";
                                msg.Body = "Dear," + modelsender.Name + " You successfully send  Appointment request to " +
                                    modelspecialist.Name + "(" + modelspecialist.occupation + ")"
                                    + "Details Of Specialist is:" + " Name: " + modelspecialist.Name + ";" + " Occupation: " + modelspecialist.occupation + ";" + " Mobile:" + modelspecialist.Phone + ";"
                                   + " Email:" + modelspecialist.Email + ";"
                                   + " Date of Appointment:" + model.AppointmentDate.ToString("dd/MM/yyyy") + ";" +
                                   " Time of Appointment:" + model.AppointmentTime.ToString("hh:mm tt")
                                   + " Address:" + modelspecialist.Address + ";"
                                   + " Thanks,Pradyuman yadav";
                                msg.IsBodyHtml = false;
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                smtp.Host = "smtp.gmail.com";
                                smtp.Port = 587;
                                smtp.EnableSsl = true;

                                NetworkCredential nc = new NetworkCredential("19134@iiitu.ac.in", "yadav@123");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = nc;
                                smtp.Send(msg);
                            }
                            if (model.SpecialistId == modelspecialist.Id)
                            {
                                MailMessage msg = new MailMessage("19134@iiitu.ac.in", modelspecialist.Email);
                                msg.Subject = "Appointment,Request";
                                msg.Body = "Dear," + modelspecialist.Name + " You recevied Appointment request from " + modelsender.Name + "(" + modelsender.City + "," + modelsender.State + ")" + "" +
                                    "" +
                                    " To Accept/Ignore request  appointment visit appointment.com" +
                                    "" + "Details Of Sender is:" + " Name: " + modelsender.Name + ";" + " Mobile:" + modelsender.Phone + ";"
                                   + " Email:" + modelsender.Email + ";" + " Date of Appointment:" + model.AppointmentDate.ToString("dd/MM/yyyy") + ";" +
                                   " Time of Appointment:" + model.AppointmentTime.ToString("hh:mm tt") + " Address:" + modelsender.Address + ";"
                                   +
                                    "" +
                                    " Thanks,Pradyuman yadav";
                                msg.IsBodyHtml = false;
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                smtp.Host = "smtp.gmail.com";
                                smtp.Port = 587;
                                smtp.EnableSsl = true;

                                NetworkCredential nc = new NetworkCredential("19134@iiitu.ac.in", "yadav@123");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = nc;
                                smtp.Send(msg);
                            }

                        }

                        message = "Successfully Send Appointment Request.";
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                status = false;
                message = "could not send request some error occured!";
            }
            return Json(new
            {
                Status = status,
                Message = message,
                Tempmodel = model
            });
        }
        [HttpPost]
        public ActionResult CancleRequest(long Id)
        {
            var status = false;
            var message = "";
            try
            {
                if (Id > 0)
                {
                    PendingAppointment model = new PendingAppointment();
                    _pendingService.RemoveUserProfile(Id);
                    status = true;
                    message = "Your Appointment is cancled";
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;

            }
            return Json(new
            {
                Status = status,
                Message = message,
            });
        }
        [HttpPost]
        public ActionResult DeclineRequest(long Id)
        {
            var status = false;
            var message = "";
            try
            {
                if (Id > 0)
                {
                    AppointmentViewModel model = new AppointmentViewModel();
                    model = _pendingService.GetByID(Id);
                    model.IsActive = false;
                    _pendingService.UpdateUserProfile(model);
                    _pendingService.Save();
                    status = true;
                    message = "Appointment Rejected";
                }

            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return Json(new
            {
                Status = status,
                Message = message,
            });
        }
        public ActionResult AcceptRequest(long Id)
        {
            var status = false;
            var message = "";
            try
            {
                AppointmentViewModel model = new AppointmentViewModel();
                model = _pendingService.GetByID(Id);
                AppointmentViewModel modelreturn = new AppointmentViewModel();
                if (Id > 0)
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
                    _nextService.Add(modelreturn);
                    _nextService.Save();
                    _pendingService.RemoveUserProfile(model.AppointId);
                    _pendingService.Save();
                    status = true;
                    message = "Appointment is Fixed";
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return Json(new
            {
                Status = status,
                Message = message,
            });
        }

        public ActionResult Next()
        {
            if (!User.Identity.IsAuthenticated)
            { return RedirectToAction("Login", "Account"); }
            List<AppointmentViewModel> model = new List<AppointmentViewModel>();
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string username = identity.Name;
            SignupViewModel modeluserprofile = _serviceRegister.GetUserDetailsByEmail(username);
            ViewBag.userid = modeluserprofile.Id;
            ViewBag.username = modeluserprofile.Name;
            ViewBag.imgurl = "~/Uploads/" + modeluserprofile.imgurl;
            model = _nextService.GetAllUsers().ToList();

            return View(model);
        }
        public ActionResult Previous()
        {
            if (!User.Identity.IsAuthenticated)
            { return RedirectToAction("Login", "Account"); }
            List<AppointmentViewModel> model = new List<AppointmentViewModel>();
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string username = identity.Name;
            SignupViewModel modeluserprofile = _serviceRegister.GetUserDetailsByEmail(username);
            ViewBag.userid = modeluserprofile.Id;
            ViewBag.username = modeluserprofile.Name;
            ViewBag.imgurl = "~/Uploads/" + modeluserprofile.imgurl;
            model = _previousService.GetAllUsers().ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Completed(long Id)
        {
            var status = false;
            var message = "";
            try
            {
                AppointmentViewModel model = new AppointmentViewModel();
                model = _nextService.GetByID(Id);
                AppointmentViewModel modelreturn = new AppointmentViewModel();
                if (Id > 0)
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
                    _previousService.Add(modelreturn);
                    _previousService.Save();
                    _nextService.RemoveUserProfile(model.NextId);
                    _nextService.Save();
                    status = true;
                    message = "Appointment is Fixed";
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return Json(new
            {
                Status = status,
                Message = message,
            });
        }

        public ActionResult PreviousDelete(long Id)
        {
            var status = false;
            var message = "";
            try
            {
                AppointmentViewModel model = new AppointmentViewModel();
                model = _previousService.GetByID(Id);
                if (Id > 0)
                {
                    _previousService.RemoveUserProfile(model.PId);
                    _previousService.Save();
                    status = true;
                    message = "Data deleted Successfully";
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return Json(new
            {
                Status = status,
                Message = message,
            });
        }

        [HttpPost]
        public ActionResult Rating(RatingViewModel model)
        {
            var status = false;
            var message = "";
            try
            {
                if (model.rateid > 0)
                {
                    status = false;
                    message = "You already give the feedback";
                }
                else
                {
                    if (model != null)
                    {
                        _ratingService.Add(model);
                        _ratingService.Save();
                        status = true;
                        message = "Successfully Recorded Your Feedback";

                    }
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return Json(new { Status = status, Message = message, });
        }
    }
}