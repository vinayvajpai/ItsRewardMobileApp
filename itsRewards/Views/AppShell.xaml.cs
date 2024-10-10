using System;
using System.Collections.Generic;
using itsRewards.Services.Altrias;
using itsRewards.Services.Auths;
using itsRewards.Services.Base;
using itsRewards.Services.Base.ModelServices;
using itsRewards.Services.Base.NavigationServices;
using itsRewards.Services.Base.ServiceProvider;
using itsRewards.ViewModels;
using itsRewards.Views;
using itsRewards.Views.Auths;
using TinyIoC;
using Xamarin.Forms;

namespace itsRewards.Views
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private static TinyIoCContainer container;

        public AppShell()
        {
            container = new TinyIoCContainer();
            RegisterCommonServices();
            RegisterServices();
            RegisterRoutes();
            InitializeComponent();
        }

        static void RegisterRoutes()
        {
            // REGISTER ROUTES
            RegisterRoute<LandingPage>();
            RegisterRoute<LoginPage>();
            RegisterRoute<SignUpPage>();
            RegisterRoute<HomePage>();
            RegisterRoute<SearchPage>();
            RegisterRoute<CouponsPage>();
            RegisterRoute<SettingsPage>();
            RegisterRoute<OfferDetailPage>();
            RegisterRoute<NotificationPage>();
            RegisterRoute<WebViewPage>();
            RegisterRoute<UserConsentPage>();
            RegisterRoute<VerifyInfoPage>();
            RegisterRoute<WalletPage>();
        }

        static void RegisterCommonServices()
        {
            RegisterSingleton<INavigationService, NavigationService>();
            RegisterSingleton<IInitialiserService, InitialiserService>();
            RegisterSingleton<ISpinner, AppSpinner>();
            RegisterSingleton<IAlertMessage, AlertMessage>();
            RegisterSingleton<IRequestProvider, RequestProvider>();
        }

        static void RegisterServices()
        {
            RegisterSingleton<IAccountService, AccountService>();
            RegisterSingleton<IAltriaService, AltriaService> ();
        }

        public static TService Resolve<TService>() where TService : class
        {
            return container.Resolve<TService>();
        }

        private static void RegisterRoute<TPage>() where TPage : class
        {
            var page = typeof(TPage).Name.ToLower();
            Routing.RegisterRoute(page, typeof(TPage));
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            container.Register<TInterface, T>().AsSingleton();
        }

        async void Shell_Navigating(System.Object sender, Xamarin.Forms.ShellNavigatingEventArgs e)
        {
            if (e.Current != null)
            {
                var deferral = e.GetDeferral(); // hey shell, wait a moment
                // intercept navigation here and do your custom logic. 
                // continue on to the destination route, cancel it, or reroute as needed

                // e.Cancel(); to stop routing
                // deferral.Complete(); to resume
                if (e.Current.Location.OriginalString.Contains("offerdetailpage") &&  !e.Target.Location.OriginalString.Contains("OffersPage"))
                {
                    e.Cancel();
                    await Shell.Current.GoToAsync("//OffersPage");
                    await Shell.Current.GoToAsync(e.Target.Location.OriginalString);
                }

                deferral.Complete();
            }
        }
    }
}
