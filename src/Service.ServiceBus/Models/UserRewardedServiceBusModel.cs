using System.Runtime.Serialization;
using Service.Core.Client.Constants;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Пользователь получил ачивки/статусы
	/// </summary>
	[DataContract]
	public class UserRewardedServiceBusModel
	{
		public const string TopicName = "myjeteducation-user-rewarded";

		[DataMember(Order = 1)]
		public string UserId { get; set; }

		[DataMember(Order = 2)]
		public UserAchievement[] Achievements { get; set; }

		[DataMember(Order = 3)]
		public UserStatusGrpcModel[] Statuses { get; set; }
	}

	[DataContract]
	public class UserStatusGrpcModel
	{
		[DataMember(Order = 1)]
		public UserStatus Status { get; set; }

		[DataMember(Order = 2)]
		public int? Level { get; set; }
	}
}