using System;
using System.Collections.Generic;
using itsRewards.ViewModels.Base;

namespace itsRewards.ViewModels
{
	public class VerifyInfoViewModel : BaseViewModel
	{
		public VerifyInfoViewModel()
		{
            BindState();
        }

        #region properties
        private List<string> _stateList;
        public List<string> StateList
        {
            get => _stateList;
            set => SetProperty(ref _stateList, value);
        }
        #endregion

        /// <summary>
        /// Bind static list of States and Number of stores
        /// </summary>
        void BindState()
        {
            // Get static list of States
            StateList = new List<string>()
            {   "AL",
                "AK",
                "AS",
                "AZ",
                "AR",
                "CA",
                "CO",
                "CT",
                "DE",
                "DC",
                "FL",
                "GA",
                "GU",
                "HI",
                "ID",
                "IL",
                "IN",
                "IA",
                "KS",
                "KY",
                "LA",
                "ME",
                "MD",
                "MA",
                "MI",
                "MN",
                "MS",
                "MO",
                "MT",
                "NE",
                "NV",
                "NH",
                "NJ",
                "NM",
                "NY",
                "NC",
                "ND",
                "MP",
                "OH",
                "OK",
                "OR",
                "PA",
                "PR",
                "RI",
                "SC",
                "SD",
                "TN",
                "TX",
                "UT",
                "VT",
                "VA",
                "VI",
                "WA",
                "WV",
                "WI",
                "WY"
            };
        }
    }
}

