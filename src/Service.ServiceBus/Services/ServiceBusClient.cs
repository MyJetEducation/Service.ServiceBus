using System;
using System.Collections.Generic;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.LivnesProbs;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;

namespace Service.ServiceBus.Services
{
	public static class ServiceBusClient
	{
		public static MyServiceBusTcpClient RegisterServiceBusClient(this ContainerBuilder builder, Func<string> getHostPort, ILoggerFactory loggerFactory)
		{
			MyServiceBusTcpClient serviceBusClient = builder.RegisterMyServiceBusTcpClient(getHostPort, loggerFactory);

			var manager = new ServiceBusManager(serviceBusClient, getHostPort?.Invoke());
			builder.RegisterInstance(manager).As<IServiceBusManager>().SingleInstance();

			builder.RegisterType<ServiceBusLifeTime>().AsSelf().As<ILivenessReporter>().SingleInstance().AutoActivate();

			return serviceBusClient;
		}

		public static MyServiceBusTcpClient RegisterServiceBusClient<TServiceBusModel>(this ContainerBuilder builder, Func<string> getHostPort, string topicName, ILoggerFactory loggerFactory)
			where TServiceBusModel : class
		{
			MyServiceBusTcpClient tcpServiceBus = builder.RegisterServiceBusClient(getHostPort, loggerFactory);

			builder
				.RegisterInstance(new ServiceBusPublisher<TServiceBusModel>(tcpServiceBus, topicName, false))
				.As<IPublisher<TServiceBusModel>>()
				.SingleInstance();

			tcpServiceBus.Start();

			return tcpServiceBus;
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