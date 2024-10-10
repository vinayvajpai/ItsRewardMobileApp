using System;
using Xamarin.Forms;

namespace itsRewards.Extensions.Controls
{
    public class CustomEntry : Entry
    {

    }

	public class PinEntry : Entry
	{
		public delegate void BackspaceEventHandler(object sender, EventArgs e);

		public event BackspaceEventHandler OnBackspace;

		public PinEntry() { }

		public void OnBackspacePressed()
		{
			if (OnBackspace != null) 
			{
				OnBackspace(null, null);
			}

		}
	}
}
