using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PatientPortal.Api.Extensions;
using PatientPortal.Api.Mappers;
using PatientPortal.Api.Services;

namespace PatientPortal.Api
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
            services.AddControllers();

            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IProviderService, ProviderService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IPatientMapper, PatientMapper>();
            services.AddTransient<IContactMapper, ContactMapper>();
            services.AddTransient<IProviderMapper, ProviderMapper>();
            services.AddTransient<IUserMapper, UserMapper>();

            services.AddTransient<IPasswordHash, PasswordHash>();

            services.AddDbContext<PatientPortalDbContext>(options =>
            {
                var dbConfig = Configuration.GetSection("Database");
                var connectionString = new SqlConnectionStringBuilder
                {
                    DataSource = dbConfig["Host"],
                    UserID = dbConfig["User"],
                    Password = dbConfig["Password"]
                }.ToString();


                options.UseSqlServer(connectionString);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PatientPortal.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PatientPortal.Api v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
