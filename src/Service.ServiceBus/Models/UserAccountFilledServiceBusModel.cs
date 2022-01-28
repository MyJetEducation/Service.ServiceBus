﻿using System;
using System.Runtime.Serialization;

namespace Service.ServiceBus.Models
{
	/// <summary>
	///     Внес все персональные данные в профиле
	/// </summary>
	[DataContract]
	public class UserAccountFilledServiceBusModel
	{
		public const string TopicName = "myjeteducation-useraccount-filled";

		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }
	}
}