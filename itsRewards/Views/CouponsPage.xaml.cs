using itsRewards.Extensions;
using itsRewards.Helpers;
using itsRewards.Models.Auths;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace itsRewards.Views
{
    public partial class CouponsPage : ContentPage
    {
        public CouponsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (UriHelper.CheckIsAgeVerified())
            {
                frm.IsVisible = true;
            }
            else
            {
                frm.IsVisible = false;
            }
            ViewModel.LoadDataCommand.Execute(null);
        }

    }
}