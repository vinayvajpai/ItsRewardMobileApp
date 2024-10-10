using itsRewards.Helpers;
using Xamarin.Forms;

namespace itsRewards.Views
{
    public partial class OffersPage : ContentPage
    {
        public OffersPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //if (UriHelper.CheckIsAgeVerified())
            //    ViewModel.LoadDataCommand.Execute(null);
            //else
            //    ViewModel.LoadUserConsent();

            ViewModel.LoadDataCommand.Execute(null);
        }
    }
}