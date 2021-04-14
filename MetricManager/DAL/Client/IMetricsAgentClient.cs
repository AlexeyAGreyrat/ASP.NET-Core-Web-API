using MetricManager.DAL.Request;
using MetricManager.DAL.Responses;

namespace MetricManager.DAL.Client
{
    public interface IMetricsAgentClient
    {
        AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

        AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

        DotNetMetricsApiResponse GetAllDotNetMetrics(GetDotNetHeapMetrisApiRequest request);

        AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);

        AllNetworkMetricsApiResponce GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
    }
}
