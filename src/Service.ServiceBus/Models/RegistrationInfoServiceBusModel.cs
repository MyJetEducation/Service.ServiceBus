using System.Runtime.Serialization;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Регистрация пользователя
	/// </summary>
	[DataContract]
	public class RegistrationInfoServiceBusModel
	{
		public const string TopicName = "myjeteducation-registration-confirm";

		[DataMember(Order = 1)]
		public string Email { get; set; }

		[DataMember(Order = 2)]
		public string Hash { get; set; }
	}
}