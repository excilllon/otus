using BtreeIndexProject.Abstractions;
using Microsoft.Extensions.Hosting;

namespace BtreeIndexProject.Services.BackgroundServices
{
	public  class DbmsInitializer: BackgroundService
	{
		private readonly IMetaDataReader _metaDataReader;

		public DbmsInitializer(IMetaDataReader metaDataReader)
		{
			_metaDataReader = metaDataReader;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await _metaDataReader.ReadMetaData("C:\\Users\\Admin\\source\\repos\\Otus\\HomeWork\\BtreeIndexProject.WebApp\\bin\\Debug\\net6.0\\DataBase\\");
		}
	}
}
