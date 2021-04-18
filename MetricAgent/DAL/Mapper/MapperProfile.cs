using AutoMapper;
using MetricAgent.DAL.Metric;
using MetricAgent.DAL.Models;
using System;

namespace MetricAgent.DAL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>()
                .ForMember(dto => dto.Time, opt => opt.MapFrom(c => DateTimeOffset.FromUnixTimeSeconds((long)c.Time.TotalSeconds)));
            CreateMap<DotNetMetric, DotNetMetricDto>()
                .ForMember(dto => dto.Time, opt => opt.MapFrom(c => DateTimeOffset.FromUnixTimeSeconds((long)c.Time.TotalSeconds)));
            CreateMap<NetworkMetric, NetworkMetricDto>()
                .ForMember(dto => dto.Time, opt => opt.MapFrom(c => DateTimeOffset.FromUnixTimeSeconds((long)c.Time.TotalSeconds)));
            CreateMap<HddMetric, HddMetricDto>()
                .ForMember(dto => dto.Time, opt => opt.MapFrom(c => DateTimeOffset.FromUnixTimeSeconds((long)c.Time.TotalSeconds)));
            CreateMap<RamMetric, RamMetricDto>()
                .ForMember(dto => dto.Time, opt => opt.MapFrom(c => DateTimeOffset.FromUnixTimeSeconds((long)c.Time.TotalSeconds)));
        }
    }
}
