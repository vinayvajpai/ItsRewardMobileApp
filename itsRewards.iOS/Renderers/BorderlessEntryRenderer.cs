using System;
using System.ComponentModel;
using itsRewards.Extensions.Controls;
using itsRewards.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(Entry), typeof(BorderlessEntryRenderer))]
[assembly: ExportRenderer(typeof(Entry), typeof(PinEntryRenderer))]
namespace itsRewards.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
                return;

            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }

    public class UIBackwardsTextField : UITextField
    {
        // A delegate type for hooking up change notifications.
        public delegate void DeleteBackwardEventHandler(object sender, EventArgs e);

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event DeleteBackwardEventHandler OnDeleteBackward;
        public void OnDeleteBackwardPressed()
        {
            if (OnDeleteBackward != null)
            {
                OnDeleteBackward(null, null);
            }
        }

        public UIBackwardsTextField()
        {
            BorderStyle = UITextBorderStyle.RoundedRect;
            ClipsToBounds = true;
        }

        public override void DeleteBackward()
        {
            base.DeleteBackward();
            OnDeleteBackwardPressed();
        }
    }

    public class PinEntryRenderer : EntryRenderer, IUITextFieldDelegate
    {

        IElementController ElementController => Element as IElementController;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (Element == null)
            {
                return;
            }
            if (Element.GetType() == typeof(PinEntry))
            {
                var entry = (PinEntry)Element;
                var textField = new UIBackwardsTextField();

                textField.EditingChanged += OnEditingChanged;
                textField.OnDeleteBackward += (sender, a) =>
                {
                    entry.OnBackspacePressed();
                };

                SetNativeControl(textField);
            }
            base.OnElementChanged(e);
        }

        void OnEditingChanged(object sender, EventArgs eventArgs)
        {
            ElementController.SetValueFromRenderer(Entry.TextProperty, Control.Text);
        }


    }
}