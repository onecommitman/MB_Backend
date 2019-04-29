using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using ModulSchool.BusinessLogic;
using ModulSchool.Services.Interfaces;
using ModulSchool.Services;
using ModulSchool.Consumers;
using ModulSchool.Commands;
using MassTransit;

namespace ModulSchool
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<GetUsersInfoRequestHandler>();
            services.AddScoped<AppendUsersRequestHandler>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            // Обработчики событий MassTransit
            services.AddScoped<AppendUserConsumer>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AppendUserConsumer>();
                x.AddBus(provider => MassTransit.Bus.Factory.CreateUsingInMemory(cfg =>
                {
                    cfg.ReceiveEndpoint("append-user-queue", ep =>
                    {
                        ep.ConfigureConsumer<AppendUserConsumer>(provider);
                        EndpointConvention.Map<AppendUserCommand>(ep.InputAddress);
                    });
                }));

                x.AddRequestClient<AppendUserCommand>();
            });

            services.AddSingleton<IHostedService, BusService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
