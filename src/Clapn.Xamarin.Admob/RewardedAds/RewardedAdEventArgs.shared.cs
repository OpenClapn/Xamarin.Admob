using System;

namespace Clapn.Xamarin.Admob.RewardedAds
{
    public class RewardedAdEventArgs : EventArgs
    {
        public int? ErrorCode { get; set; }

        public int RewardAmount { get; set; }

        public string RewardType { get; set; }
    }
}
