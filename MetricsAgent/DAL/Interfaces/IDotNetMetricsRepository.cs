using Core;
using MetricAgent.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricAgent.DAL.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetricModel>
    {
        IList<DotNetMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
