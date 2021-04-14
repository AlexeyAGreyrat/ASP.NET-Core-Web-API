using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using Core.Interfaces;
using MetricManager.DAL.Metrics;

namespace MetricManager.DAL.Repository
{
    public class HddMetricsRepository : IRepositoryGet<HddMetric>
    {
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO hddmetrics (value, time) VALUES (@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        agentid = item.AgentId
                    });
            }
        }      
        public IList<HddMetric> GetInTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE time >= @fromtime AND time <= @totime",
                    new
                    {
                        fromtime = fromTime.ToUnixTimeSeconds(),
                        totime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }

        public IList<HddMetric> GetFromToByAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE time >= @fromtime AND time <= @totime AND agentid = @agentId",
                    new
                    {
                        agentid = agentId,
                        fromtime = fromTime.ToUnixTimeSeconds(),
                        totime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }
        public HddMetric GetLast()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    return connection.QuerySingle<HddMetric>("SELECT * FROM cpumetrics ORDER BY id DESC LIMIT 1");

                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
