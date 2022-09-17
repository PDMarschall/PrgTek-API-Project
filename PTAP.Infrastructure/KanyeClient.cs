using PTAP.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PTAP.Infrastructure
{
    public class KanyeClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAddress = "https://localhost:7219/KanyeAPI";

        public bool IsConfigured { get; set; }
        public KanyeImage Image { get; set; }
        public Quote Quote { get; set; }

        public KanyeClient(HttpClient http)
        {
            _httpClient = http;
            ConfigureClient();
        }

        private void ConfigureClient()
        {
            _httpClient.BaseAddress = new Uri("https://api.kanye.rest/");
            IsConfigured = true;
        }

        private async Task GetWisdom()
        {
            if (_httpClient.BaseAddress != null)
            {
                Quote = await GetQuoteAsync(_httpClient.BaseAddress.ToString());
                Image = await GetImageAsync(_apiAddress);
            }
        }

        private async Task<KanyeImage> GetImageAsync(string path)
        {
            KanyeImage image = null;
            HttpResponseMessage response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                image = new KanyeImage(await _httpClient.GetByteArrayAsync(_apiAddress));
            }
            return image;
        }

        private async Task<Quote> GetQuoteAsync(string path)
        {
            Quote quote = null;
            HttpResponseMessage response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                quote = await response.Content.ReadFromJsonAsync<Quote>();
            }
            return quote;
        }


        public async Task CreateAsync()
        {
            await GetWisdom();
        }
    }
}
