
using Microsoft.EntityFrameworkCore;
using LMS_CMS_DAL.Models;
using LMS_CMS_BL.UOW;
using LMS_CMS_BL.Config;

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
