using System.Collections.Generic;

namespace Clapn.Xamarin.Admob.RewardedAds
{
    public class RewardedAdOptions
    {
        public RewardedAdOptions(string adUnit)
        {
            AdUnit = adUnit;
        }

        public RewardedAdOptions(string adUnit, string customData, string userId)
           : this(adUnit)
        {
            CustomData = customData;
            UserId = userId;
        }

        public string AdUnit { get; }
        public string CustomData { get; }
        public string UserId { get; }
    }

    public class RewardedAdOptionsEqualityComparer : IEqualityComparer<RewardedAdOptions>
    {
        public bool Equals(RewardedAdOptions x, RewardedAdOptions y)
        {
            return x.AdUnit == y.AdUnit
                && x.CustomData == y.CustomData
                && x.UserId == y.UserId;
        }

        public int GetHashCode(RewardedAdOptions obj)
        {
            return obj.AdUnit.GetHashCode()
                ^ obj.CustomData.GetHashCode()
                ^ obj.UserId.GetHashCode();
        }
    }
}
