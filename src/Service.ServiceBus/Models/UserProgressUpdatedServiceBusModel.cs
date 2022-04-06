using System;
using System.Runtime.Serialization;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Обновление прогресса пользователя
	/// </summary>
	[DataContract]
	public class UserProgressUpdatedServiceBusModel
	{
		public const string TopicName = "myjeteducation-user-progress-updated";

		[DataMember(Order = 1)]
		public string UserId { get; set; }

		/// <summary>
		///     Кол-во заработанных привычек
		/// </summary>
		[DataMember(Order = 2)]
		public int HabitCount { get; set; }
	}
}