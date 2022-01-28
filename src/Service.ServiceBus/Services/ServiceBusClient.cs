using System;
using System.Collections.Generic;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
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

		public static ContainerBuilder RegisterServiceBusSubscriberBatch<T>(this ContainerBuilder builder, MyServiceBusTcpClient client, string topicName, string queueName, TopicQueueType queryType = TopicQueueType.Permanent)
		{
			builder
				.RegisterInstance(new MyServiceBusSubscriber<T>(client, topicName, queueName, queryType, true))
				.As<ISubscriber<IReadOnlyList<T>>>()
				.SingleInstance();

			return builder;
		}

		public static ContainerBuilder RegisterServiceBusSubscriberSingle<T>(this ContainerBuilder builder, MyServiceBusTcpClient client, string topicName, string queueName, TopicQueueType queryType = TopicQueueType.Permanent)
		{
			builder
				.RegisterInstance(new MyServiceBusSubscriber<T>(client, topicName, queueName, queryType, false))
				.As<ISubscriber<T>>()
				.SingleInstance();

			return builder;
		}
	}
}