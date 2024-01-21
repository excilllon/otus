using System.Diagnostics;
using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model;
using BtreeIndexProject.WebApp.Models;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BtreeIndexProject.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IQueryExecutor _queryExecutor;
		private readonly IMetaDataManager _metaDataManager;

		public HomeController(ILogger<HomeController> logger, IQueryExecutor queryExecutor, IMetaDataManager metaDataManager)
		{
			_logger = logger;
			_queryExecutor = queryExecutor;
			_metaDataManager = metaDataManager;
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
		public async Task<IActionResult> ExecuteQuery([FromBody] QueryModelInput modelInput)
		{
			var resultModel = await _queryExecutor.Execute(modelInput);
			return PartialView("_ResultsPartialView", resultModel);
		}

		[HttpGet]
		public object GetHierarchicalData(DataSourceLoadOptions loadOptions)
		{
			return new DbTreeItem[]
			{
				new DbTreeItem
				{
					Name = "База данных",
					icon = "folder",
					Items = new[]
					{
						new DbTreeItem()
						{
							Name = "Таблицы",
							icon = "tableproperties",
							Items = new[]
							{
								new DbTreeItem()
								{
									Name = "Cars",
									icon = "detailslayout",
									Items = new []
									{
										new DbTreeItem()
										{
											Name = "Колонки",
											icon = "fields",
											Items = _metaDataManager.GetTableColumns("Cars").Select(c => new DbTreeItem()
											{
												Name = c.Name, 
												icon = c.Name == "ID" ? "key" :"font",
												Expanded = false
											}).ToArray()
										},
										new DbTreeItem()
										{
											Name = "Индексы",
											icon = "share",
											Items = _metaDataManager.GetTableAllTableIndices("Cars").Select(c => new DbTreeItem()
											{
												Name = c.Name, 
												Expanded = false
											}).ToArray()
										}
									}
									
								}
							}
						}
					}
				}
			};
		}
	}
}