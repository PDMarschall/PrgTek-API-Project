using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTAP.Core.Interfaces;
using PTAP.Core.Models;
using PTAP.Infrastructure.Data;
using PTAP.Web.Models;
using System.Text.Json;

namespace PTAP.Web.Controllers
{
    public class KanyeController : Controller
    {
        private readonly KanyeContext _context;
        private readonly IKanyeClient _kanyeClient;
        private readonly string _quoteListPath;

        public KanyeController(KanyeContext context, IKanyeClient kanye)
        {
            _context = context;
            _kanyeClient = kanye;
            _quoteListPath = GetQuoteListPath();
        }

        public async Task<IActionResult> Index()
        {
            KanyeWisdomViewModel viewModel = await GetKanyeViewModel();
            await PersistWisdom(viewModel);
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

        private async Task PersistWisdom(KanyeWisdomViewModel kanyeWisdom)
        {
            _context.Add(kanyeWisdom.WisdomText);
            await _context.SaveChangesAsync();
        }


        private async Task<MemoryStream> SerializeQuotesJsonAsync(string path)
        {
            List<KanyeQuote> tempList = _context.Quote.ToList();

            await CreateQuoteJsonAsync(tempList, path);
            MemoryStream fileContents = await CopyFileToMemoryAsync(path);

            return fileContents;
        }

        private async Task CreateQuoteJsonAsync(List<KanyeQuote> tempList, string path)
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