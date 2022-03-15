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
			(typeof (SetProgressInfoServiceBusModel), new SetProgressInfoServiceBusModel {UserId = new Guid("7568c9bb-438a-4d80-ab84-d8d6763d7f7f"), Duration = TimeSpan.Zero, IsRetry = true, SetUserProgress = true, Task = 1, Tutorial = EducationTutorial.Economics, Unit = 1, Progress = 80}),
			(typeof (UserAccountFilledServiceBusModel), new UserAccountFilledServiceBusModel {UserId = new Guid("7568c9bb-438a-4d80-ab84-d8d6763d7f7f")}),
			(typeof (UserProgressUpdatedServiceBusModel), new UserProgressUpdatedServiceBusModel {UserId = new Guid("7568c9bb-438a-4d80-ab84-d8d6763d7f7f"), HabitCount = 1}),
			(typeof (ChangeEmailServiceBusModel), new ChangeEmailServiceBusModel {Email = "some@email.com", Hash = "123"}),
			(typeof (NewPaymentServiceBusModel), new NewPaymentServiceBusModel {CardId = new Guid("44584892-a988-43a7-98cd-57654894df8e"), Cvv = "123", Holder = "holder", Info = "info", Month = "01", Number = "123", Provider = "test", ServiceCode = "retry_pack", TransactionId = new Guid("3ba27c2f-c503-4960-a3e2-2ca2f5b1cefc"), UserId = new Guid("08c6f0ac-2a1b-4970-b0a2-17d5c945a293"), Year = "2001"})
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