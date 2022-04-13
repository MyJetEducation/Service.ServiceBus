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
		public EducationTutorial? ClearTutorial { get; set; }

		[DataMember(Order = 3)]
		public int? ClearUnit { get; set; }

		[DataMember(Order = 4)]
		public int? ClearTask { get; set; }

		[DataMember(Order = 5)]
		public bool ClearAchievements { get; set; }

		[DataMember(Order = 6)]
		public bool ClearStatuses { get; set; }

		[DataMember(Order = 7)]
		public bool ClearHabits { get; set; }

		[DataMember(Order = 8)]
		public bool ClearSkills { get; set; }

		[DataMember(Order = 9)]
		public bool ClearKnowledge { get; set; }

		[DataMember(Order = 10)]
		public bool ClearUserTime { get; set; }

		[DataMember(Order = 11)]
		public bool ClearRetry { get; set; }
	}
}