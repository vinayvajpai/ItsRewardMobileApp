using Newtonsoft.Json;

namespace itsRewards.Models.Auths
{
    public class RegistrationRequestModel
    {
        [JsonProperty(PropertyName = "userName")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "pin")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "cellPhone")]
        public string Cell { get; set; }

        public bool AgeVerified { get; set; }

        [JsonProperty(PropertyName = "Tocken")]
        public string Token { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public Purchases Purchases { get; set; }

        public bool IsRegistered { get; set; }

        public string Pin { get; set; }

        public bool OptOutEmail { get; set; }
        public bool OptOutNotifications { get; set; }
        public bool CancelMorningDrive { get; set; }
        public bool CancelAfterDrive { get; set; }
    }

    public class Purchases
    {
        public string TobaccoMember { get; set; }

        public bool Cigar { get; set; }

        public bool Mst { get; set; }

        public bool OTDN { get; set; }

        public bool Cigarette { get; set; }
    }
}