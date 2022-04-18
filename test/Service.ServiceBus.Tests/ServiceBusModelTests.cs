using System;
using System.Reflection;
using MyJetWallet.Sdk.ServiceBus;
using Newtonsoft.Json;
using NUnit.Framework;
using Service.Education.Structure;
using Service.MarketProduct.Domain.Models;
using Service.ServiceBus.Models;

namespace Service.ServiceBus.Tests
{
	public class ServiceBusModelTests
	{
		private static readonly (Type type, object model)[] TestBusModels =
		{
			(typeof (ProfilingFinishedServiceBusModel), new ProfilingFinishedServiceBusModel {UserId = "7568c9bb-438a-4d80-ab84-d8d6763d7f7f", Long = true}),
			(typeof (RecoveryInfoServiceBusModel), new RecoveryInfoServiceBusModel {Email = "some@email.com", Hash = "qwe"}),
			(typeof (RegistrationInfoServiceBusModel), new RegistrationInfoServiceBusModel {Email = "some@email.com", Hash = "qwe"}),
			(typeof (RetryUsedServiceBusModel), new RetryUsedServiceBusModel {UserId = "7568c9bb-438a-4d80-ab84-d8d6763d7f7f", Count = 1,}),
			(typeof (SetProgressInfoServiceBusModel), new SetProgressInfoServiceBusModel {UserId = "7568c9bb-438a-4d80-ab84-d8d6763d7f7f", Duration = TimeSpan.Zero, IsRetry = true, SetUserProgress = true, Task = 1, Tutorial = EducationTutorial.Economics, Unit = 1, Progress = 80}),
			(typeof (UserAccountFilledServiceBusModel), new UserAccountFilledServiceBusModel {UserId = "7568c9bb-438a-4d80-ab84-d8d6763d7f7f"}),
			(typeof (UserProgressUpdatedServiceBusModel), new UserProgressUpdatedServiceBusModel {UserId = "7568c9bb-438a-4d80-ab84-d8d6763d7f7f", HabitCount = 1}),
			(typeof (ChangeEmailServiceBusModel), new ChangeEmailServiceBusModel {Email = "some@email.com", Hash = "123"}),
			(typeof (NewPaymentServiceBusModel), new NewPaymentServiceBusModel {CardId = new Guid("44584892-a988-43a7-98cd-57654894df8e"), TransactionId = new Guid("3ba27c2f-c503-4960-a3e2-2ca2f5b1cefc"), UserId = "08c6f0ac-2a1b-4970-b0a2-17d5c945a293"}),
			(typeof (UserTimeChangedServiceBusModel), new UserTimeChangedServiceBusModel {UserId = "08c6f0ac-2a1b-4970-b0a2-17d5c945a293", TotalSpan = TimeSpan.FromDays(1), TodaySpan = TimeSpan.FromMinutes(1)}),
			(typeof (ClearEducationUiProgressServiceBusModel), new ClearEducationUiProgressServiceBusModel {UserId = "08c6f0ac-2a1b-4970-b0a2-17d5c945a293"}),
			(typeof (MarketProductPurchasedServiceBusModel), new MarketProductPurchasedServiceBusModel {UserId = "08c6f0ac-2a1b-4970-b0a2-17d5c945a293", Product = MarketProductType.MascotSkin, Amount = 10}),
			(typeof (ClearEducationProgressServiceBusModel), new ClearEducationProgressServiceBusModel {UserId = "08c6f0ac-2a1b-4970-b0a2-17d5c945a293", ClearAchievements = true, ClearStatuses = true, ClearHabits = true, ClearSkills = true, ClearKnowledge = true, ClearUserTime = true, ClearRetry = true, ClearProgress = true})
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