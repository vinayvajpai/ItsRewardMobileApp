using System.Collections.Generic;

namespace itsRewards.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public List<string> Promotions { get; set; }
        public List<string> Loyalty { get; set; }
        public List<string> OfferingNames { get; set; }
        public List<Offer> Offerings { get; set; }
        public string AccountNumber { get; set; }
        public string StoreName { get; set; }
        public bool Registered { get; set; }
        public bool Active { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Devise { get; set; }
        public string Merchant { get; set; }
        public string MAC { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string SecretKey { get; set; }
        public string ClientID { get; set; }
        public int Tier { get; set; }
        public List<WalletDetail> WalletDetails { get; set; }
        public List<string> Wallet { get; set; }

        public string Address
        {
            get
            {
                return string.Format("{0}, {1}, {2}, {3}, {4} - {5}", Address1, Address2, Address3, City, State, Zip);
                //return Address1 + " " + Address2 + " " + Address3 + " " + City + " " + " " + State + " " + Zip;
            }
        }

        public string Image { get; set; }
    }


    public class WalletDetail
    {
        public string Name { get; set; }
        public object Category { get; set; }
        public string BrandingURL { get; set; }
        public int OfferType { get; set; }
        public int BrandId { get; set; }
        public object Brand { get; set; }
        public string AdId { get; set; }
    }
}