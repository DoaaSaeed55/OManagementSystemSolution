using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using OMS.Core.Repositries.Interfaces;
using OMS.Core.Services.Interfaces;
using OMS.Repositry.Data;
using OMS.Repositry.Repostries;
using OMS.Servise;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OMS.Repositry;
using OMS.Core;

namespace OManagementSystemSolution.APIs
{

    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<OMSDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnection"));
            });


            builder.Services.AddIdentity<User, IdentityRole>()
                  .AddEntityFrameworkStores<OMSDbContext>()
                  .AddDefaultTokenProviders();
            builder.Services.AddScoped<ITokenService, TokenServise>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options => {
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuer = true,
                         ValidIssuer = builder.Configuration["JWT:ValidIssues"],
                         ValidateAudience = true,
                         ValidAudience = builder.Configuration["JWT:Audience"],
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                     };

                 });



            //builder.Services.AddScoped<IGenericRepositry<Product>, GenericRepositry<Product>>();
            //builder.Services.AddScoped<IGenericRepositry<Customer>, GenericRepositry<Customer>>();
            //builder.Services.AddScoped<IGenericRepositry<Invoice>, GenericRepositry<Invoice>>();

            builder.Services.AddScoped(typeof(IGenericRepositry<>),typeof(GenericRepositry<>));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IOrderService,OrderService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IEmailService>(provider =>
            {
                var smtpServer = builder.Configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(builder.Configuration["EmailSettings:SmtpPort"]);
                var smtpUser = builder.Configuration["EmailSettings:SmtpUser"];
                var smtpPass = builder.Configuration["EmailSettings:SmtpPass"];
                return new EmailService(smtpServer, smtpPort, smtpUser, smtpPass);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "Admin", "Customer" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@example.com"
            };

            string adminPassword = "Admin@123";

            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }



}
