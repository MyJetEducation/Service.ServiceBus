using System;
using System.Runtime.Serialization;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Смена email пользователя
	/// </summary>
	[DataContract]
	public class ChangeEmailServiceBusModel
	{
		public const string TopicName = "myjeteducation-change-email";

		[DataMember(Order = 1)]
		public string Email { get; set; }

		[DataMember(Order = 2)]
		public string Hash { get; set; }
	}
}