using Core;
using MetricAgent.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricAgent.DAL.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetricModel>
    {
        IList<NetworkMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
