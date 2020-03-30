using Android.Gms.Ads.Reward;
using System;

namespace Clapn.Xamarin.Admob.RewardedAds
{
    public class RewardedAdListener : Java.Lang.Object, IRewardedVideoAdListener
    {
        public event EventHandler<RewardedAdEventArgs> OnRewardedEvent;
        public event EventHandler OnRewardedVideoAdClosedEvent;
        public event EventHandler<RewardedAdEventArgs> OnRewardedVideoAdFailedToLoadEvent;
        public event EventHandler OnRewardedVideoAdLeftApplicationEvent;
        public event EventHandler OnRewardedVideoAdLoadedEvent;
        public event EventHandler OnRewardedVideoAdOpenedEvent;
        public event EventHandler OnRewardedVideoStartedEvent;
        public event EventHandler OnRewardedVideoCompletedEvent;

        public void OnRewarded(IRewardItem reward)
        {
            OnRewardedEvent?.Invoke(null, new RewardedAdEventArgs() { RewardAmount = reward.Amount, RewardType = reward.Type });
        }

        public void OnRewardedVideoAdClosed()
        {
            OnRewardedVideoAdClosedEvent?.Invoke(null, null);
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            OnRewardedVideoAdFailedToLoadEvent?.Invoke(null, new RewardedAdEventArgs() { ErrorCode = errorCode });
        }

        public void OnRewardedVideoAdLeftApplication()
        {
            OnRewardedVideoAdLeftApplicationEvent?.Invoke(null, null);
        }

        public void OnRewardedVideoAdLoaded()
        {
            OnRewardedVideoAdLoadedEvent?.Invoke(null, null);
        }

        public void OnRewardedVideoAdOpened()
        {
            OnRewardedVideoAdOpenedEvent?.Invoke(null, null);
        }

        public void OnRewardedVideoStarted()
        {
            OnRewardedVideoStartedEvent?.Invoke(null, null);
        }

        public void OnRewardedVideoCompleted()
        {
            OnRewardedVideoCompletedEvent?.Invoke(null, null);
        }
    }
}
