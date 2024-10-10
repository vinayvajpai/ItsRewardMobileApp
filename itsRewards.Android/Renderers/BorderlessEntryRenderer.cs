using Android.Content;
using Android.Views;
using itsRewards.Droid.Renderers;
using itsRewards.Extensions.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(BorderlessEntryRenderer))]
[assembly: ExportRenderer(typeof(Entry), typeof(PinEntryRenderer))]
namespace itsRewards.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }

    public class PinEntryRenderer : EntryRenderer
    {
        public PinEntryRenderer(Context context) : base(context)
        {
        }

        public override bool DispatchKeyEvent(KeyEvent e)
        {
            if (e.Action == KeyEventActions.Down)
            {
                if (e.KeyCode == Keycode.Del)
                {
                    if (string.IsNullOrWhiteSpace(Control.Text))
                    {
                        if (Element.GetType() == typeof(PinEntry))
                        {
                            var entry = (PinEntry)Element;
                            entry.OnBackspacePressed();
                        }
                    }
                }
            }
            return base.DispatchKeyEvent(e);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }

    }
}