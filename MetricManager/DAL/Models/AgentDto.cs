using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricManager.DAL.Models
{
    public class AgentDto
    {
        public int agentId { get; set; }
        public Uri AgentAddress { get; set; }
    }
}
