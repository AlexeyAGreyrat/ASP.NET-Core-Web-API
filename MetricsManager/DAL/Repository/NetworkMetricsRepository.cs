using MetricManager.DAL.Interfaces;
using MetricManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Core;

namespace MetricManager.DAL.Repository
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly ISqlSettingsProvider _provider;

        public NetworkMetricsRepository(ISqlSettingsProvider provider)
        {
            _provider = provider;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(NetworkMetricModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("INSERT INTO networkmetrics(value, time, idagent) VALUES(@value, @time, @idagent)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    idagent = item.IdAgent
                });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("DELETE FROM networkmetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(NetworkMetricModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<NetworkMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            var q = connection
                .Query<NetworkMetricModel>($"SELECT id, time, value From networkmetrics")
                .ToList();
            return q;
        }

        public NetworkMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .QuerySingle<NetworkMetricModel>("SELECT Id, Time, Value FROM networkmetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }

        public IList<NetworkMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<NetworkMetricModel>(
                    $"SELECT id, time, value From networkmetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<NetworkMetricModel> GetMetricsFromTimeToTimeFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime, int idAgent)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<NetworkMetricModel>(
                    $"SELECT id, time, value From networkmetrics WHERE time > @fromTime AND time < @toTime AND idagent = {idAgent}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                    })
                .ToList();
        }

        public IList<NetworkMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<NetworkMetricModel>($"SELECT * FROM networkmetrics WHERE time > @fromTime AND time < @toTime ORDER BY {field}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<NetworkMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field, int idAgent)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<NetworkMetricModel>($"SELECT * FROM networkmetrics WHERE time > @fromTime AND time < @toTime AND idagent = {idAgent} ORDER BY {field}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                    })
                .ToList();
        }

        public DateTimeOffset LastTime()
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            var res = connection.QueryFirstOrDefault<NetworkMetricModel>("SELECT * FROM networkmetrics ORDER BY time DESC LIMIT 1");
            var result = res ?? new NetworkMetricModel();
            return result.Time;
        }
    }
}
