using MetricManager.DAL.Client;
using MetricManager.DAL.Client.ApiRequests;
using MetricManager.DAL.Client.ApiResponses;
using MetricManager.DAL.Interfaces;
using MetricManager.DAL.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MetricManager.DAL.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricsFromAgents : IJob
    {
        private readonly IRamMetricsRepository _repositoryRam;
        private readonly IAgentsRepository _repositoryAgent;
        private readonly IMetricsManagerClient _client;

        public RamMetricsFromAgents(IRamMetricsRepository repositoryRam, IAgentsRepository repositoryAgent, IMetricsManagerClient client)
        {
            _repositoryRam = repositoryRam;
            _repositoryAgent = repositoryAgent;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            DateTimeOffset toTime = DateTimeOffset.UtcNow;
            DateTimeOffset fromTime = _repositoryRam.LastTime();
            IList<AgentModel> agents = _repositoryAgent.GetAll();


            foreach (var agent in agents)
            {
                if (agent.Status == true)
                {
                    AllRamMetricsApiResponse allRamMetrics = _client.GetAllRamMetrics(new GetAllRamMetricsApiRequest
                    {
                        FromTime = fromTime,
                        ToTime = toTime,
                        Addres = agent.Ipaddress
                    });

                    if (allRamMetrics != null)
                    {
                        foreach (var metric in allRamMetrics.Metrics)
                        {
                            _repositoryRam.Create(new RamMetricModel
                            {
                                IdAgent = agent.Id,
                                Time = metric.Time,
                                Value = metric.Value
                            });
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
