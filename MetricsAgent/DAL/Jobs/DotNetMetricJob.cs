using MetricAgent.DAL.Interfaces;
using MetricAgent.DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace MetricAgent.DAL.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IDotNetMetricsRepository _repository;
        private readonly PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IDotNetMetricsRepository>();
            _dotnetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            int bytesInHeaps = Convert.ToInt32(_dotnetCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new DotNetMetricModel()
            {
                Time = time,
                Value = bytesInHeaps
            });
            return Task.CompletedTask;
        }
    }
}
