using System;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace itsRewards.Views.CommonViews
{
    public partial class AlertPage : PopupPage
    {
        public event EventHandler backgroundClick;

        private readonly bool _isCancellable;

        public static readonly BindableProperty AlertTitleProperty = BindableProperty.Create(
            nameof(AlertTitle),
            typeof(string),
            typeof(AlertPage),
            string.Empty, BindingMode.TwoWay, null, propertyChanged: OnTitlePropertyChanged);


        public string AlertTitle
        {
            get => (string)GetValue(AlertTitleProperty);
            set => SetValue(AlertTitleProperty, value);
        }

        public static readonly BindableProperty AlertMessageProperty = BindableProperty.Create(
           nameof(AlertMessage),
           typeof(string),
           typeof(AlertPage),
           string.Empty, BindingMode.TwoWay, null, propertyChanged: OnMessagePropertyChanged);

        public string AlertMessage
        {
            get => (string)GetValue(AlertMessageProperty);
            set => SetValue(AlertMessageProperty, value);
        }

        public static readonly BindableProperty CancelButtonTextProperty = BindableProperty.Create(
           nameof(CancelButtonText),
           typeof(string),
           typeof(AlertPage),
           string.Empty, BindingMode.TwoWay, null, propertyChanged: OnCancelButtonTextPropertyChanged);

        public string CancelButtonText
        {
            get => (string)GetValue(CancelButtonTextProperty);
            set => SetValue(CancelButtonTextProperty, value);
        }

        public event EventHandler CloseButtonClicked;

        public AlertPage(bool isCancellable = true)
        {
            InitializeComponent();
            _isCancellable = isCancellable;
        }

        private static void OnTitlePropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var page = bindable as AlertPage;
            page.MessageTitle.Text = (string)newVal;

        }

        private static void OnMessagePropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var page = bindable as AlertPage;
            page.Message.Text = (string)newVal;
        }

        private static void OnCancelButtonTextPropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var page = bindable as AlertPage;
            page.Cancel.Text = (string)newVal;
        }


        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            CloseButtonClicked?.Invoke(sender, e);
        }


        // Invoked when back button on Android is clicked
        protected override bool OnBackButtonPressed()
        {
            // Return default value - OnBackButtonPressed
            if (_isCancellable)
                return false;

            return true;
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            if (backgroundClick != null)
            {
                backgroundClick.Invoke(null,null);
            }

            if (_isCancellable)
                return true;

            return false;
        }
    }
}