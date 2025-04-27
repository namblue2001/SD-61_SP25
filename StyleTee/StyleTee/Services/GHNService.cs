using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StyleTee.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Linq;
using System.Text;

namespace StyleTee.Services
{
    public class GHNService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;
        private readonly ILogger<GHNService> _logger;

        public GHNService(HttpClient httpClient, IConfiguration configuration, ILogger<GHNService> logger)
        {
            _httpClient = httpClient;
            _token = configuration["GHN:Token"] ?? throw new ArgumentNullException("GHN:Token is not configured");
            _logger = logger;

            // Configure HttpClient
            _httpClient.BaseAddress = new Uri("https://online-gateway.ghn.vn");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Token", _token);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<int?> GetDistrictIdByName(string districtName)
        {
            try
            {
                var response = await _httpClient.GetAsync("/shipping/public-api/master-data/district");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"API Error: {errorContent}");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GHNResponse>(content);

                if (result?.Data != null)
                {
                    var normalizedDistrictName = districtName.ToLower()
                        .Replace(" ", "")
                        .Replace("-", "")
                        .Replace("_", "");

                    var district = result.Data.FirstOrDefault(d => 
                        d.DistrictName.ToLower()
                            .Replace(" ", "")
                            .Replace("-", "")
                            .Replace("_", "")
                            .Contains(normalizedDistrictName) ||
                        normalizedDistrictName.Contains(d.DistrictName.ToLower()
                            .Replace(" ", "")
                            .Replace("-", "")
                            .Replace("_", "")));
                    
                    if (district != null)
                    {
                        return district.DistrictID;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy district_id cho huyện: {districtName}");
                return null;
            }
        }

        public async Task<List<Province>> GetProvinces()
        {
            try
            {
                var response = await _httpClient.GetAsync("/shiip/public-api/master-data/province");
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API Error: {content}");
                    return new List<Province>();
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<GHNProvinceResponse>(content, options);

                if (result?.Data != null && result.Data.Count > 0)
                {
                    return result.Data;
                }
                else
                {
                    _logger.LogWarning($"Không có dữ liệu tỉnh/thành phố từ API. Response code: {result?.Code}, Message: {result?.Message}");
                    return new List<Province>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách tỉnh/thành phố");
                return new List<Province>();
            }
        }

        public async Task<List<District>> GetDistrictsByProvinceId(int provinceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/shiip/public-api/master-data/district?province_id={provinceId}");
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API Error: {content}");
                    return new List<District>();
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<GHNResponse>(content, options);

                if (result?.Data != null && result.Data.Count > 0)
                {
                    return result.Data;
                }
                else
                {
                    _logger.LogWarning($"Không có dữ liệu quận/huyện từ API. Response code: {result?.Code}, Message: {result?.Message}");
                    return new List<District>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy danh sách quận/huyện cho tỉnh/thành phố ID: {provinceId}");
                return new List<District>();
            }
        }

        public async Task<decimal?> CalculateShippingFee(int districtId)
        {
            try
            {
                var requestData = new
                {
                    service_type_id = 2,
                    to_district_id = districtId,
                    weight = 1500
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(requestData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync("/shiip/public-api/v2/shipping-order/fee", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API Error: {responseContent}");
                    return null;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<GHNFeeResponse>(responseContent, options);

                if (result?.Data != null)
                {
                    return result.Data.Total;
                }
                else
                {
                    _logger.LogWarning($"Không có dữ liệu phí vận chuyển. Response code: {result?.Code}, Message: {result?.Message}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi tính phí vận chuyển cho district_id: {districtId}");
                return null;
            }
        }
    }

    public class GHNResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<District> Data { get; set; }
    }

    public class District
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
    }

    public class GHNProvinceResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<Province> Data { get; set; }
    }

    public class Province
    {
        [JsonPropertyName("ProvinceID")]
        public int ProvinceID { get; set; }

        [JsonPropertyName("ProvinceName")]
        public string ProvinceName { get; set; }

        [JsonPropertyName("CountryID")]
        public int CountryID { get; set; }

        [JsonPropertyName("Code")]
        public string Code { get; set; }

        [JsonPropertyName("NameExtension")]
        public List<string> NameExtension { get; set; }

        [JsonPropertyName("IsEnable")]
        public int IsEnable { get; set; }

        [JsonPropertyName("RegionID")]
        public int RegionID { get; set; }

        [JsonPropertyName("RegionCPN")]
        public int RegionCPN { get; set; }

        [JsonPropertyName("UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [JsonPropertyName("CreatedAt")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("UpdatedAt")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("Status")]
        public int Status { get; set; }

        [JsonPropertyName("CanUpdateCOD")]
        public bool CanUpdateCOD { get; set; }
    }

    public class GHNFeeResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public GHNFeeData Data { get; set; }
    }

    public class GHNFeeData
    {
        public decimal Total { get; set; }
    }
} 