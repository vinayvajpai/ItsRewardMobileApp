namespace itsRewards.Services.Base.ModelServices
{
    public interface ISpinner
    {
        void HideLoading();
        void ShowLoading(bool isCancellable = true);
    }
}