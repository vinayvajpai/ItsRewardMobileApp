using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using itsRewards.LocalDataBase;
using itsRewards.Models;
using itsRewards.Services.Altrias;
using itsRewards.Services.Base.ServiceProvider;
using itsRewards.ViewModels.Base;
using itsRewards.Views;
using itsRewards.Views.Auths;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace itsRewards.ViewModels
{
    public class CouponsPageViewModel : BaseViewModel
    {
        InitDataBaseTable db;

        #region Properties
        ObservableCollection<Coupon> coupons = new ObservableCollection<Coupon>();
        public ObservableCollection<Coupon> Coupons
        {
            get { return coupons; }
            set { SetProperty(ref coupons, value); }
        }


        ObservableCollection<CouponCategory> couponCategories = new ObservableCollection<CouponCategory>()
        {
           new CouponCategory()
           {
               ID=1,
               Name = "Promotions",
               IsSelected = true
           },
            new CouponCategory()
           {
                ID=2,
               Name = "Loyalty",
               IsSelected = false
           }
        };

        public ObservableCollection<CouponCategory> CouponCategories
        {
            get { return couponCategories; }
            set { SetProperty(ref couponCategories, value); }
        }

        #endregion

        #region Commands
        public ICommand ProfileCommand { get; }
        public ICommand LoadDataCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand AddRemoveToWalletCommand { get; }
        public ICommand SelectCouponCategoryCommand { get; }
        #endregion

        #region Services
        public IAltriaService AltriaService { get; }
        #endregion

        #region Constructor
        public CouponsPageViewModel()
        {
            #region Assign Commands
            db = new InitDataBaseTable();
            ProfileCommand = new Command(ExecuteProfileAsync);
            LoadDataCommand = new Command(() =>
            {
                ExecuteLoadDataAsync(false);
            });
            CopyCommand = new Command<Coupon>(ExecuteCopyAsync);
            AddRemoveToWalletCommand = new Command<Coupon>(ExecuteAddToWalletCommand);
            SelectCouponCategoryCommand = new Command<CouponCategory>(ExecuteSelectCouponCategoryCommand);
            #endregion

            #region Assign Services
            AltriaService = itsRewards.Views.AppShell.Resolve<IAltriaService>();
            #endregion
        }
        #endregion

        #region Open store detail Async

        async void ExecuteProfileAsync()
        {
            //App.IsSignUpFirstTime = false;
            //await _navigationService.GoToAsync(nameof(SignUpPage).ToLower());
            await _navigationService.GoToAsync(nameof(WebViewPage).ToLower());
            //await _navigationService.GoToAsync(nameof(StoreDetailPage).ToLower());
        }

        void ExecuteCopyAsync(Coupon coupon)
        {
            //await _navigationService.GoToAsync(nameof(StoreDetailPage).ToLower());
        }
        #endregion

        #region Load Data Async

        void ExecuteLoadDataAsync(bool IsLoyalty=true)
        {
            if (Xamarin.Essentials.Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                AlertMessage.Show("No Internet", "Please check your internet connection", "Ok");
                return;
            }

            if (HomePageViewModel.SelectedStores == null)
            {
                AlertMessage.Show("No Store Selected", "Please select any store from home screen.", "Ok");

                return;
            }

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                Coupons.Clear();
                if (IsLoyalty)
                {
                    foreach (var loyalty in HomePageViewModel.SelectedStores.Loyalty)
                    {
                        Coupons.Add(new Coupon()
                        {
                            Name = loyalty,
                            IsAddToWallet = CheckIsAddInWallet(loyalty)
                        });
                    }
                }
                else
                {
                    foreach (var promotion in HomePageViewModel.SelectedStores.Promotions)
                    {
                        Coupons.Add(new Coupon()
                        {
                            Name = promotion,
                            IsAddToWallet = CheckIsAddInWallet(promotion)
                        });
                    }
                }

                //foreach (var promotion in HomePageViewModel.SelectedStores.Promotions)
                //{
                //    Coupons.Add(new Coupon()
                //    {
                //        Name = promotion
                //    });
                //}

                //List<Coupon> response = await AltriaService.GetCoupons(SelectedBrand.Value);
                //if (response != null && response.Count > 0)
                //{
                //    Coupons = response;
                //}
            }
            catch (HttpRequestExceptionEx ex)
            {
                if (ex.HttpCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                    AlertMessage.Show("Server Error!", "Something went wrong.", "Ok");
                }
                else
                {
                    AlertMessage.Show("Server Error!", ex.Message, "Ok");
                }
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

        void ExecuteAddToWalletCommand(Coupon coupon)
        {
            try
            {
                if (coupon.IsAddToWallet == false)
                {
                    var saveCoupons = db.Connection.Table<WalletDatabaseTable>().ToList();
                    if (saveCoupons != null && saveCoupons.Count > 0)
                    {
                        var saveCoupon = saveCoupons.Where(c => c.data.ToLower() == coupon.Name).FirstOrDefault();
                        if (saveCoupon == null)
                        {
                            db.Save<WalletDatabaseTable>(new WalletDatabaseTable()
                            {
                                data = coupon.Name,
                                Type = "C"
                            });
                            coupon.IsAddToWallet = true;
                        }
                    }
                    else
                    {
                        db.Save<WalletDatabaseTable>(new WalletDatabaseTable()
                        {
                            data = coupon.Name,
                            Type = "C"
                        });
                        coupon.IsAddToWallet = true;
                    }
                }
                else
                {
                    var saveCoupons = db.Connection.Table<WalletDatabaseTable>().ToList();
                    if (saveCoupons != null && saveCoupons.Count > 0)
                    {
                        var saveCoupon = saveCoupons.Where(c => c.data.ToLower() == coupon.Name.ToLower()).FirstOrDefault();
                        if (saveCoupon != null)
                        {
                            db.Delete<WalletDatabaseTable>(saveCoupon.Id);
                            coupon.IsAddToWallet = false;
                        }
                    }
                }

                MessagingCenter.Send<string>("AppShell", "ChangeTextBadge");
            }
            catch (Exception ex)
            {

            }
        }

        bool CheckIsAddInWallet(string name)
        {
            try
            {
                InitDataBaseTable db = new InitDataBaseTable();
                var saveCoupons = db.Connection.Table<WalletDatabaseTable>().ToList();
                if (saveCoupons != null && saveCoupons.Count > 0)
                {
                    var saveCoupon = saveCoupons.Where(c => c.data.ToLower() == name.ToLower()).FirstOrDefault();
                    if (saveCoupon != null)
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return false;
        }

        void ExecuteSelectCouponCategoryCommand(CouponCategory couponCategory)
        {
            CouponCategories.All(x => {
                x.IsSelected = x.Name == couponCategory.Name;
                return true;
            });
            if (couponCategory.ID == 1)
                ExecuteLoadDataAsync(false);
            else
                ExecuteLoadDataAsync(true);

        }

        #endregion
    }
}