using FluentMigrator;
using MetricManager.DAL.Interfaces;

namespace MetricManager.DAL.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("agents")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("status").AsInt32()
                .WithColumn("ipaddress").AsString()
                .WithColumn("name").AsString();

            Create.Table("cpumetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64()
                .WithColumn("idagent").AsInt32()
                .ForeignKey("idagent", "agents", "id");

            Create.Table("dotnetmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64()
                .WithColumn("idagent").AsInt32()
                .ForeignKey("idagent", "agents", "id");

            Create.Table("networkmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64()
                .WithColumn("idagent").AsInt32()
                .ForeignKey("idagent", "agents", "id");

            Create.Table("rammetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsDouble()
                .WithColumn("time").AsInt64()
                .WithColumn("idagent").AsInt32()
                .ForeignKey("idagent", "agents", "id");

            Create.Table("hddmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsDouble()
                .WithColumn("time").AsInt64()
                .WithColumn("idagent").AsInt32()
                .ForeignKey("idagent", "agents", "id");
        }

        public override void Down()
        {
            Delete.Table("agents");
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("rammetrics");
            Delete.Table("networkmetrics");
        }
    }
}
