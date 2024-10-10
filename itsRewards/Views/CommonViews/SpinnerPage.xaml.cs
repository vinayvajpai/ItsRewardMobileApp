using Rg.Plugins.Popup.Pages;

namespace itsRewards.Views.CommonViews
{
    public partial class SpinnerPage : PopupPage
    {
        private readonly bool _isCancellable;

        public SpinnerPage(bool isCancellable = true)
        {
            InitializeComponent();
            _isCancellable = isCancellable;
        }

        // Invoked when back button on Android is clicked
        protected override bool OnBackButtonPressed()
        {
            // Return default value - OnBackButtonPressed
            if (_isCancellable)
                return false;

            return true;
        }

        //// Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            if (_isCancellable)
                return true;

            return false;
        }
    }
}