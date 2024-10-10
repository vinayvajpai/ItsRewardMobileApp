using itsRewards.Models.Base;

namespace itsRewards.Models
{
    public class OfferCategory : BaseModel
    {
        public string Name { get; set; }

        bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }
    }
}