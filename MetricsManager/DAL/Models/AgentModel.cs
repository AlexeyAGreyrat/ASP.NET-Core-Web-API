using System;

namespace MetricManager.DAL.Models
{
    public class AgentModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Ipaddress { get; set; }
        public string Name { get; set; }
    }
}
