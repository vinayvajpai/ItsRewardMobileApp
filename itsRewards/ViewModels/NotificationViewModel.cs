using System;
using itsRewards.Extensions.Validations;
using itsRewards.Models;
using itsRewards.ViewModels.Base;
using Newtonsoft.Json;

namespace itsRewards.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {
        public NotificationViewModel()
        {
            if (!App.Current.Properties.ContainsKey("Notification"))
            {
                IsEmail = new bool();
                IsAppNotification = new bool();
                IsBefore = new bool();
                IsAfter = new bool();
                IsEmail = true;
                IsAppNotification = true;
            }
            else
            {
                GetNotificationSettings();
            }
        }

        #region Properties
       bool _IsEmail =new bool();
        public bool IsEmail
        {
            get {  return _IsEmail; }
            set {
                SetProperty(ref _IsEmail, value);
                SetNotificationSettings();
            }
        }

        bool _IsAppNotification =new bool();
        public bool IsAppNotification
        {
            get { return _IsAppNotification; }
            set {

                SetProperty(ref _IsAppNotification, value);
                SetNotificationSettings();


            }
        }

        bool _IsBefore= new bool();
        public bool IsBefore
        {
            get { return _IsBefore; }
            set { SetProperty(ref _IsBefore, value);
                SetNotificationSettings();


            }
        }

        bool _IsAfter = new bool();
        public bool IsAfter
        {
            get { return _IsAfter; }
            set {
                SetProperty(ref _IsAfter, value);
                SetNotificationSettings();
            }
        }

        #endregion

        #region Method

        void SetNotificationSettings()
        {
            try
            {
                var modelNotification = new NotificationModel();
                modelNotification.AfterNotification = IsAfter;
                modelNotification.BeforeNotification = IsBefore;
                modelNotification.EmailNotification = IsEmail;
                modelNotification.AppNotification = IsAppNotification;
                var json = JsonConvert.SerializeObject(modelNotification);
                App.Current.Properties["Notification"] = json;
                App.Current.SavePropertiesAsync();

            }
            catch (Exception ex)
            {

            }
        }

        void GetNotificationSettings()
        {
            try
            {
                if (App.Current.Properties.ContainsKey("Notification")) {
                    var json = App.Current.Properties["Notification"].ToString();
                    var notificationModel = JsonConvert.DeserializeObject<NotificationModel>(json);
                    IsAfter = notificationModel.AfterNotification;
                    IsBefore = notificationModel.BeforeNotification;
                    IsEmail = notificationModel.EmailNotification;
                    IsAppNotification = notificationModel.AppNotification;
                }

            }
            catch (Exception ex)
            {

            }
        }


        #endregion
    }
}

