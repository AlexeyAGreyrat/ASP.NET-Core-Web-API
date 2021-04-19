using AutoMapper;
using MetricManager.DAL.Models;
using MetricManager.DAL.Responses;

namespace MetricManager.DAL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgentModel, AgentManagerDto>();
            CreateMap<CpuMetricModel, CpuMetricManagerDto>();
            CreateMap<DotNetMetricModel, DotNetMetricManagerDto>();
            CreateMap<NetworkMetricModel,NetworkMetricManagerDto>();
            CreateMap<HddMetricModel, HddMetricManagerDto>();
            CreateMap<RamMetricModel, RamMetricManagerDto>();
        }
    }
}
