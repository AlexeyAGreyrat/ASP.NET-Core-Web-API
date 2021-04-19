using AutoMapper;
using Core;
using FluentMigrator.Runner;
using MetricManager.DAL.Client;
using MetricManager.DAL.Interfaces;
using MetricManager.DAL.Repository;
using MetricManager.DAL.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.IO;
using System.Reflection;
using MetricManager.DAL.Mapper;

namespace MetricManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient<IMetricsManagerClient, MetricsManagerClient>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ =>
                TimeSpan.FromMilliseconds(1000)));
            
            services.AddControllers();

            services.AddSingleton<ISqlSettingsProvider, SqlSettingsProvider>();
            services.AddSingleton<IAgentsRepository, AgentsRepository>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(new SqlSettingsProvider().GetConnectionString())
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<CpuMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricsFromAgents),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<DotNetMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotNetMetricsFromAgents),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<NetworkMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricsFromAgents),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<HddMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricsFromAgents),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<RamMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricsFromAgents),
                cronExpression: "0/5 * * * * ?"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API сервис менеджера сбора метрик",
                    Contact = new OpenApiContact
                    {
                        Name = "Alexy",
                        Email = "n"
                    }
                });
                var xmlFile =
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API сервис менеджера сбора метрик");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
