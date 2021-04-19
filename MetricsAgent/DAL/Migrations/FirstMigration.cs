using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace MetricAgent.DAL.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("cpumetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();
            Create.Table("dotnetmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();
            Create.Table("hddmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("freesize").AsDouble()
                .WithColumn("time").AsInt64();
            Create.Table("rammetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("available").AsDouble()
                .WithColumn("time").AsInt64();
            Create.Table("networkmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
        }

        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("rammetrics");
            Delete.Table("networkmetrics");
        }
    }
}
