using Microsoft.EntityFrameworkCore;
using LMS_CMS_BL.UOW;
using LMS_CMS_BL.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using LMS_CMS_PL.Middleware;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Services;
using LMS_CMS_DAL.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

namespace LMS_CMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //////// TO Open The Cors for the other domains:
            /// 1)
            string txt = "AllowAllOrigins";

            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP System API", Version = "v1" });

                // Add JWT authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your token in the input below.\nExample: 'Bearer abc123xyz'"
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
                        Array.Empty<string>()
                    }
                });

                // Add custom header operation filter
                c.OperationFilter<AddCustomHeaderOperationFilter>();
            });



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
            builder.Services.AddDbContext<Octa_DbContext>(
                op => op.UseSqlServer(builder.Configuration.GetConnectionString("con")));


            builder.Services.AddScoped<DynamicDatabaseService>();
            builder.Services.AddScoped<DbContextFactoryService>();
            builder.Services.AddScoped<GenerateJWTService>();
            builder.Services.AddScoped<FileImageValidationService>();
            builder.Services.AddScoped<CancelInterviewDayMessageService>();


            /// 2)
            builder.Services.AddCors(option =>
            {
                //option.AddPolicy(txt, builder => {
                //    builder.AllowAnyOrigin();
                //    builder.AllowAnyMethod();
                //    builder.AllowAnyHeader();
                //    builder.WithHeaders("domain-name", "content-type", "Domain-Name");
                //});

                option.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.AllowAnyOrigin()  
                           .AllowAnyMethod()  
                           .AllowAnyHeader()  
                           .WithHeaders( "content-type", "Domain-Name");  
                });
            });

            /// For generic repo:
            builder.Services.AddScoped<UOW>();


            /// For Auto Mapper:
            builder.Services.AddAutoMapper(typeof(AutoMapConfig).Assembly);


            /// Json String Enum Converter:
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


            var app = builder.Build();

            ///////// send files
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/uploads"
            });


            /// 1) For DB Check
            app.UseMiddleware<DbConnection_Check_Middleware>();
            app.UseCors("AllowSpecificOrigin");

            //////// Authentication
            app.UseAuthentication();


            //////// Get Connection String
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/with-domain"), appBuilder =>
            {
                appBuilder.UseMiddleware<GetConnectionStringMiddleware>();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            /// For Endpoint, to check if the user has access for this endpoint or not
            /// Make sure to be here before UseAuthorization
            app.UseMiddleware<Endpoint_Authorization_Middleware>();


            /// 3)
            app.UseCors(txt);


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
