using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace itsRewards.Services.Base.NavigationServices
{
    public interface INavigationService
    {
        INavigation Navigation { get; }
        Shell Shell { get; }
        Page CurrentPage { get; }
        NavigationDirection NavigationDirection { get; set; }
        Task GoToAsync<TViewModel>(string route, Action<TViewModel> initialiser = null, bool animate = true);
        Task GoToAsync(string route, bool animate = true);
        Task GoBackAsync<TViewModel>(Action<TViewModel> initialiser, bool animate = true);
        Task GoBackAsync(bool animate = true);
        Task GoBackAsync(string route, bool animate = true);
        Task GoToRootAsync(bool animate = true);
        void GoToShell(string title, bool animate = true);
        void GoToShell<TViewModel>(string route, Action<TViewModel> initialiser = null, bool animate = true);
        void GoToShellRootAsync(bool animate = true);

    }
}
