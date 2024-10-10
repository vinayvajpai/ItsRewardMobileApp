using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace itsRewards.Services.Base.NavigationServices
{
    public class NavigationService : INavigationService
    {
        private readonly IInitialiserService _initialiserService;
        public INavigation Navigation => GetCurrentPage().Navigation;
        public Shell Shell => Application.Current.MainPage as Shell;
        public Page CurrentPage => GetCurrentPage();
        public NavigationDirection NavigationDirection { get; set; }
        public string Root { get; set; } = "root";

        private Stack<string> _navigationStack;

        public NavigationService(IInitialiserService initialiserService)
        {
            _initialiserService = initialiserService;
            _navigationStack = new Stack<string>();
        }

        private Page GetCurrentPage()
        {
            var mainPage = GetShellPage(Application.Current.MainPage);

            if (mainPage?.Navigation?.NavigationStack?.Count() > 1)
                return mainPage?.Navigation?.NavigationStack.LastOrDefault();

            switch (mainPage)
            {
                case TabbedPage _:
                case CarouselPage _:
                    return ((MultiPage<Page>)mainPage).CurrentPage;
                default:
                    return mainPage;
            }
        }

        private Page GetShellPage(object item)
        {
            if (item is null)
                return null;

            if (item is ShellContent content)
                return content.Content as Page;

            if (item is Shell shell)
                return GetShellPage(shell.CurrentItem);

            if (item is ShellItem shellItem)
            {
                if (shellItem.CurrentItem?.Stack?.Count > 1)
                {
                    return GetShellPage(shellItem.CurrentItem.Stack.LastOrDefault());
                }
                else
                {
                    return GetShellPage(shellItem.CurrentItem);
                }
            }


            if (item is ShellGroupItem shellGroupItem)
            {
                return GetShellPage(shellGroupItem);
            }

            if (item is ShellSection shellSection)
                return GetShellPage(shellSection.CurrentItem);

            if (item is Page)
                return item as Page;

            return null;
        }


        public async Task GoToAsync<TViewModel>(string route, Action<TViewModel> initialiser = null, bool animate = true)
        {
            if (initialiser != null)
                _initialiserService.SetInitialiser(initialiser);

            await GoToAsync(route, animate);
        }

        public async Task GoToAsync(string route, bool animate = true)
        {
            if (Shell == null)
                return;

            NavigationDirection = NavigationDirection.Forward;

            if (AddToNavigationStack(route))
                await Shell.Current.GoToAsync(route, animate);
        }

        public async Task GoBackAsync<TViewModel>(Action<TViewModel> initialiser = null, bool animate = true)
        {
            if (initialiser != null)
                _initialiserService.SetInitialiser(initialiser);

            await GoBackAsync(animate);
        }

        public async Task GoBackAsync(bool animate = true)
        {
            if (Shell == null)
                return;

            NavigationDirection = NavigationDirection.Backwards;

            if (_navigationStack.Any())
            {
                try
                {
                    _navigationStack.Pop();
                    await Navigation.PopAsync(animate);
                }
                catch (Exception ex)
                {

                }
            }
            else
                await GoToRootAsync();
        }

        public async Task GoToRootAsync(bool animate = true)
        {
            if (Shell == null)
                return;

            NavigationDirection = NavigationDirection.Backwards;

            // clear
            _navigationStack.Clear();

            await Navigation.PopToRootAsync(animate);
        }

        public async Task GoBackAsync(string route, bool animate = true)
        {
            if (Shell == null)
                return;

            NavigationDirection = NavigationDirection.Backwards;

            var itemindex = _navigationStack.IndexOf(route.Replace("/", ""));
            for (int j = 0; j < itemindex; j++)
            {
                _navigationStack.Pop();
                await Navigation.PopAsync(false);
            }
        }

        public void GoToShellRootAsync(bool animate = true)
        {
            if (Shell == null)
                return;

            NavigationDirection = NavigationDirection.Backwards;
            if (Shell.Current != null && Shell.Current.Items != null && Shell.Current.Items.Count > 0)
            {
                Shell.Current.CurrentItem = Shell.Current.Items.First(x => x.Route == Root);
            }
        }

        public bool AddToNavigationStack(string route)
        {
            if (NavigationDirection == NavigationDirection.Backwards)
                return false;

            _navigationStack.Push(route.Replace("/", ""));
            NavigationDirection = NavigationDirection.Forward;

            return true;
        }

        public void GoToShell(string title, bool animate = true)
        {
            if (Shell == null)
                return;

            NavigationDirection = NavigationDirection.Backwards;

            // clear
            _navigationStack.Clear();

            Shell.Current.CurrentItem = Shell.Current.Items.FirstOrDefault(x => x.Route == title);
        }

        public void GoToShell<TViewModel>(string route, Action<TViewModel> initialiser = null, bool animate = true)
        {
            if (Shell == null)
                return;

            NavigationDirection = NavigationDirection.Backwards;

            // clear
            _navigationStack.Clear();


            if (initialiser != null)
                _initialiserService.SetInitialiser(initialiser);

            Shell.Current.CurrentItem = Shell.Current.Items.FirstOrDefault(x => x.Route == route);
        }
    }
}