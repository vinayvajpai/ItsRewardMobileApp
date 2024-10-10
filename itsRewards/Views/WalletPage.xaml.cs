using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace itsRewards.Views
{	
	public partial class WalletPage : ContentPage
	{	
		public WalletPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadWallets();
        }
    }
}

