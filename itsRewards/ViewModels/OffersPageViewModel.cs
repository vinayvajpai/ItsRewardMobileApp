using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using itsRewards.LocalDataBase;
using itsRewards.Models;
using itsRewards.Services.Altrias;
using itsRewards.Services.Base.ServiceProvider;
using itsRewards.ViewModels.Base;
using itsRewards.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace itsRewards.ViewModels
{
    public class OffersPageViewModel : BaseViewModel
    {
        InitDataBaseTable db;

        #region Properties
        List<Offer> Offerlist = new List<Offer>();

        ObservableCollection<Offer> offers = new ObservableCollection<Offer>();
        public ObservableCollection<Offer> Offers
        {
            get { return offers; }
            set { SetProperty(ref offers, value); }
        }
        ObservableCollection<OfferCategory> offerCategories = new ObservableCollection<OfferCategory>();

        public ObservableCollection<OfferCategory> OfferCategories
        {
            get { return offerCategories; }
            set { SetProperty(ref offerCategories, value); }
        }
        #endregion

        #region Commands
        public ICommand LoadDataCommand { get; }
        public ICommand SelectOfferCategoryCommand { get; }
        public ICommand OpenOfferCommand { get; }
        #endregion

        #region Services
        public IAltriaService AltriaService { get; }
        #endregion

        #region Constructor
        public OffersPageViewModel()
        {
            #region Assign Commands
            db = new InitDataBaseTable();
            LoadDataCommand = new Command(ExecuteLoadDataAsync);
            SelectOfferCategoryCommand = new Command<OfferCategory>(ExecuteSelectOfferCategoryCommand);
            #endregion

            #region Assign Services
            AltriaService = itsRewards.Views.AppShell.Resolve<IAltriaService>();
            #endregion

           
        }
        #endregion


        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }
            return value.ToString();
        }
 

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


            if (HomePageViewModel.SelectedStores == null)
            {
                Offerlist = new List<Offer>() {
                     new Offer(){
                        Name = "Marlboro Offering",
                        Category = "Cigerate Offerings",
                        OfferType = 0,
                        BrandId = 27,
                        Brand = "marlboro"
                    },
                    new Offer(){
                        Name = "L&M Offering",
                        Category = "Cigerate Offerings",
                        OfferType = 0,
                        BrandId = 13,
                        Brand = "lm"
                    },
                    new Offer(){
                        Name = "Copenhagen Offering",
                        Category = "Smokeless Offerings",
                        OfferType = 0,
                        BrandId = 2400,
                        Brand = "copenhagen"
                    },
                    new Offer(){
                        Name = "Skoal Offering",
                        Category = "Smokeless Offerings",
                        OfferType = 0,
                        BrandId = 1000,
                        Brand = "skoal"
                    }
                };

                OfferCategories.Clear();
                OfferCategories.Add(new OfferCategory() { Name = GetEnumDescription(OfferCategoryEnum.AllOffers) });
                foreach (var offer in Offerlist)
                {
                    if (!OfferCategories.Any(x=>x.Name == offer.Category))
                    {
                        OfferCategories.Add(new OfferCategory() { Name = offer.Category });
                    }
                }
                OfferCategories.FirstOrDefault().IsSelected = true;
                FilterOffers();
                IsBusy = false;
            }
            else
            {
                try
                {
                    OfferCategories.Clear();
                    Offerlist = HomePageViewModel.SelectedStores.Offerings;

                    if (Offerlist != null && Offerlist.Count > 0)
                    {
                        Offerlist.All(x =>
                        {
                            x.Brand = x.Category;
                            return true;
                        });
                    }

                    OfferCategories.Add(new OfferCategory() { Name = GetEnumDescription(OfferCategoryEnum.AllOffers) });
                    foreach (var offer in HomePageViewModel.SelectedStores.OfferingNames)
                    {
                        OfferCategories.Add(new OfferCategory() { Name = offer });
                    }
                    OfferCategories.FirstOrDefault().IsSelected = true;
                    FilterOffers();
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
        }

       public async void LoadUserConsent()
        {
            try
            {
                await _navigationService.GoToAsync(nameof(UserConsentPage).ToLower());
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        void FilterOffers()
        {
            Offers.Clear();
            var selectedOffer = OfferCategories.FirstOrDefault(x => x.IsSelected);
            if (selectedOffer.Name != GetEnumDescription(OfferCategoryEnum.AllOffers))
            {
                var fOffers = Offerlist.Where(x => x.Category == selectedOffer.Name);
                foreach (var offer in fOffers)
                {
                    Offers.Add(offer);
                }
            }
            else
            {
                if (Offerlist != null)
                {
                    foreach (var offer in Offerlist)
                    {
                        Offers.Add(offer);
                    }
                }
            }
        }


        #region Brand Item Changed Async
        void ExecuteSelectOfferCategoryCommand(OfferCategory offerCategory)
        {
            OfferCategories.All(x => {
                x.IsSelected = x.Name == offerCategory.Name;
                return true;
            });
            FilterOffers();
        }

        
        #endregion

        #region Open Offer
        async void ExecuteOpenOfferCommand(Offer offer)
        {
            await _navigationService.GoToAsync<OfferDetailPageViewModel>(nameof(OfferDetailPage).ToLower(), vm => {
                vm.Brand = offer.Brand.ToLower();
            });
        }
        #endregion   
    }
}