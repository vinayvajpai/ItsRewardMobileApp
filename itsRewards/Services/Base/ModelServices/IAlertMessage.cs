namespace itsRewards.Services.Base.ModelServices
{
    public interface IAlertMessage
    {
        void Show(string title, string message, string cancelButtonName, bool isCancellable = true);
    }
}