
using Microsoft.EntityFrameworkCore;
using LMS_CMS_DAL.Models;
using LMS_CMS_BL.UOW;
using LMS_CMS_BL.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LMS_CMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //////// TO Open The Cors for the other domains:
            /// 1)
            string txt = "";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //////// JWT (Token)
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });


            //////// DB
            builder.Services.AddDbContext<LMS_CMS_Context>(
                op => op.UseSqlServer(builder.Configuration.GetConnectionString("con")));


            /// 2)
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(txt, builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            /// For generic repo:
            builder.Services.AddScoped<UOW>();


            /// For Auto Mapper:
            builder.Services.AddAutoMapper(typeof(AutoMapConfig).Assembly);

            var app = builder.Build();

            
            //////// Authentication
            app.UseAuthentication();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            /// 3)
            app.UseCors(txt);

            app.MapControllers();

            app.Run();
        }
    }
}
