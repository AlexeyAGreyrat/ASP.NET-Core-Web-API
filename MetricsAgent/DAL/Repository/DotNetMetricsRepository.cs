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
            connection.Execute("INSERT INTO dotnetmetrics (value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
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
            return connection
                .Query<DotNetMetricModel>($"SELECT id, time, value From dotnetmetrics")
                .ToList();
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

        public IList<DotNetMetricModel> GetMetricsFromTimeToTime(
            DateTimeOffset fromTime,
            DateTimeOffset toTime)
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

        public IList<DotNetMetricModel> GetMetricsFromTimeToTimeOrderBy(
            DateTimeOffset fromTime,
            DateTimeOffset toTime,
            string sortingField)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<DotNetMetricModel>($"SELECT * FROM dotnetmetrics WHERE time > @fromTime AND time < @toTime ORDER BY {sortingField}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }
    }
}
