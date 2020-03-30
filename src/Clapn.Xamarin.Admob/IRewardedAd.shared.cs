using Clapn.Xamarin.Admob.RewardedAds;
using System;

namespace Clapn.Xamarin.Admob
{
    public interface IRewardedAd
    {
        event EventHandler<RewardedAdEventArgs> OnRewarded;
        event EventHandler OnRewardedAdClosed;
        event EventHandler<RewardedAdEventArgs> OnRewardedAdFailedToLoad;
        event EventHandler OnRewardedAdLeftApplication;
        event EventHandler OnRewardedAdLoaded;
        event EventHandler OnRewardedAdOpened;
        event EventHandler OnRewardedStarted;
        event EventHandler OnRewardedAdCompleted;
    }
}
