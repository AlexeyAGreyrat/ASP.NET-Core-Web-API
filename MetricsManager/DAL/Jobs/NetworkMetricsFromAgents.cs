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
    public class NetworkMetricsFromAgents : IJob
    {
        private readonly INetworkMetricsRepository _repositoryNetwork;
        private readonly IAgentsRepository _repositoryAgent;
        private readonly IMetricsManagerClient _client;

        public NetworkMetricsFromAgents(INetworkMetricsRepository repositoryNetwork, IAgentsRepository repositoryAgent, IMetricsManagerClient client)
        {
            _repositoryNetwork = repositoryNetwork;
            _repositoryAgent = repositoryAgent;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            DateTimeOffset toTime = DateTimeOffset.UtcNow;
            DateTimeOffset fromTime = _repositoryNetwork.LastTime();
            IList<AgentModel> agents = _repositoryAgent.GetAll();


            foreach (var agent in agents)
            {
                if (agent.Status == true)
                {
                    AllNetworkMetricsApiResponse allNetworkMetrics = _client.GetAllNetworkMetrics(new GetAllNetworkMetricsApiRequest
                    {
                        FromTime = fromTime,
                        ToTime = toTime,
                        Addres = agent.Ipaddress
                    });

                    if (allNetworkMetrics != null)
                    {
                        foreach (var metric in allNetworkMetrics.Metrics)
                        {
                            _repositoryNetwork.Create(new NetworkMetricModel
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
