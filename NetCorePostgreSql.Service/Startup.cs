using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCorePostgreSql.Core.Infrastructure;
using NetCorePostgreSql.Core.Repository;
using NetCorePostgreSql.Data.Context;

namespace NetCorePostgreSql.Service
{
    public class Startup
    {
        private IContainer _autofacContainer;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
                            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(jwtBearerOptions =>
   {
       jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
       {
           ValidateActor = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = Configuration["Issuer"],
           ValidAudience = Configuration["Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]))
       };
   });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc(options =>   //Veri tipi json olarak ayarlandı.
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            });
            // Setup Autofac integration
            var builder = new ContainerBuilder();

            // Autofac registration calls can go here.
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();

            // If the container requires many registrations or registrations that are shared with other
            // containers, builder.RegisterModule is a useful API.
            // builder.RegisterModule(new MyAutofacModule);

            // Dependency Injection: Adds ASP.NET Core-registered services to the Autofac container
            builder.Populate(services);

            // Dependency Injection: Storing the container in a field so that other components can make use of it.
            // In many scenarios, this isn't necessary. builder.Build() can often be returned directly.
            _autofacContainer = builder.Build();

            // Dependency Injection: Return the DI container to be used by this web application.
            return new AutofacServiceProvider(_autofacContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseCors(options =>
              options.WithOrigins("http://localhost:8080")
              .AllowAnyMethod()
              .AllowAnyHeader()
          );
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
