using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Order_API.Data;
using Order_API.Repository;
using Order_API.Repository.IRepository;
using AutoMapper;
using Order_API.MovMapper;
using System.Reflection;
using System.IO;
using System;

namespace Order_API
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
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddAutoMapper(typeof(CustomerMapper));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("CustomerOpenAPI", new OpenApiInfo { 
                    Title = "Customers_API", 
                    Version = "v1", 
                    Description = "Moving Masters Application"
                });
                c.SwaggerDoc("OrderOpenAPI", new OpenApiInfo
                {
                    Title = "Orders_API",
                    Version = "v1",
                    Description = "Moving Masters Application"
                });
                //This gives us xml comments file
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                c.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/CustomerOpenAPI/swagger.json", "Customer API");
                    c.SwaggerEndpoint("/swagger/OrderOpenAPI/swagger.json", "Order API");
                    c.RoutePrefix = "";
                    });

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
