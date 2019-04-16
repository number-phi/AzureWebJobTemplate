using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebJobCoreTemplate.Services.Interfaces;

namespace WebJobCoreTemplate
{
	public class Functions
	{
		private readonly IWebJobService _webJobService;		
		private readonly object lockObject = new object();
		private readonly IConfiguration _configuration;

		public Functions(IWebJobService webJobService, IConfiguration configuration)
		{
			_webJobService = webJobService;
			_configuration = configuration;
		}

		//public async Task ProcessQueueMessage([ServiceBusTrigger("hr-relation")] string message, ILogger logger)
		//{
			
		//	logger.LogInformation(message);
		//	try
		//	{
		//		PersonRelationChangeMessage workItem;
		//		lock(lockObject)
		//		{
		//			workItem = JsonConvert.DeserializeObject<PersonRelationChangeMessage>(message);
		//		}				

		//		if (workItem != null)
		//		{
		//			await _privilegeService.Run(workItem);
		//		}

		//	}
		//	catch (System.Exception)
		//	{

		//		throw;
		//	}
			
		//}
	}
}
