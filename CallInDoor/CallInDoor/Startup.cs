using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Domain;
using Domain.Entities;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using CallInDoor.Models;
using CallInDoor.Config.Extentions;
using CallInDoor.Config;
using CallInDoor.Config.Middleware;
using Domain.Utilities;
using Service;
using Service.Interfaces.JwtManager;
using Service.Interfaces.Common;
using Service.Interfaces.Account;
using Service.Interfaces.ServiceType;
using Service.Interfaces.Category;
using AutoMapper;
using Domain.DTO.Account;

namespace CallInDoor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLocalization();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            //cors origin
            services.AddCors(opt =>
            {

                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(
                               "http://localhost:4200",
                               "http://localhost:443",
                               "http://localhost:80",
                                "https://localhost:4200",
                               "https://localhost:443",
                               "https://localhost:80",
                               "http://localhost:3000",
                               "http://localhost:3001",
                               "http://localhost:4321",
                               "https://localhost:44374",
                               "http://panel.callindoor.ir",
                                "https://panel.callindoor.ir"

                               )
                    .AllowCredentials();
                });

            });



            //setting  of identity
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789@#+-اآبپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیئيك";

                //confirm Phone Number
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = true;
            })
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            //این توکن میسازه   باید باشه برا چنج پسورد و.کانفیرم ایمیل
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<DataContext>();



            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true);





            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.AddOurAuthentication(appSettings);

            //swagger config
            services.AddOurSwaager();

            services.AddControllersWithViews();
            services.AddRazorPages();


            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(new ModelStateCheckFilter());
            })
             .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization()
            .AddNewtonsoftJson();


            //services.AddControllers().AddNewtonsoftJson();


            //inject autoMapper
            services.AddAutoMapper(typeof(LoginDTO));




            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ICategoryService, CategoryService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<ErrorWrappingMiddleware2>();
            if (env.IsDevelopment())
            {
                ////////////app.UseDeveloperExceptionPage();
                ////////////app.UseDatabaseErrorPage();
            }
            else
            {
                //////////app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                ////////////app.UseHsts();
            }

            /*.........................swagger..........................*/
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "v1");
            });



            app.UseHttpsRedirection();
            app.UseStaticFiles();


            //2 زبانه
            var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo(PublicHelper.persianCultureName),
                new CultureInfo(PublicHelper.EngCultureName)
            };
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(PublicHelper.persianCultureName),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider(),
                }
            };
            app.UseRequestLocalization(options);


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }
    }
}
