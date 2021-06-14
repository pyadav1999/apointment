using apointment.Context;
using apointment.Entity;
using apointment.Models;
using apointment.Repository.IRepository;
using apointment.Repository.Repository;
using apointment.Services;
using apointment.Services.Interface;
using apointment.Services.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;

namespace apointment
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ITextEncoder, Base64UrlTextEncoder>();
            container.RegisterType<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>();
            container.RegisterType<IDataSerializer<AuthenticationTicket>, TicketSerializer>();
            container.RegisterInstance(new DpapiDataProtectionProvider().Create("ASP.NET Identity"));
            container.RegisterType<IOwinContext>(new InjectionFactory(o => HttpContext.Current.GetOwinContext()));
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));



            container.RegisterType<IIdentityMessageService, EmailService>("production");
           // container.RegisterType<IIdentityMessageService, MailtrapEmailService>("debugging");


            //var json = container.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            //config.Formatters.Remove(config.Formatters.XmlFormatter);


            //container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            //container.RegisterType<ApplicationRoleManager>(new PerRequestLifetimeManager());

            container.RegisterType<IIdentityMessageService>(new InjectionFactory(c =>
            {
                try
                {
                    // not in debug mode and not local request => we are in production
                    if (!HttpContext.Current.IsDebuggingEnabled && !HttpContext.Current.Request.IsLocal)
                    {
                        return c.Resolve<IIdentityMessageService>("production");
                    }
                }
                catch (Exception)
                {
                    // Catching exceptions for cases if there is no Http request available
                }
                return c.Resolve<IIdentityMessageService>("debugging");
            }));

            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new InjectionConstructor(typeof(AppointDbContext)));

            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole, string, IdentityUserRole>>(
                new InjectionConstructor(typeof(AppointDbContext)));

            container.RegisterType<AppointDbContext>(new HierarchicalLifetimeManager());
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new InjectionConstructor(new AppointDbContext()));
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole, string, IdentityUserRole>>(new InjectionConstructor(typeof(AppointDbContext)));
            container.RegisterType<UserManager<ApplicationUser>, ApplicationUserManager>(new HierarchicalLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new HierarchicalLifetimeManager());
           // container.RegisterType<ApplicationRoleManager>(new HierarchicalLifetimeManager());
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<DbContext, AppointDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<ApplicationSignInManager>();
           
            container.RegisterType<IRegisterService, RegisterService> (new HierarchicalLifetimeManager());
            container.RegisterType<IRegisterRepository, RegisterRepository> (new HierarchicalLifetimeManager());

            container.RegisterType<IPendingService,PendingService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPendingRepository,PendingAppointmentRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<INextService, NextService>(new HierarchicalLifetimeManager());
            container.RegisterType<INextRepository, NextReository>(new HierarchicalLifetimeManager());

            container.RegisterType<IPreviousService, PreviousService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPrevious, PreviousReository>(new HierarchicalLifetimeManager());

            container.RegisterType<IRatingService, RatingService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRatingRepository, RatingRepository>(new HierarchicalLifetimeManager());


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}