using System;
using System.Runtime.Serialization;
using Service.MarketProduct.Domain.Models;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Пользователь купил товар типа "маскот"
	/// </summary>
	[DataContract]
	public class NewMascotProductServiceBusModel
	{
		public const string TopicName = "myjeteducation-new-mascot-product";

		[DataMember(Order = 1)]
		public string UserId { get; set; }

		[DataMember(Order = 2)]
		public MarketProductType Product { get; set; }
	}
}