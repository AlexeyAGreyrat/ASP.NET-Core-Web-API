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
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly ISqlSettingsProvider _provider;

        public DotNetMetricsRepository(ISqlSettingsProvider provider)
        {
            _provider = provider;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(DotNetMetricModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("INSERT INTO dotnetmetrics(value, time, idagent) VALUES(@value, @time, @idagent)",
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
            connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(DotNetMetricModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<DotNetMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            var q = connection
                .Query<DotNetMetricModel>($"SELECT id, time, value From dotnetmetrics")
                .ToList();
            return q;
        }

        public DotNetMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .QuerySingle<DotNetMetricModel>("SELECT Id, Time, Value FROM dotnetmetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }

        public IList<DotNetMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<DotNetMetricModel>(
                    $"SELECT id, time, value From dotnetmetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<DotNetMetricModel> GetMetricsFromTimeToTimeFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime, int idAgent)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<DotNetMetricModel>(
                    $"SELECT id, time, value From dotnetmetrics WHERE time > @fromTime AND time < @toTime AND idagent = @IdAgent",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                        IdAgent = idAgent
                    })
                .ToList();
        }

        public IList<DotNetMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<DotNetMetricModel>($"SELECT * FROM dotnetmetrics WHERE time > @fromTime AND time < @toTime ORDER BY {field}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<DotNetMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string field, int idAgent)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<DotNetMetricModel>($"SELECT * FROM dotnetmetrics WHERE time > @fromTime AND time < @toTime AND idagent = @IdAgent ORDER BY {field}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                        IdAgent = idAgent
                    })
                .ToList();
        }

        public DateTimeOffset LastTime()
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            var res = connection.QueryFirstOrDefault<DotNetMetricModel>("SELECT * FROM dotnetmetrics ORDER BY time DESC LIMIT 1");
            var result = res ?? new DotNetMetricModel();
            return result.Time;
        }
    }
}
