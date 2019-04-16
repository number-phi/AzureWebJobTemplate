using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebJobCoreTemplate.Services;
using WebJobCoreTemplate.Services.Interfaces;

namespace WebJobCoreTemplate
{
	class Program
	{
		private static IConfigurationRoot configuration;

		static async Task Main(string[] args)
		{
			configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional:false, reloadOnChange: true)
				//.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
				//.AddXmlFile("App.config", optional: false, reloadOnChange: true)
				.Build();

			var builder = new HostBuilder();
			
			builder.ConfigureWebJobs(b =>
			{
				b.AddAzureStorageCoreServices();
				b.AddAzureStorage();
				//per aggiungere la gestione di un service bus, importare il pacchetto nuget: Microsoft.Azure.WebJobs.Extensions.ServiceBus
				//b.AddServiceBus(sbOptions =>
				//{
				//	sbOptions.MessageHandlerOptions.AutoComplete = true;
				//	sbOptions.MessageHandlerOptions.MaxConcurrentCalls = 16;
				//	sbOptions.ConnectionString = configuration["ConnectionStrings:AzureWebJobsServiceBus"];
				//});
			});
			builder.ConfigureLogging((context, b) =>
			{
				b.SetMinimumLevel(LogLevel.Debug);
				b.AddConsole();

				// If the key exists in settings, use it to enable Application Insights.
				string instrumentationKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
				if (!string.IsNullOrEmpty(instrumentationKey))
				{
					b.AddApplicationInsights(o => o.InstrumentationKey = instrumentationKey);
				}
			});
			builder.ConfigureServices(s =>
			{
				s.AddSingleton<IWebJobService, WebJobService>();
				s.AddSingleton<IConfiguration>(configuration);
			})
			.UseConsoleLifetime();
			
			var host = builder.Build();
			using (host)
			{
				await host.RunAsync();
			}
		}
	}
}
