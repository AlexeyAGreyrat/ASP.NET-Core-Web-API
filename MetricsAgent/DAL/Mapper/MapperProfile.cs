using AutoMapper;
using MetricAgent.DAL.Models;
using MetricAgent.DAL.Responses;

namespace MetricAgent.DAL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricModel, CpuMetricDto>();
            CreateMap<DotNetMetricModel, DotNetMetricDto>();
            CreateMap<NetworkMetricModel,NetworkMetricDto>();
            CreateMap<HddMetricModel, HddMetricDto>();
            CreateMap<RamMetricModel, RamMetricDto>();
        }
    }
}
