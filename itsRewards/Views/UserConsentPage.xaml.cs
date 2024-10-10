using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace itsRewards.Views
{	
	public partial class UserConsentPage : ContentPage
	{	
		public UserConsentPage ()
		{
			InitializeComponent ();
		}

        void chk_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            
            if (chk1.IsChecked && chk2.IsChecked && chk3.IsChecked && chk4.IsChecked)
                chkMain.IsChecked = true;
            else
                chkMain.IsChecked = false;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (!chkMain.IsChecked)
            {
                chkMain.IsChecked = true;
                chk1.IsChecked = true;
                chk2.IsChecked = true;
                chk3.IsChecked = true;
                chk4.IsChecked = true;
            }
            else
            {
                chkMain.IsChecked = false;
                chk1.IsChecked = false;
                chk2.IsChecked = false;
                chk3.IsChecked = false;
                chk4.IsChecked = false;
            }
        }
    }
}

