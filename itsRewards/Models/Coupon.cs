using System;
using System.Linq;
using itsRewards.LocalDataBase;
using itsRewards.Models.Base;
using itsRewards.ViewModels.Base;

namespace itsRewards.Models
{
    public class Coupon : BaseModel
    {
        public string Name { get; set; }
        bool isAddToWallet = false;
        public bool IsAddToWallet
        {
            get
            {
                return isAddToWallet;
            }
            set
            {
                isAddToWallet = value;
                OnPropertyChanged("IsAddToWallet");
                OnPropertyChanged("ActionSymbol");
            }
        }

        public string ActionSymbol
        {
            get
            {
                return isAddToWallet == true ? "--" : "+";
            }
        }

        
    }

    public class CouponCategory : BaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }
    }
}


