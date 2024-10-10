using System;
namespace itsRewards.Models
{
	public class NotificationModel
	{
		public bool EmailNotification { get; set; }
        public bool AppNotification { get; set; }
        public bool BeforeNotification { get; set; }
        public bool AfterNotification { get; set; }
    }
}

