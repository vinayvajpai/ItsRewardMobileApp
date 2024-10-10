/*
 * 04/20/2022 ALI - Updated to add max lengths to files
 */
using System;
using itsRewards.ViewModels.Auths;
using Xamarin.Forms;
namespace itsRewards.Views.Auths
{
    
    public partial class SignUpPage : ContentPage
    {
        SignUpPageViewModel vm;
        public SignUpPage()
        {
            InitializeComponent();
            vm = new SignUpPageViewModel();
            BindingContext = vm;
            vm.IsAgree = false;
        }

        

        void Pin1_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 1)
            {
                Pin2.Focus();
            }
        }

        void Pin2_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 1)
            {
                Pin3.Focus();
            }

            if (e.NewTextValue.Length == 0)
            {
                Pin2.OnBackspace += EntryBackspaceEventHandler2;

            }
        }

        void Pin3_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 1)
            {
                Pin4.Focus();
            }
            if (e.NewTextValue.Length == 0)
            {
                Pin3.OnBackspace += EntryBackspaceEventHandler3;
            }
        }

        void Pin4_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 1)
            {
                Pin5.Focus();
            }

            if (e.NewTextValue.Length == 0)
            {
                Pin4.OnBackspace += EntryBackspaceEventHandler4;
            }
        }

        void Pin5_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 1)
            {
                Pin6.Focus();
            }

            if (e.NewTextValue.Length == 0)
            {
                Pin5.OnBackspace += EntryBackspaceEventHandler5;
            }
        }

        private void EntryBackspaceEventHandler2(object sender, EventArgs e)
        {
            Pin1.Focus();
        }

        private void EntryBackspaceEventHandler3(object sender, EventArgs e)
        {
            Pin2.Focus();
        }

        private void EntryBackspaceEventHandler4(object sender, EventArgs e)
        {
            Pin3.Focus();
        }

        private void EntryBackspaceEventHandler5(object sender, EventArgs e)
        {
            Pin4.Focus();
        }

        private void EntryBackspaceEventHandler6(object sender, EventArgs e)
        {
            Pin5.Focus();
        }

        void OnVerifyCode_Click(System.Object sender, System.EventArgs e)
        {
            var pin = $"{Pin1.Text}{Pin2.Text}{Pin3.Text}{Pin4.Text}{Pin5.Text}{Pin6.Text}";
            //vm.Pin =pin;
            vm.ExecutePinCommand(pin);
        }

        void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if(chkVerify.IsChecked)
            {
                vm.IsAgree = true;
                btnSubmit.Opacity = 1;
            }
            else
            {
                vm.IsAgree = false;
                btnSubmit.Opacity = 0.5;
            }
        }
    }
}
