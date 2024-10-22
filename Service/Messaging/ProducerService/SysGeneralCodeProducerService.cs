using Confluent.Kafka;
using Domain.Models;
using Messaging.Abstraction.IConsumerService;
using Service.Helper;
using System.Text.Json;

namespace Messaging.ProducerService
{
	public class SysGeneralCodeProducerService : BaseMessagingService, ISysGeneralCodeProducerService
	{
		public async Task Produce(string topic, SysGeneralCode data)
		{
			try
			{
				using var producer = new ProducerBuilder<int, string>(_producerConfig).Build();
				var jsonData = JsonSerializer.Serialize(data);
				var message = new Message<int, string> { Key = 1, Value = jsonData };
				var result = await producer.ProduceAsync(topic, message);
			}
			catch (Exception)
			{
				throw;
			}

		}



	}

}