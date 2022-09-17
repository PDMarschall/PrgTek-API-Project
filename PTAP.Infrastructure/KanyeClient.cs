using PTAP.Core.Interfaces;
using PTAP.Core.Models;
using System.Net.Http.Json;

namespace PTAP.Infrastructure
{
    public class KanyeClientDefault : IKanyeClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _imageApiAddress = "https://localhost:7219/KanyeAPI";
        private readonly string _quoteApiAddress = "https://api.kanye.rest/";

        public KanyeImage Image { get; set; }
        public KanyeQuote Quote { get; set; }

        public KanyeClientDefault(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task GetWisdom()
        {
            Quote = await GetQuoteAsync(_quoteApiAddress);
            Image = await GetImageAsync(_imageApiAddress);
        }

        private async Task<KanyeImage> GetImageAsync(string path)
        {
            KanyeImage image = null;
            HttpResponseMessage response = await _httpClient.GetAsync(path);
            if (IsValidResponse(response))
            {
                image = new KanyeImage(await _httpClient.GetByteArrayAsync(_imageApiAddress));
            }
            return image;
        }

        private async Task<KanyeQuote> GetQuoteAsync(string path)
        {
            KanyeQuote quote = null;
            HttpResponseMessage response = await _httpClient.GetAsync(path);
            if (IsValidResponse(response))
            {
                quote = await response.Content.ReadFromJsonAsync<KanyeQuote>();
            }
            return quote;
        }

        private bool IsValidResponse(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode &&
                response.StatusCode != System.Net.HttpStatusCode.NoContent;
        }
    }
}
