using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using itsRewards.Models;
using itsRewards.ViewModels.Base;
using Xamarin.Forms;

namespace itsRewards.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        #region Properties
        List<Store> stores;
        public List<Store> Stores
        {
            get { return stores; }
            set { SetProperty(ref stores, value); }
        }

        List<Store> filteredStores;
        public List<Store> FilteredStores
        {
            get { return filteredStores; }
            set { SetProperty(ref filteredStores, value); }
        }

        string searchKeyword;
        public string SearchKeyword
        {
            get { return searchKeyword; }
            set { SetProperty(ref searchKeyword, value); }
        }
        #endregion

        #region Commands
        public ICommand LoadDataCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand OpenStoreDetailCommand { get; }
        #endregion

        #region Constructor
        public SearchPageViewModel()
        {
            #region Assign Commands
            LoadDataCommand = new Command(ExecuteLoadDataAsync);
            SearchCommand = new Command(ExecuteSearchCommand);
            OpenStoreDetailCommand = new Command<Store>(ExecuteOpenStoreDetailAsync);
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
                Stores = HomePageViewModel.staticStores;
                SearchCommand.Execute(null);
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
            //await _navigationService.GoToAsync(nameof(StoreDetailPage).ToLower());
        }
        #endregion

        #region Execute Search
        void ExecuteSearchCommand()
        {
            FilteredStores = Stores.Where(x=>x.StoreName.ToLower().Contains(SearchKeyword.ToLower())).ToList();
        }
        #endregion
    }
}