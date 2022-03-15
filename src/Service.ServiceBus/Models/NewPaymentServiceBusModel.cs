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
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public Guid? CardId { get; set; }

		[DataMember(Order = 3)]
		public string Number { get; set; }

		[DataMember(Order = 4)]
		public string Holder { get; set; }

		[DataMember(Order = 5)]
		public string Month { get; set; }

		[DataMember(Order = 6)]
		public string Year { get; set; }

		[DataMember(Order = 7)]
		public string Cvv { get; set; }

		[DataMember(Order = 8)]
		public string ServiceCode { get; set; }

		[DataMember(Order = 9)]
		public string Info { get; set; }

		[DataMember(Order = 10)]
		public string Provider { get; set; }

		[DataMember(Order = 11)]
		public Guid? TransactionId { get; set; }
	}
}