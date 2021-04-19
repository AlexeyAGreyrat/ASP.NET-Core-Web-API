using Core;
using Dapper;
using MetricAgent.DAL.Interfaces;
using MetricAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricAgent.DAL.Repository
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
            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
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
            return connection
                .Query<NetworkMetricModel>($"SELECT id, time, value From networkmetrics")
                .ToList();
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

        public IList<NetworkMetricModel> GetMetricsFromTimeToTime(
            DateTimeOffset fromTime,
            DateTimeOffset toTime)
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
    }
}
