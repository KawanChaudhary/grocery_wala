using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.AdminFactory.GetFacade;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.AddFacade;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.ProductFactory.EditFacade;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory;
using GroceryWala.BusinessLayer.GroceryWalaAbstractFactory.UserFactory.UserFacade;
using GroceryWala.DataAccessLayer;
using GroceryWala.DataAccessLayer.Repository.UnitOfWork;
using GroceryWala.DataServiceLayer.Services.Concrete;
using GroceryWala.DataServiceLayer.Services.Helper;
using GroceryWala.DataServiceLayer.Services.Interface;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace GroceryWala
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
            services.AddDbContext<GroceryWalaContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // adding identity

            services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<GroceryWalaContext>()
                .AddDefaultTokenProviders();

            // Cors 

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true).AllowCredentials();
                });
            });

            // Password Parameters
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                /*   options.SignIn.RequireConfirmedEmail = true;

                   options.Lockout.MaxFailedAccessAttempts = 5;*/
            });

            // Json
            services.AddControllers().AddNewtonsoftJson();


            services.AddControllers();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IGroceryWalaAbstractFactory, GroceryWalaAbstractFactory>();

            services.AddScoped<IAdminFactory, AdminFactory>();
            services.AddScoped<IUserFactory, UserFactory>();

            services.AddScoped<IAddFacade, AddFacade>();

            services.AddScoped<IEditFacade, EditFacade>();

            services.AddScoped<IGetFacade, GetFacade>();
            services.AddScoped<IUserFacade, UserFacade>();

            services.AddHttpContextAccessor();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();


            // JWT token Implementation

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = Configuration["JWT:ValidAudience"],
                     ValidIssuer = Configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                 };
             });

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("AllowOrigin");


            // Authentication
            app.UseAuthorization();

            app.UseAuthentication();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
