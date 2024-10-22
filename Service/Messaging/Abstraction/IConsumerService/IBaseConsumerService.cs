using Domain.Models;
namespace Messaging.Abstraction.IConsumerService
{
   public interface IBaseConsumerService<T> where T : BaseModel
   {
      Task Consume(string data);
   }
}