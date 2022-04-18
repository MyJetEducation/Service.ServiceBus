using System.Runtime.Serialization;
using Service.MarketProduct.Domain.Models;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Пользователь купил товар
	/// </summary>
	[DataContract]
	public class MarketProductPurchasedServiceBusModel
	{
		public const string TopicName = "myjeteducation-market-product-purchased";

		[DataMember(Order = 1)]
		public string UserId { get; set; }

		[DataMember(Order = 2)]
		public MarketProductType Product { get; set; }

		[DataMember(Order = 3)]
		public decimal? Amount { get; set; }
	}
}