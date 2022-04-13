using System;
using System.Runtime.Serialization;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Пройдено профилирование
	/// </summary>
	[DataContract]
	public class ProfilingFinishedServiceBusModel
	{
		public const string TopicName = "myjeteducation-profiling-finished";

		[DataMember(Order = 1)]
		public string UserId { get; set; }

		/// <summary>
		///     Длинное
		/// </summary>
		[DataMember(Order = 2)]
		public bool Long { get; set; }
	}
}