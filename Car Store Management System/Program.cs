
using Application.CommandHandlers;
using Application.Commands.User;
using Application.Fluent_Validation.User;
using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Domain.Interfaces;
using Domain.IRepostories;
using Domain.Models;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Car_Store_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddControllers();

            builder.Services.AddControllers()
                 .AddFluentValidation(fv =>
                 {
                     fv.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();

                 });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")));

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenRepository, SQLTokenRepository>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IImageStorageService, ImageStorateService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IReportReposiroty, ReportRepository>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IUserService, UserService>();



            // افزودن Identity برای مدیریت کاربران و نقش‌ها
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // اضافه کردن Authentication به سرویس‌ها
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };

                });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                // 🔹 تنظیم احراز هویت برای Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter 'Bearer {token}' (without quotes) in the field below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("BuyerSellerManagerPolicy", policy =>
                    policy.RequireRole("Buyer", "Seller", "Manager"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
