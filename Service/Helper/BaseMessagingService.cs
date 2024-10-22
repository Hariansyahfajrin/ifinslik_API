using Confluent.Kafka;
using DotNetEnv;
namespace Service.Helper
{
   public class BaseMessagingService
   {
      private string bootstrapServers = Env.GetString("BOOTSTRAP_SERVER");
      private string groupID = Env.GetString("GROUP_ID");
      protected ProducerConfig _producerConfig;
      protected ConsumerConfig _consumerConfig;

      public BaseMessagingService()
      {
         _producerConfig = new ProducerConfig { BootstrapServers = bootstrapServers };
         _consumerConfig = new ConsumerConfig
         {
            BootstrapServers = bootstrapServers,
            GroupId = groupID,
            AutoOffsetReset = AutoOffsetReset.Earliest
         };
      }

      protected string GUID()
      {
         return Guid.NewGuid().ToString("N").ToLower();
      }
   }
}