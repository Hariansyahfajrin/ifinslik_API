using System.Text.Json;
using Confluent.Kafka;
using Domain.Abstract.Service;
using Domain.Models;
using Messaging.Abstraction.IConsumerService;
using Service.Helper;

namespace Messaging.ConsumerService
{
	public class SysGeneralCodeConsumerService : BaseMessagingService, ISysGeneralCodeConsumerService
	{
		public Task Consume(string topic)
		{
			try
			{
				using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
				consumer.Subscribe(topic);

				TopicPartitionOffset? latestOffset = null;

				while (true)
				{
					var consumeResult = consumer.Consume();

					if (latestOffset == null || consumeResult.TopicPartitionOffset.Offset > latestOffset.Offset)
					{
						latestOffset = consumeResult.TopicPartitionOffset;
						var receivedData = JsonSerializer.Deserialize<SysGeneralCode>(consumeResult.Message.Value);
					}
				}
			}
			catch (Exception)
			{

				throw;
			}

		}
	}

}