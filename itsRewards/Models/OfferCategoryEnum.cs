using System;
using System.ComponentModel;

namespace itsRewards.Models
{
    public enum OfferCategoryEnum
    {
        [Description("All Offers")]
        AllOffers,
        [Description("Cigarette Offers")]
        CigaretteOffers,
        [Description("Smokeless Offers")]
        SmokelessOffers,
        [Description("Cigar Offers")]
        CigarOffers,
        [Description("E-Vapor Offers")]
        EVaporOffers
    }
}