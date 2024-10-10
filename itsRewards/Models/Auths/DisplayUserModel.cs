/*
 * 04/20/2022 ALI - Added Portal Message to be display to the user
 */
using System.Collections.Generic;
using System.Net;

namespace itsRewards.Models.Auths
{
    //public class DisplayUserModel
    //{
    //    public string Id { get; set; }
    //    public string Email { get; set; }
    //    public string UserName { get; set; }
    //    public string CellPhone { get; set; }
    //    public bool AgeVerified { get; set; }
    //    public string Pin { get; set; }
    //    public List<string> Tocken { get; set; }
    //    public string Address1 { get; set; }
    //    public string Address2 { get; set; }
    //    public string Address3 { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    public string Zip { get; set; }
    //    public Purchases Purchases { get; set; }
    //    public List<Store> Stores { get; set; }
    //    public string PortalMessage { get; set; }
    //}


    public class DisplayUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string CellPhone { get; set; }
        public bool AgeVerified { get; set; }
        public string Pin { get; set; }
        public List<string> Token { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool OptOutEmail { get; set; }
        public bool OptOutNotifications { get; set; }
        public bool CancelMorningDrive { get; set; }
        public bool CancelAfterDrive { get; set; }
        public Purchases Purchases { get; set; }
        public List<Store> Stores { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PortalMessage { get; set; }
    }

}