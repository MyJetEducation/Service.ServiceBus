using System;
using System.Runtime.Serialization;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Новое значение пребывание пользователя в сервисе в течение определенного времени
	/// </summary>
	[DataContract]
	public class UserTimeChangedServiceBusModel
	{
		public const string TopicName = "myjeteducation-usertime";

		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public TimeSpan TotalSpan { get; set; }

		[DataMember(Order = 3)]
		public TimeSpan TodaySpan { get; set; }
	}
}