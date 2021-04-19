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

    public class AgentsRepository : IAgentsRepository
    {
        private readonly ISqlSettingsProvider _provider;

        public AgentsRepository(ISqlSettingsProvider provider)
        {
            _provider = provider;
        }

        public void Create(AgentModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute($"INSERT INTO agents(status, ipaddress, name) VALUES(@status, @ipaddress, @name)",
                new
                {
                    status = Convert.ToInt32(item.Status),
                    ipaddress = item.Ipaddress,
                    name = item.Name
                });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute($"DELETE FROM agents WHERE id = @id",
                new
                {
                    id = target
                });
        }

        public void Update(AgentModel item)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            connection.Execute($"UPDATE agents SET status = @status, ipaddress = @ipaddress, name = @name WHERE id = @id",
                new
                {
                    status = Convert.ToInt32(item.Status),
                    ipaddress = item.Ipaddress,
                    name = item.Name,
                    id = item.Id
                });
        }

        public IList<AgentModel> GetAll()
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            var q = connection
                .Query<AgentModel>($"SELECT * From agents")
                .ToList();
            return q;
        }

        public AgentModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_provider.GetConnectionString());
            return connection
                .QuerySingle<AgentModel>("SELECT * FROM agents WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }
    }
}
