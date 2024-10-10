using System;
using itsRewards.LocalDataBase;
using System.Collections.ObjectModel;
using itsRewards.ViewModels.Base;
using Xamarin.Forms;

namespace itsRewards.ViewModels
{
	public class AppShellViewModel : BaseViewModel
	{
		public AppShellViewModel()
		{
            MessagingCenter.Subscribe<string>("AppShell", "ChangeTextBadge", (val) =>
            {
                SetWalletBadgeCount();
            });
            SetWalletBadgeCount();
        }

        #region Property
        private string _badgeText = "";
        public string BadgeText
        {
            get => _badgeText;
            set
            {
                _badgeText = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        private void ChangeText(string value)
        {
            BadgeText = value;
        }


        public void SetWalletBadgeCount()
        {
            try
            {
                InitDataBaseTable db = new InitDataBaseTable();
                var saveCouponsList = db.Connection.Table<WalletDatabaseTable>().ToList();
                if (saveCouponsList != null && saveCouponsList.Count > 0)
                {
                    ChangeText(saveCouponsList.Count.ToString());
                }
                else
                {
                    ChangeText(string.Empty);
                }

                if (HomePageViewModel.SelectedStores != null)
                {
                    if (HomePageViewModel.SelectedStores.Wallet != null)
                    {
                        if (saveCouponsList != null && saveCouponsList.Count > 0)
                        {
                            var totalCount = saveCouponsList.Count + HomePageViewModel.SelectedStores.Wallet.Count;
                            ChangeText(totalCount.ToString());
                        }
                        else
                        {
                            ChangeText(HomePageViewModel.SelectedStores.Wallet.Count.ToString());
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}

