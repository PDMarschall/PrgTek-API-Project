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
            await SerializeQuotesForDownload();
            return View();
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

        private async Task SerializeQuotesForDownload()
        {
            List<Quote> tempList = _context.Quote.ToList();

            using FileStream createStream = System.IO.File.Create("Quote_List.json");
            await JsonSerializer.SerializeAsync(createStream, tempList);
            await createStream.DisposeAsync();

            FileInfo attributes = new FileInfo("Quote_List.json");
            var fileLength = attributes.Length;

            Response.Clear();
            Response.ContentType = "application/json";
            Response.ContentLength = fileLength;            
            Response.Headers.Add("content-disposition", "attachment; filename=Quotes_List.json");

            await Response.SendFileAsync("Quote_List.json");
            await Response.CompleteAsync();            
        }
    }
}