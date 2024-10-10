using System;
using itsRewards.Models;
using System.Collections.ObjectModel;
using itsRewards.ViewModels.Base;
using itsRewards.LocalDataBase;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace itsRewards.ViewModels
{
	public class WalletPageViewModel : BaseViewModel
	{
		public WalletPageViewModel()
		{
            RemoveToWalletCommand = new Command<WalletDatabaseTable>(ExecuteRemoveToWalletCommand);
        }

        #region Properties
        ObservableCollection<WalletDatabaseTable> wallets = new ObservableCollection<WalletDatabaseTable>();
        public ObservableCollection<WalletDatabaseTable> Wallets
        {
            get { return wallets; }
            set { SetProperty(ref wallets, value); }
        }
        #endregion

        #region Commands
        #region Commands
        public ICommand RemoveToWalletCommand { get; }
        #endregion
        #endregion

        #region Methods
        public void LoadWallets()
        {
            try
            {
                InitDataBaseTable db = new InitDataBaseTable();
                var saveCouponsList = db.Connection.Table<WalletDatabaseTable>().ToList();
                if(saveCouponsList != null && saveCouponsList.Count > 0)
                {
                    Wallets = new ObservableCollection<WalletDatabaseTable>(saveCouponsList);
                }

                if (HomePageViewModel.SelectedStores != null)
                {
                    if (HomePageViewModel.SelectedStores.Wallet != null)
                    {
                        foreach (var wallet in HomePageViewModel.SelectedStores.Wallet)
                        {
                            Wallets.Add(new WalletDatabaseTable()
                            {
                                data = wallet,
                                Type = "C",
                                Id = 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {}
        }

        void ExecuteRemoveToWalletCommand(WalletDatabaseTable coupon)
        {
            try
            {
                //if (coupon.Id != null)
                {
                    InitDataBaseTable db = new InitDataBaseTable();
                    db.Delete<WalletDatabaseTable>(coupon.Id);
                    var wallObj = Wallets.ToList().Where(c => c.Id == coupon.Id).FirstOrDefault();
                    Wallets.Remove(wallObj);
                    OnPropertyChanged("Wallets");
                    MessagingCenter.Send<string>("AppShell", "ChangeTextBadge");
                }
            }
            catch (Exception ex)
            {}
        }

        #endregion
    }
}

