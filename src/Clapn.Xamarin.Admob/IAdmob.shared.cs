using Clapn.Xamarin.Admob.RewardedAds;
using System.Collections.Generic;

namespace Clapn.Xamarin.Admob
{
    public interface IAdmob : IRewardedAd, IInterstitialAd
    {
        string AdsId { get; set; }
        bool UserPersonalizedAds { get; set; }

        IList<string> TestDevices { get; set; }

        bool IsInterstitialLoaded();
        void LoadInterstitial(string adUnit);
        void ShowInterstitial();

        bool IsRewardedVideoLoaded();
        void LoadRewardedVideo(RewardedAdOptions options);
        void ShowRewardedVideo();
    }
}
