using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using itsRewards.Extensions;
using itsRewards.Models;
using itsRewards.Models.Auths;
using itsRewards.ViewModels;
using Newtonsoft.Json;

namespace itsRewards.Helpers
{
    public static class UriHelper
    {
        public static string CombineUri(params string[] uriParts)
        {
            string uri = string.Empty;
            if (uriParts != null && uriParts.Count() > 0)
            {
                char[] trims = new char[] { '\\', '/' };
                uri = (uriParts[0] ?? string.Empty).TrimEnd(trims);
                for (int i = 1; i < uriParts.Count(); i++)
                {
                    uri = string.Format("{0}/{1}", uri.TrimEnd(trims), (uriParts[i] ?? string.Empty).TrimStart(trims));
                }
            }
            return uri;
        }

        public static NotificationModel GetNotificationSetting()
        {
            try
            {
                if (App.Current.Properties.ContainsKey("Notification"))
                {
                    var json = App.Current.Properties["Notification"].ToString();
                    return JsonConvert.DeserializeObject<NotificationModel>(json);
                }
            }
            catch (System.Exception ex)
            {

            }
            return new NotificationModel() { EmailNotification = true,AppNotification=true };
        }

        public static string ReadHtmlFileContent(string FileName)
        {
            string jsonFileName = FileName;
            string GetPageCode = null;
            try
            {
                var assembly = typeof(UriHelper).GetTypeInfo().Assembly;
                var FPath = assembly.GetName().Name + ".Resources";
                Stream stream = assembly.GetManifestResourceStream($"{FPath}.{jsonFileName}");

                using (var reader = new System.IO.StreamReader(stream))
                {
                    GetPageCode = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return GetPageCode;
        }

        public static bool CheckIsAgeVerified()
        {
            if(SharedPreferences.LoginUserInfo != null)
            {
                DisplayUserModel userModel = JsonConvert.DeserializeObject<DisplayUserModel>(SharedPreferences.LoginUserInfo);
                if (userModel != null)
                {
                    if (userModel.AgeVerified)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public static bool IsSelectedStoreTire()
        {
            try
            {
                if (HomePageViewModel.SelectedStores != null)
                {
                    if (HomePageViewModel.SelectedStores.Tier == 3)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}