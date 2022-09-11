using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NuGet.Protocol;
using PTAP.Core.Models;
using PTAP.Infrastructure;
using PTAP.Infrastructure.Data;

namespace PTAP.Web.Controllers
{
    public class KanyeController : Controller
    {
        private readonly KanyeContext _context;
        private readonly KanyeClient _kanyeClient;

        public KanyeController(KanyeContext context, KanyeClient kanye)
        {
            _context = context;
            _kanyeClient = kanye;
        }

        public async Task<IActionResult> Index()
        {
            await GetAndDisplayQuote();
            return View();
        }

        public async Task<IActionResult> DownloadList()
        {
            string path = GetPath();

            MemoryStream fileContents = await SerializeQuotesForDownloadAsync(path);           

            return File(fileContents, "application/json", Path.GetFileName(path));
        }

        private async Task GetAndDisplayQuote()
        {
            if (_kanyeClient.Quote != null)
            {
                ViewBag.CurrentQuote = _kanyeClient.Quote.QuoteText;
                _context.Add(_kanyeClient.Quote);
                await _context.SaveChangesAsync();
            }
            else
            {
                ViewBag.CurrentQuote = "Quote could not be retrieved from source.";
            }
        }

        private async Task<MemoryStream> SerializeQuotesForDownloadAsync(string path)
        {
            
            List<Quote> tempList = _context.Quote.ToList();

            using FileStream createStream = System.IO.File.Create(path);
            await JsonSerializer.SerializeAsync(createStream, tempList);
            await createStream.DisposeAsync();

            MemoryStream memory = new MemoryStream();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return memory;
        }

        private string GetPath()
        {
            return Path.Combine(Environment.CurrentDirectory, @"wwwroot/files/", "Quotes_List.json");
        }
    }
}