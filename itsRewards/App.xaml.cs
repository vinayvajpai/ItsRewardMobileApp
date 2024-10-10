using Xamarin.Forms;
using itsRewards.Views;
using itsRewards.Models;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using Plugin.FirebasePushNotification;

[assembly: ExportFont("Ubuntu-Bold.ttf", Alias = "Ubuntu Bold")]
[assembly: ExportFont("Ubuntu-Light.ttf", Alias = "Ubuntu Light")]
[assembly: ExportFont("Ubuntu-Medium.ttf", Alias = "Ubuntu Medium")]
[assembly: ExportFont("Ubuntu-Regular.ttf", Alias = "Ubuntu")]
[assembly: ResolutionGroupName("itsRewards")]
namespace itsRewards
{
    public partial class App : Application
    {
        public static bool IsSignUpFirstTime = false;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            if (!App.Current.Properties.ContainsKey("Notification"))
            {
                SetNotificationSettings();
            }
        }
        protected override void OnStart(){
            if (Device.RuntimePlatform == Device.iOS)
            {
                //iOS stuff

                CrossFirebasePushNotification.Current.Subscribe("general");
                CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
                };
                System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

                CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine("Received");
                        if (p.Data.ContainsKey("body"))
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }

                };

                CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
                {
                    //System.Diagnostics.Debug.WriteLine(p.Identifier);

                    System.Diagnostics.Debug.WriteLine("Opened");
                    foreach (var data in p.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }

                    if (!string.IsNullOrEmpty(p.Identifier))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {

                        });
                    }
                    else if (p.Data.ContainsKey("color"))
                    {

                    }
                    else if (p.Data.ContainsKey("aps.alert.title"))
                    {
                    }
                };

                CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine("Action");

                    if (!string.IsNullOrEmpty(p.Identifier))
                    {
                        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                        foreach (var data in p.Data)
                        {
                            System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                        }

                    }

                };

                CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine("Dismissed");
                };
            }
        }
        protected override void OnSleep(){}
        protected override void OnResume(){}

        void SetNotificationSettings()
        {
            try
            {
                var modelNotification = new NotificationModel();
                modelNotification.AfterNotification = false;
                modelNotification.BeforeNotification = false;
                modelNotification.EmailNotification = true;
                modelNotification.AppNotification = true;
                var json = JsonConvert.SerializeObject(modelNotification);
                App.Current.Properties["Notification"] = json;
                App.Current.SavePropertiesAsync();

            }
            catch (Exception ex)
            {

            }
        }
    }
}