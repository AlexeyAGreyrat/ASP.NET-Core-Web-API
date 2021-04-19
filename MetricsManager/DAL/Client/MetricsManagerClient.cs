using MetricManager.DAL.Client.ApiRequests;
using MetricManager.DAL.Client.ApiResponses;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace MetricManager.DAL.Client
{
    public class MetricsManagerClient : IMetricsManagerClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsManagerClient> _logger;

        public MetricsManagerClient(HttpClient httpClient, ILogger<MetricsManagerClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.Addres}/api/metrics/cpu/from/{request.FromTime:o}/to/{request.ToTime:o}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var content = streamReader.ReadToEnd();
                var result = JsonSerializer.Deserialize<AllCpuMetricsApiResponse>(content, new JsonSerializerOptions()
                    { PropertyNameCaseInsensitive = true });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.Addres}/api/metrics/dotnet/from/{request.FromTime:o}/to/{request.ToTime:o}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var content = streamReader.ReadToEnd();
                var result = JsonSerializer.Deserialize<AllDotNetMetricsApiResponse>(content, new JsonSerializerOptions()
                    { PropertyNameCaseInsensitive = true });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.Addres}/api/metrics/hdd/from/{request.FromTime:o}/to/{request.ToTime:o}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var content = streamReader.ReadToEnd();
                var result = JsonSerializer.Deserialize<AllHddMetricsApiResponse>(content, new JsonSerializerOptions()
                    { PropertyNameCaseInsensitive = true });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.Addres}/api/metrics/network/from/{request.FromTime:o}/to/{request.ToTime:o}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var content = streamReader.ReadToEnd();
                var result = JsonSerializer.Deserialize<AllNetworkMetricsApiResponse>(content, new JsonSerializerOptions()
                    { PropertyNameCaseInsensitive = true });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.Addres}/api/metrics/ram/from/{request.FromTime:o}/to/{request.ToTime:o}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var content = streamReader.ReadToEnd();
                var result = JsonSerializer.Deserialize<AllRamMetricsApiResponse>(content, new JsonSerializerOptions()
                    { PropertyNameCaseInsensitive = true });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}


