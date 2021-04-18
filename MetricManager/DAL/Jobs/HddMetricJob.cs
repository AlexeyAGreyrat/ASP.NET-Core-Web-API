using Core.Interfaces;
using MetricManager.DAL.Client;
using MetricManager.DAL.DTO;
using MetricManager.DAL.Metrics;
using MetricManager.DAL.Request;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MetricManager.DAL.Jobs
{
    [DisallowConcurrentExecution]
    public class HddMetricJob : IJob
    {
        private IRepositoryGet<HddMetric> _metricRepository;
        private IAgentsRepository<AgentInfo> _agentRepository;
        private IMetricsAgentClient _client;
        public HddMetricJob(IRepositoryGet<HddMetric> metricRepository, IAgentsRepository<AgentInfo> agentsRepository, IMetricsAgentClient client)
        {
            _metricRepository = metricRepository;
            _agentRepository = agentsRepository;
            _client = client;
        }
        public Task Execute(IJobExecutionContext context)
        {
            IList<AgentInfo> agents = _agentRepository.GetAll();

            List<DateTimeOffset> last = new List<DateTimeOffset>();

            for (int i = 0; i < agents.Count; i++)
            {
                if (_metricRepository.GetLastFromAgent(agents[i].AgentId) == null)
                {
                    last.Add(DateTimeOffset.UnixEpoch);
                }
                else
                {
                    last.Add(DateTimeOffset.FromUnixTimeSeconds((long)_metricRepository.GetLastFromAgent(agents[i].AgentId).Time.TotalSeconds));
                }
            }

            for (int i = 0; i < agents.Count; i++)
            {
                var request = new GetAllHddMetricsApiRequest();

                request.ClientBaseAddress = agents[i].AgentURL;
                if (last[i] > DateTimeOffset.UnixEpoch)
                {
                    request.FromTime = last[i];
                }
                else
                {
                    request.FromTime = DateTimeOffset.UnixEpoch;
                }
                request.ToTime = DateTimeOffset.UtcNow;


                var result = _client.GetAllHddMetrics(request);

                for (int j = 0; j < result.Metrics.Count; j++)
                {
                    _metricRepository.Create(new HddMetric
                    {
                        AgentId = agents[i].AgentId,
                        Value = result.Metrics[j].Value,
                        Time = TimeSpan.FromSeconds(result.Metrics[j].Time.ToUnixTimeSeconds())
                    });
                }
            }

            return Task.CompletedTask;
        }
    }
}
