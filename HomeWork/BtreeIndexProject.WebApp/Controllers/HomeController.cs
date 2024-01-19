using System.Diagnostics;
using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model;
using BtreeIndexProject.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BtreeIndexProject.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQueryExecutor _queryExecutor;

        public HomeController(ILogger<HomeController> logger, IQueryExecutor queryExecutor)
        {
	        _logger = logger;
	        _queryExecutor = queryExecutor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ExecuteQuery([FromBody]QueryModelInput modelInput)
        {
	        var resultModel = _queryExecutor.Execute(modelInput);
	        return PartialView("_ResultsPartialView", resultModel);
        }
    }
}