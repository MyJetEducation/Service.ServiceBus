using System;
using System.Runtime.Serialization;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Пользователь произвел оплату услуги
	/// </summary>
	[DataContract]
	public class NewPaymentServiceBusModel
	{
		public const string TopicName = "myjeteducation-payment";

		[DataMember(Order = 1)]
		public string UserId { get; set; }

		[DataMember(Order = 2)]
		public Guid? CardId { get; set; }

		[DataMember(Order = 11)]
		public Guid? TransactionId { get; set; }
	}
}