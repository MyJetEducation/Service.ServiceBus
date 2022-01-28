using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.ServiceBus.Services
{
    public interface IServiceBusPublisher<in T>
    {
	    ValueTask PublishAsync(T valueToPublish);

        Task PublishTaskAsync(T message);
        
        Task PublishTaskAsync(IEnumerable<T> messageList);
    }
}