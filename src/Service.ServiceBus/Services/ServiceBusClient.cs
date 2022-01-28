using System;
using Autofac;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.TcpClient;

namespace Service.ServiceBus.Services
{
	public static class ServiceBusClient
	{
		public static MyServiceBusTcpClient RegisterServiceBusClient<TServiceBusModel>(this ContainerBuilder builder, Func<string> getHostPort, string topicName, ILoggerFactory loggerFactory)
			where TServiceBusModel : class
		{
			MyServiceBusTcpClient tcpServiceBus = builder.RegisterMyServiceBusTcpClient(getHostPort, loggerFactory);

			builder
				.RegisterInstance(new MyServiceBusPublisher<TServiceBusModel>(tcpServiceBus, topicName, false))
				.As<IServiceBusPublisher<TServiceBusModel>>()
				.SingleInstance();

			tcpServiceBus.Start();

			return tcpServiceBus;
		}
		
		public static MyServiceBusTcpClient RegisterServiceBusClient(this ContainerBuilder builder, Func<string> getHostPort, ILoggerFactory loggerFactory)
		{
			return builder.RegisterMyServiceBusTcpClient(getHostPort, loggerFactory);
		}
	}
}