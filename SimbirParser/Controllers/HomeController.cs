using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimbirParser.Data;
using SimbirParser.Data.Models;
using SimbirParser.ViewModels;

namespace SimbirParser.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Separators array
        /// </summary>
        private readonly char[] SEPARATORS = { ' ', ',', '.', '!', '?', '"', '«' , '»', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' };
    
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// DB context
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        public HomeController(ILogger<HomeController> logger, 
                              ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Get main page with parser
        /// </summary>
        /// <returns>Parser View</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View(new StatsViewModel { Url = "https://www.simbirsoft.com/" });
        }

        /// <summary>
        /// Post main page with parser
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Parser View with result</returns>
        [HttpPost]
        public async Task<IActionResult> Index(StatsViewModel model)
        {
            try 
            {
                // Parsing
                var web = new HtmlWeb();
                var htmlDocument = await web.LoadFromWebAsync(model.Url);
                var text = htmlDocument.DocumentNode.SelectSingleNode("//body").InnerText;
                var words = text.ToUpper().Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
                
                // Get stats
                model.UniqueWordsCount = words.GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());
                
                // Add stats to DB
                var now = DateTime.Now;
                
                await _context.AddRangeAsync(model.UniqueWordsCount.Select(uwc => new UniqueWordFrequency
                {
                    Word = uwc.Key,
                    CountOnPage = uwc.Value,
                    PageUrl = model.Url,
                    CheckDateTime = now
                }));
                
                await _context.SaveChangesAsync();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during parsing");
                throw;
            }
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns>Error view</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
