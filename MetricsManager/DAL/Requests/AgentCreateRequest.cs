using System;

namespace MetricManager.DAL.Requests
{
    public class AgentCreateRequest
    {
        public bool Status { get; set; }
        public string Ipaddress { get; set; }
        public string Name { get; set; }
    }
}
