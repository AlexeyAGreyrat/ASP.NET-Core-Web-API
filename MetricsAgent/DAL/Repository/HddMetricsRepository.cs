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
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly ISqlSettingsProvider _provider;

        public HddMetricsRepository(ISqlSettingsProvider provider)
        {
            _provider = provider;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(HddMetricModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("INSERT INTO hddmetrics (freesize, time) VALUES(@freesize, @time)",
                new
                {
                    freesize = item.FreeSize,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("DELETE FROM hddmetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(HddMetricModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute("UPDATE hddmetrics SET freesize = @freesize, time = @time WHERE id = @id",
                new
                {
                    freesize = item.FreeSize,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<HddMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<HddMetricModel>($"SELECT id, time, freesize From hddmetrics")
                .ToList();
        }

        public HddMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .QuerySingle<HddMetricModel>("SELECT id, time, freesize FROM hddmetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }

        public IList<HddMetricModel> GetMetricsFromTimeToTime(
            DateTimeOffset fromTime,
            DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .Query<HddMetricModel>(
                    $"SELECT * From hddmetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }
    }
}
