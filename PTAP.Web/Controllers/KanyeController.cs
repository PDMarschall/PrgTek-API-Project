﻿using System;
using System.Collections.Generic;
using System.IO;
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
using PTAP.Web.Models;

namespace PTAP.Web.Controllers
{
    public class KanyeController : Controller
    {
        private readonly KanyeContext _context;
        private readonly KanyeClient _kanyeClient;
        private readonly string _quoteListPath;

        public KanyeController(KanyeContext context, KanyeClient kanye)
        {
            _context = context;
            _kanyeClient = kanye;
            _quoteListPath = GetQuoteListPath();
        }

        public async Task<IActionResult> Index()
        {
            KanyeWisdomViewModel viewModel = await GetKanyeViewModel();
            return View(viewModel);
        }

        public async Task<IActionResult> DownloadList()
        {
            MemoryStream fileContents = await SerializeQuotesJsonAsync(_quoteListPath);

            return File(fileContents, "application/json", Path.GetFileName(_quoteListPath));
        }

        public async Task<IActionResult> History()
        {
            return View(await _context.Quote.ToListAsync());
        }


        private async Task<KanyeWisdomViewModel> GetKanyeViewModel()
        {
            await _kanyeClient.GetWisdom();
            return new KanyeWisdomViewModel(_kanyeClient.Quote, _kanyeClient.Image);
        }


        private async Task<MemoryStream> SerializeQuotesJsonAsync(string path)
        {
            List<Quote> tempList = _context.Quote.ToList();

            await CreateQuoteJsonAsync(tempList, path);
            MemoryStream fileContents = await CopyFileToMemoryAsync(path);

            return fileContents;
        }

        private async Task CreateQuoteJsonAsync(List<Quote> tempList, string path)
        {
            using (FileStream createStream = System.IO.File.Create(path))
            {
                await JsonSerializer.SerializeAsync(createStream, tempList);
                await createStream.DisposeAsync();
            };
        }

        private async Task<MemoryStream> CopyFileToMemoryAsync(string path)
        {
            MemoryStream memory = new MemoryStream();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
                memory.Position = 0;
            }

            return memory;
        }

        private string GetQuoteListPath()
        {
            return Path.Combine(Environment.CurrentDirectory, @"wwwroot/files/", "Quote_List.json");
        }
    }
}