using System.Runtime.Serialization;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Сброс прогресса обучения для UI
	/// </summary>
	[DataContract]
	public class ClearEducationUiProgressServiceBusModel
	{
		public const string TopicName = "myjeteducation-clear-education-ui-progress";

		[DataMember(Order = 1)]
		public string UserId { get; set; }
	}
}