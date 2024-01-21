using BtreeIndexProject.Abstractions;
using Microsoft.Extensions.Hosting;

namespace BtreeIndexProject.Services.BackgroundServices
{
	public  class DbmsInitializer: BackgroundService
	{
		private readonly IMetaDataManager _metaDataManager;

		public DbmsInitializer(IMetaDataManager metaDataManager)
		{
			_metaDataManager = metaDataManager;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await _metaDataManager.ReadMetaData();
		}
	}
}
