using System;
using System.Runtime.Serialization;
using Service.Education.Structure;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Сброс прогресса обучения и баллов пользователя
	/// </summary>
	[DataContract]
	public class ClearEducationProgressServiceBusModel
	{
		public const string TopicName = "myjeteducation-clear-education-progress";

		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public EducationTutorial? Tutorial { get; set; }

		[DataMember(Order = 3)]
		public int? Unit { get; set; }

		[DataMember(Order = 4)]
		public int? Task { get; set; }

		[DataMember(Order = 5)]
		public bool? ClearUiProgress { get; set; }
	}
} 