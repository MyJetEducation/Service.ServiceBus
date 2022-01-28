using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.TcpClient;

namespace Service.ServiceBus.Services
{
	public class ServiceBusPublisher<T> : IServiceBusPublisher<T>
	{
		private readonly MyServiceBusTcpClient _client;
		private readonly string _topicName;
		private readonly bool _immediatelyPersist;

		public ServiceBusPublisher(MyServiceBusTcpClient client, string topicName, bool immediatelyPersist)
		{
			_client = client;
			_topicName = topicName;
			_immediatelyPersist = immediatelyPersist;
			_client.CreateTopicIfNotExists(topicName);
		}

		public async ValueTask PublishAsync(T valueToPublish) => await PublishTaskAsync(valueToPublish);

		public Task PublishTaskAsync(T message) => _client.PublishAsync(_topicName, message.ServiceBusContractToByteArray(), _immediatelyPersist);

		public Task PublishTaskAsync(IEnumerable<T> messageList)
		{
			List<byte[]> batch = messageList.Select(e => e.ServiceBusContractToByteArray()).ToList();

			return _client.PublishAsync(_topicName, batch, _immediatelyPersist);
		}
	}
}