using System;
using System.Reflection;
using MyJetWallet.Sdk.ServiceBus;
using Newtonsoft.Json;
using NUnit.Framework;
using Service.Education.Structure;
using Service.ServiceBus.Models;

namespace Service.ServiceBus.Tests
{
	public class ServiceBusModelTests
	{
		private static readonly (Type type, object model)[] TestBusModels =
		{
			(typeof (ProfilingFinishedServiceBusModel), new ProfilingFinishedServiceBusModel {UserId = new Guid("7568c9bb-438a-4d80-ab84-d8d6763d7f7f"), Long = true}),
			(typeof (RecoveryInfoServiceBusModel), new RecoveryInfoServiceBusModel {Email = "some@email.com", Hash = "qwe"}),
			(typeof (RegistrationInfoServiceBusModel), new RegistrationInfoServiceBusModel {Email = "some@email.com", Hash = "qwe"}),
			(typeof (RetryUsedServiceBusModel), new RetryUsedServiceBusModel {UserId = new Guid("7568c9bb-438a-4d80-ab84-d8d6763d7f7f"), Count = 1,}),
			(typeof (SetProgressInfoServiceBusModel), new SetProgressInfoServiceBusModel {UserId = new Guid("7568c9bb-438a-4d80-ab84-d8d6763d7f7f"), Duration = TimeSpan.Zero, IsRetry = true, SetUserProgress = true, Task = 1, Tutorial = EducationTutorial.Economics, Unit = 1}),
			(typeof (UserAccountFilledServiceBusModel), new UserAccountFilledServiceBusModel {UserId = new Guid("7568c9bb-438a-4d80-ab84-d8d6763d7f7f")}),
			(typeof (UserProgressUpdatedServiceBusModel), new UserProgressUpdatedServiceBusModel {UserId = new Guid("7568c9bb-438a-4d80-ab84-d8d6763d7f7f"), HabitCount = 1}),
			(typeof (ChangeEmailServiceBusModel), new ChangeEmailServiceBusModel {UserId = new Guid("7568c9bb-438a-4d80-ab84-d8d6763d7f7f"), Hash = "123"})
		};

		public static void AreEqualByJson(object expected, object actual)
		{
			string expectedJson = JsonConvert.SerializeObject(expected);
			string actualJson = JsonConvert.SerializeObject(actual);
			Assert.AreEqual(expectedJson, actualJson);
		}

		[TestCaseSource(nameof(TestBusModels))]
		public void TestProfilingFinishedServiceBusModel_serialization((Type type, object model) tuple)
		{
			(Type type, object sourceModel) = tuple;

			MethodInfo method = typeof (ContractToDomainMapper).GetMethod(
				nameof(ContractToDomainMapper.ByteArrayToServiceBusContract),
				new[] {typeof (ReadOnlyMemory<byte>)});

			var byteArray = (ReadOnlyMemory<byte>) sourceModel.ServiceBusContractToByteArray();

			MethodInfo genericMethod = method?.MakeGenericMethod(type);

			object destModel = genericMethod?.Invoke(this, new object[] {byteArray});

			AreEqualByJson(sourceModel, destModel);
		}
	}
}