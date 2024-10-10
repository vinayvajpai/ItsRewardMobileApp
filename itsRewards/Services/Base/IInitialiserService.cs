using System;
namespace itsRewards.Services.Base
{
    public interface IInitialiserService
    {
        void SetInitialiser<TViewModel>(Action<TViewModel> initialiser);
        void Initialise<TViewModel>(TViewModel viewModel);
    }
}
