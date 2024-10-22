using Domain.Models;
namespace Messaging.Abstraction.IConsumerService
{
    public interface IBaseProducerService<T> where T : BaseModel
    {
        Task Produce(string topic, T data);
    }
}