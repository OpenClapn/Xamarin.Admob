using System.Collections.Generic;

namespace Clapn.Xamarin.Admob.RewardedAds
{
    public class RewardedAdOptions
    {
        public RewardedAdOptions(string customData, string userId)
        {
            CustomData = customData;
            UserId = userId;
        }

        public string CustomData { get; }
        public string UserId { get; }
    }
}
