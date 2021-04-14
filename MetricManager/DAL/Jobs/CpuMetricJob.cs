using Core.Interfaces;
using MetricManager.DAL.DTO;
using MetricManager.DAL.Metrics;
using MetricManager.DAL.Client;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MetricManager.DAL.Request;

namespace MetricManager.DAL.Jobs
{
    public class CpuMetricJob : IJob
    {
        private IRepositoryGet<CpuMetric> _metricRepository;
        private IAgentsRepository<AgentInfo> _agentRepository;
        private IMetricsAgentClient _client;
        private DateTimeOffset _UNIX = new DateTime(2000, 01, 01);
        public CpuMetricJob(IRepositoryGet<CpuMetric> metricRepository, IAgentsRepository<AgentInfo> agentsRepository, IMetricsAgentClient client)
        {
            _metricRepository = metricRepository;
            _agentRepository = agentsRepository;
            _client = client;
        }
        public Task Execute(IJobExecutionContext context)
        {
            IList<AgentInfo> agents = _agentRepository.GetAll();
            DateTimeOffset last;
            if (_metricRepository.GetLast() == null)
            {
                last = _UNIX;
            }
            else
            {
                last = _UNIX.AddSeconds(_metricRepository.GetLast().Time.TotalSeconds);
            }

            for (int i = 0; i < agents.Count; i++)
            {
                var temp = new GetAllCpuMetricsApiRequest();

                temp.ClientBaseAddress = agents[i].AgentAddress.ToString();
                if (last > _UNIX)
                {
                    temp.FromTime = last;
                }
                else
                {
                    temp.FromTime = _UNIX;
                }
                temp.ToTime = DateTimeOffset.UtcNow;

                var result = _client.GetAllCpuMetrics(temp);

                for (int j = 0; j < result.Metrics.Count; j++)
                {
                    _metricRepository.Create(new CpuMetric
                    {
                        AgentId = result.Metrics[i].AgentId,
                        Value = result.Metrics[i].Value,
                        Time = TimeSpan.FromSeconds(result.Metrics[i].Time.ToUnixTimeSeconds())
                    });
                }
            }

            return Task.CompletedTask;
        }
    }
}
