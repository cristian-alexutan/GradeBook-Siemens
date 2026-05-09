using System.Text.Json;
using System.Text.Json.Serialization;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.ApiClients
{
    public class GradeApiClient : IGradeApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public GradeApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _url = configuration["GradeApi:Url"] ?? throw new InvalidOperationException("GradeApi:Url is not configured.");
        }

        public async Task<IEnumerable<Grade>> FetchAllAsync()
        {
            var response = await _httpClient.GetStringAsync(_url);
            var wrapper = JsonSerializer.Deserialize<GradeListWrapper>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return wrapper?.Items ?? Enumerable.Empty<Grade>();
        }

        private class GradeListWrapper
        {
            [JsonPropertyName("items")]
            public List<Grade> Items { get; set; } = new();
        }
    }
}