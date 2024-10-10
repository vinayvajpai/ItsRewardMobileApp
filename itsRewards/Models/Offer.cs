using System;
using itsRewards.Helpers;
using itsRewards.Services;
using Xamarin.Forms;

namespace itsRewards.Models
{
    public class Offer
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string BrandingURL { get; set; }
        public OfferingType OfferType { get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public string AdId { get; set; }
        public string BrandingFullURL { get {
                if (UriHelper.IsSelectedStoreTire() && !string.IsNullOrEmpty(BrandingURL))
                {
                    return string.Format("http://smokinrebate.us:8082/images/{0}", BrandingURL);
                }
                else
                {
                    return "http://smokinrebate.us:8082/images/marlboro-default.png";
                }
            } }
    }

    public enum OfferingType
    {
        Ad,
        Choice,
        Match,
        Promotion,
        Loyalty
    }

}