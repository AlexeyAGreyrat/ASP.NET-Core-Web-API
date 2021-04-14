using AutoMapper;
using MetricAgent.Metric;
using MetricAgent.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent.DAL.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<CpuMetric, CpuMetricDto>();
			CreateMap<DotNetMetric, DotNetMetricDto>();
			CreateMap<HddMetric, HddMetricDto>();
			CreateMap<NetworkMetric, NetworkMetricDto>();
			CreateMap<RamMetric, RamMetricDto>();
		}
	}
}
