using System;
using System.Windows.Input;
using itsRewards.Extensions;
using itsRewards.Services;
using itsRewards.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace itsRewards.ViewModels
{
    public class OfferDetailPageViewModel : BaseViewModel
    {
        #region Properties
        string brand;
        public string Brand
        {
            get { return brand; }
            set { SetProperty(ref brand, value); }
        }

        string couponhtml;
        public string CouponHtml
        {
            get { return couponhtml; }
            set { SetProperty(ref couponhtml, value); }
        }
        #endregion

        #region Commands
        public ICommand LoadDataCommand { get; }
        #endregion

        #region Constructor
        public OfferDetailPageViewModel()
        {
            #region Assign Commands
            LoadDataCommand = new Command(ExecuteLoadDataAsync);
            #endregion
        }
        #endregion

        #region Load Data Async
        async void ExecuteLoadDataAsync()
        {
            if (Xamarin.Essentials.Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                AlertMessage.Show("No Internet", "Please check your internet connection", "Ok");
                return;
            }
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CouponHtml = $"https://api.insightsc3m.com/rdcapp/index.html?Brand={Brand}&subscription-key=c94cc924617d438f895bb924a217a9e0" +
                                $"&access-token={ SharedPreferences.AltriaAccessToken}";
                    
#if notused
                CouponHtml = "https://uat1-retail.insightsc3m.com/index.html?Access-Token=" +
                    SharedPreferences.AltriaAccessToken +
                    "&Subscription-Key=" +
                    EndPoints.AltriaSubscriptionKey+
                    "&Brand="+ Brand;
#endif
            }
            catch (Exception ex)
            {
                AlertMessage.Show("Server Error!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
#endregion
    }
}