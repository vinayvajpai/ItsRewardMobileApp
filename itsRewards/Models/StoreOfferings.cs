using System;
using System.Collections.Generic;

namespace itsRewards.Models
{
    /// <summary>
    /// Setting for the the store for ItsRewards
    /// </summary>
    public class StoreOfferings : Store
    {
        public string[] Promotions { get; set; }

        public string[] Loyalty { get; set; }

        /// <summary>
        /// This is the configuarable offerings via each store
        /// </summary>
        // all tobacco offerings,
        // This will be  tobacco, cigeratte, smoleless
        // this will will represent the the header...
        public string[] OfferingNames { get; set; }

        /// <summary>
        /// This is the offering specifics
        /// </summary>
        public List<Offer> Offerings { get; set; }

        /// <summary></summary>
        public string AccountNumber { get; set; }

    }
}
