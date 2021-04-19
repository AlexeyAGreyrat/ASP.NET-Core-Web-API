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
    public class HddMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IHddMetricsRepository _repository;
        private readonly PerformanceCounter _hddCounter;

        public HddMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IHddMetricsRepository>();
            _hddCounter = new PerformanceCounter("LogicalDisk", "Free Megabytes", "C:");
        }

        public Task Execute(IJobExecutionContext context)
        {
            double hddFreeSpace = _hddCounter.NextValue()/1024/1024;
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new HddMetricModel()
            {
                Time = time,
                FreeSize = hddFreeSpace
            });
            return Task.CompletedTask;
        }
    }
}
