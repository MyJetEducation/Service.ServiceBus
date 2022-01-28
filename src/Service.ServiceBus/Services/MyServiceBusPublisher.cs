using System.Threading.Tasks;
using DotNetCoreDecorators;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.TcpClient;

namespace Service.ServiceBus.Services
{
	public class ServiceBusPublisher<T> : IPublisher<T>
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

		public async ValueTask PublishAsync(T valueToPublish) =>
			await _client.PublishAsync(_topicName, valueToPublish.ServiceBusContractToByteArray(), _immediatelyPersist);
	}
}