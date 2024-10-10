using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using itsRewards.Extensions;
using itsRewards.Models;
using itsRewards.ViewModels.Base;
using Xamarin.Forms;

namespace itsRewards.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Properties
        public static List<Store> staticStores;
        public  List<Store> Stores
        {
            get { return staticStores; }
            set { SetProperty(ref staticStores, value); }
        }


        ObservableCollection<Store> filteredStores;
        public ObservableCollection<Store> FilteredStores
        {
            get { return filteredStores; }
            set { SetProperty(ref filteredStores, value); }
        }

        string searchKeyword;
        public string SearchKeyword
        {
            get {
                return searchKeyword;
            }
            set {
                searchKeyword = value;
                OnPropertyChanged("SearchKeyword");
                ExecuteSearchCommand();
            }
        }

        public static Store SelectedStores = null;
        #endregion

        #region Commands
        public ICommand LoadDataCommand { get; }
        public ICommand OpenStoreDetailCommand { get; }
        public ICommand SearchCommand { get; }
        #endregion

        #region Constructor
        public HomePageViewModel()
        {
            #region Assign Commands
            LoadDataCommand = new Command(ExecuteLoadDataAsync);
            OpenStoreDetailCommand = new Command<Store>(ExecuteOpenStoreDetailAsync);
            SearchCommand = new Command(ExecuteSearchCommand);
            FilteredStores = new ObservableCollection<Store>(Stores);
            #endregion
        }
        #endregion

        #region Load Data Async
        async void ExecuteLoadDataAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
               
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Open store detail Async
        async void ExecuteOpenStoreDetailAsync(Store store)
        {
            SharedPreferences.AccountNumber = store.AccountNumber;
            SelectedStores = store;
            MessagingCenter.Send<string>("AppShell", "ChangeTextBadge");
            //await _navigationService.GoToAsync(nameof(StoreDetailPage).ToLower());
        }

        void ExecuteSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchKeyword))
            {
                var filterlist = Stores.Where(x => x.StoreName.ToLower().Contains(SearchKeyword.ToLower())).ToList();
                FilteredStores = new ObservableCollection<Store>(filterlist);
            }
            else
            {
                FilteredStores = new ObservableCollection<Store>(Stores);
            }
        }

        #endregion
    }
}