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
		public string UserId { get; set; }

		[DataMember(Order = 2)]
		public ClearEducationProgressInfo ClearProgressInfo { get; set; }

		[DataMember(Order = 3)]
		public ClearEducationProgressFlags ClearFlags { get; set; }
	}

	[DataContract]
	public class ClearEducationProgressInfo
	{
		[DataMember(Order = 1)]
		public EducationTutorial? Tutorial { get; set; }

		[DataMember(Order = 2)]
		public int? Unit { get; set; }

		[DataMember(Order = 3)]
		public int? Task { get; set; }
	}

	[DataContract]
	public class ClearEducationProgressFlags
	{
		[DataMember(Order = 1)]
		public bool Progress { get; set; }

		[DataMember(Order = 2)]
		public bool Achievements { get; set; }

		[DataMember(Order = 3)]
		public bool Statuses { get; set; }

		[DataMember(Order = 4)]
		public bool Habits { get; set; }

		[DataMember(Order = 5)]
		public bool Skills { get; set; }

		[DataMember(Order = 6)]
		public bool Knowledge { get; set; }

		[DataMember(Order = 7)]
		public bool UserTime { get; set; }

		[DataMember(Order = 8)]
		public bool Retry { get; set; }
	}
}