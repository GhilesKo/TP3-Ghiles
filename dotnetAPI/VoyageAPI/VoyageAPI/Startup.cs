using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VoyageAPI.Data;
using VoyageAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VoyageAPI.Options;
using VoyageAPI.MapperProfiles;

namespace VoyageAPI
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
           
            OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                // BearerFormat = "JWT",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                Scheme = "Bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".

            };

            OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference //The name of the previously defined security scheme.
                        {
                            Id = "Bearer", //The name of the previously defined security scheme.
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[]{ }
                }
            };

            OpenApiContact contactInfo = new OpenApiContact()
            {
                Name = "Ghiles Kouaou",
                Email = "ghiles_94@hotmail.com",
               
            };

            OpenApiInfo info = new OpenApiInfo()
            {
                Title = "Voyage API",
                Version = "v1",
                Contact = contactInfo,
                License = null,
            };


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", info);
                options.AddSecurityDefinition("Bearer", securityDefinition);
                options.AddSecurityRequirement(securityRequirements);
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("VoyageAPIContext")));

            services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            JwtSettings jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings),jwtSettings);

            services.AddSingleton(jwtSettings);

            services.AddAuthentication(options => {  // <- Configuration pour spécifier que le Token est de type Bearer
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;    // <- Oblige l'utilisation de HTTPS ou non
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,              // <- Valide l’URL d’émission du Token
                    ValidateAudience = false,            // <- Valide l’URL du client
                    ValidAudience = "https://localhost:16029",            // <- l’URL du client
                    ValidIssuer = "https://localhost:16029",                // <- l’URL du serveur
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)) // <- String secret
                };
            });
          


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VoyageAPI v1"));
            }
            app.UseCors("AllowAll");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
