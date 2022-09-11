using PTAP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PTAP.Infrastructure
{
    public class KanyeClient
    {
        private readonly HttpClient _httpClient;
        public Quote Quote { get; set; }

        public KanyeClient(HttpClient http)
        {
            _httpClient = http;
            ConfigureClient();
        }

        public void RequestQuote()
        {
            Run().GetAwaiter().GetResult();
        }

        private void ConfigureClient()
        {
            _httpClient.BaseAddress = new Uri("https://api.kanye.rest/");
        }

        private async Task Run()
        {
            if (_httpClient.BaseAddress != null)
            {
            Quote quote = await GetQuoteAsync(_httpClient.BaseAddress.ToString());
            Quote = quote;
            }
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


    }
}
