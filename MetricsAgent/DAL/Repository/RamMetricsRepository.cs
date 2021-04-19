using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core;
using Dapper;
using MetricAgent.DAL.Interfaces;
using MetricAgent.DAL.Models;

namespace MetricAgent.DAL.Repository
{ 
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly ISqlSettingsProvider _provider;

        public RamMetricsRepository(ISqlSettingsProvider provider)
        {
            _provider = provider;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(RamMetricModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("INSERT INTO rammetrics (available, time) VALUES(@available, @time)",
                new
                {
                    available = item.Available,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("DELETE FROM rammetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(RamMetricModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("UPDATE rammetrics SET available = @available, time = @time WHERE id = @id",
                new
                {
                    available = item.Available,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<RamMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<RamMetricModel>($"SELECT id, time, available From rammetrics")
                .ToList();
        }

        public RamMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .QuerySingle<RamMetricModel>("SELECT id, time, available FROM rammetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }

        public IList<RamMetricModel> GetMetricsFromTimeToTime(
            DateTimeOffset fromTime,
            DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<RamMetricModel>(
                    $"SELECT * From rammetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }
    }
}
