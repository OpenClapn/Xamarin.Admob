using System;
using System.Collections.Generic;
using Clapn.Xamarin.Admob.RewardedAds;
using Foundation;
using Google.MobileAds;
using UIKit;

namespace Clapn.Xamarin.Admob
{
    public class Admob : RewardBasedVideoAdDelegate, IAdmob
    {
        public string AdsId { get; set; }
        public bool UserPersonalizedAds { get; set; }
        public IList<string> TestDevices { get; set; }

        private Interstitial _interstitialAd;

        public event EventHandler<RewardedAdEventArgs> OnRewarded;
        public event EventHandler OnRewardedAdClosed;
        public event EventHandler<RewardedAdEventArgs> OnRewardedAdFailedToLoad;
        public event EventHandler OnRewardedAdLeftApplication;
        public event EventHandler OnRewardedAdLoaded;
        public event EventHandler OnRewardedAdOpened;
        public event EventHandler OnRewardedStarted;
        public event EventHandler OnRewardedAdCompleted;
        public event EventHandler OnInterstitialAdLoaded;
        public event EventHandler OnInterstitialAdOpened;
        public event EventHandler OnInterstitialAdClosed;

        public Admob()
        {
            RewardBasedVideoAd.SharedInstance.Delegate = this;
        }

        private void CreateInterstitialAd(string adUnit)
        {
            try
            {
                if (_interstitialAd != null)
                {
                    _interstitialAd.AdReceived -= AdInterstitial_AdReceived;
                    _interstitialAd.WillPresentScreen -= AdInterstitial_WillPresentScreen;
                    _interstitialAd.WillDismissScreen -= AddInterstitial_WillDismissScreen;
                    _interstitialAd = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _interstitialAd = new Interstitial(adUnit);

            _interstitialAd.AdReceived += AdInterstitial_AdReceived;
            _interstitialAd.WillPresentScreen += AdInterstitial_WillPresentScreen;
            _interstitialAd.WillDismissScreen += AddInterstitial_WillDismissScreen;
        }

        public void LoadInterstitial(string adUnit)
        {
            CreateInterstitialAd(adUnit);

            var request = Request.GetDefaultRequest();
            _interstitialAd.LoadRequest(request);
        }

        public void ShowInterstitial()
        {
            if (_interstitialAd != null && _interstitialAd.IsReady)
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                _interstitialAd.Present(vc);
            }
        }

        public bool IsInterstitialLoaded()
        {
            return _interstitialAd != null && _interstitialAd.IsReady;
        }

        private void AddInterstitial_WillDismissScreen(object sender, EventArgs e)
        {
            OnInterstitialAdClosed?.Invoke(sender, e);
        }

        private void AdInterstitial_WillPresentScreen(object sender, EventArgs e)
        {
            OnInterstitialAdOpened?.Invoke(sender, e);
        }

        private void AdInterstitial_AdReceived(object sender, EventArgs e)
        {
            OnInterstitialAdLoaded?.Invoke(sender, e);
        }

        public bool IsRewardedVideoLoaded()
        {
            return RewardBasedVideoAd.SharedInstance.IsReady;
        }

        public void LoadRewardedVideo(string adUnit)
        {
            if (RewardBasedVideoAd.SharedInstance.IsReady)
            {
                OnRewardedAdLoaded?.Invoke(null, null);
                return;
            }

            

            var request = Request.GetDefaultRequest();
            RewardBasedVideoAd.SharedInstance.LoadRequest(request, adUnit);
        }

        public void ShowRewardedVideo(RewardedAdOptions options)
        {
            if (RewardBasedVideoAd.SharedInstance.IsReady)
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }
                
                RewardBasedVideoAd.SharedInstance.CustomRewardString = options?.CustomData;
                RewardBasedVideoAd.SharedInstance.Present(vc);
            }
        }

        public override void DidRewardUser(RewardBasedVideoAd rewardBasedVideoAd, AdReward reward)
        {
            OnRewarded?.Invoke(rewardBasedVideoAd, new RewardedAdEventArgs() { RewardAmount = (int)reward.Amount, RewardType = reward.Type });
        }

        public override void DidClose(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedAdClosed?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void DidCompletePlaying(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedAdCompleted?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void DidFailToLoad(RewardBasedVideoAd rewardBasedVideoAd, NSError error)
        {
            OnRewardedAdFailedToLoad?.Invoke(rewardBasedVideoAd, new RewardedAdEventArgs() { ErrorCode = (int)error.Code });
        }

        public override void DidOpen(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedAdOpened?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void DidReceiveAd(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedAdLoaded?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void DidStartPlaying(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedStarted?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void WillLeaveApplication(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedAdLeftApplication?.Invoke(rewardBasedVideoAd, new EventArgs());
        }
    }
}
