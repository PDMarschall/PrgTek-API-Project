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
        public List<Quote> Quotes { get; set; }

        public KanyeClient(HttpClient http)
        {
            Quotes = new List<Quote>();

            _httpClient = http;
            ConfigureClient();
            Run().GetAwaiter().GetResult();
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
            Quote quote = await GetQuoteAsync(_httpClient.BaseAddress.ToString());
            Quotes.Add(quote);
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
