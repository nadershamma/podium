using System;
using System.IO;
using System.Reflection;
using Api.Application.Applicant;
using Api.Application.Mortgage;
using Api.Core.Applicant;
using Api.Core.Mortgage;
using Api.Infrastructure.Applicant;
using Api.Infrastructure.Data;
using Api.Infrastructure.Mortgage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            services.AddSwaggerGen(opt =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<ApplicantDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DataBaseContext")));
            services.AddDbContext<MortgageDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DataBaseContext")));
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IMortgageRepository, MortgageRepository>();
            services.AddScoped<IMortgageService, MortgageService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api"));

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}