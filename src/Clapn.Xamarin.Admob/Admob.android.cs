using Android.App;
using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using Clapn.Xamarin.Admob.InterstitialAds;
using Clapn.Xamarin.Admob.RewardedAds;
using System;
using System.Collections.Generic;

namespace Clapn.Xamarin.Admob
{
    public class Admob : IAdmob
    {
        public string AdsId { get; set; }
        public bool UserPersonalizedAds { get; set; }
        public IList<string> TestDevices { get; set; }

        private InterstitialAd _interstitialAd;

        private IRewardedVideoAd _rewardedAd;
        private RewardedAdOptions _rewardedAdOptions;
        private readonly RewardedAdOptionsEqualityComparer _rewardedAdOptionsEqualityComparer = new RewardedAdOptionsEqualityComparer();

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

        private void CreateInterstitialAd(string adUnit)
        {
            var context = Application.Context;
            _interstitialAd = new InterstitialAd(context) { AdUnitId = adUnit };
            var interstitialAdListener = new InterstitialAdListener();

            interstitialAdListener.AdLoaded += InterstitialAdListener_AdLoaded;
            interstitialAdListener.AdOpened += InterstitialAdListener_AdOpened;
            interstitialAdListener.AdClosed += InterstitialAdListener_AdClosed;

            _interstitialAd.AdListener = interstitialAdListener;
        }

        public bool IsInterstitialLoaded()
        {
            return _interstitialAd != null && _interstitialAd.IsLoaded;
        }

        public void LoadInterstitial(string adUnit)
        {
            if (_interstitialAd == null || _interstitialAd?.AdUnitId != adUnit)
                CreateInterstitialAd(adUnit);

            if (!_interstitialAd.IsLoaded && !_interstitialAd.IsLoading)
            {
                var requestBuilder = new AdRequest.Builder();

                if (ClapnAdmob.Instance.TestDevices != null)
                {
                    foreach (var testDevice in ClapnAdmob.Instance.TestDevices)
                    {
                        requestBuilder.AddTestDevice(testDevice);
                    }
                }

                _interstitialAd.LoadAd(requestBuilder.Build());
            }
            else
            {
                Console.WriteLine("Interstitial already loaded");
            }
        }

        public void ShowInterstitial()
        {
            if (_interstitialAd != null && _interstitialAd.IsLoaded)
            {
                _interstitialAd.Show();
            }
            else
            {
                Console.WriteLine("Interstitial not loaded");
            }
        }

        private void InterstitialAdListener_AdClosed(object sender, EventArgs e)
        {
            OnInterstitialAdClosed?.Invoke(sender, e);
        }

        private void InterstitialAdListener_AdOpened(object sender, EventArgs e)
        {
            OnInterstitialAdOpened?.Invoke(sender, e);
        }

        private void InterstitialAdListener_AdLoaded(object sender, EventArgs e)
        {
            OnInterstitialAdLoaded?.Invoke(sender, e);
        }

        private void CreateRewardedAd()
        {
            var context = Application.Context;
            _rewardedAd = MobileAds.GetRewardedVideoAdInstance(context);

            var rewardListener = new RewardedAdListener();
            _rewardedAd.RewardedVideoAdListener = rewardListener;

            rewardListener.OnRewardedEvent += RewardListener_OnRewardedEvent;
            rewardListener.OnRewardedVideoAdClosedEvent += RewardListener_OnRewardedAdClosedEvent;
            rewardListener.OnRewardedVideoAdFailedToLoadEvent += RewardListener_OnRewardedAdFailedToLoadEvent;
            rewardListener.OnRewardedVideoAdLeftApplicationEvent += RewardListener_OnRewardedAdLeftApplicationEvent;
            rewardListener.OnRewardedVideoAdLoadedEvent += RewardListener_OnRewardedAdLoadedEvent;
            rewardListener.OnRewardedVideoAdOpenedEvent += RewardListener_OnRewardedAdOpenedEvent;
            rewardListener.OnRewardedVideoStartedEvent += RewardListener_OnRewardedStartedEvent;
            rewardListener.OnRewardedVideoCompletedEvent += RewardListener_OnRewardedCompletedEvent;
        }

        public bool IsRewardedVideoLoaded()
        {
            return _rewardedAd != null && _rewardedAd.IsLoaded;
        }

        public void LoadRewardedVideo(RewardedAdOptions options)
        {
            if (_rewardedAd == null)
            {
                CreateRewardedAd();
            }

            if (!_rewardedAd.IsLoaded && !_rewardedAdOptionsEqualityComparer.Equals(_rewardedAdOptions, options))
            {
                _rewardedAdOptions = options;
                var requestBuilder = new AdRequest.Builder();

                if (ClapnAdmob.Instance.TestDevices != null)
                {
                    foreach (var testDevice in ClapnAdmob.Instance.TestDevices)
                    {
                        requestBuilder.AddTestDevice(testDevice);
                    }
                }

                _rewardedAd.UserId = options?.UserId;
                _rewardedAd.CustomData = options?.CustomData;

                _rewardedAd.LoadAd(options.AdUnit, requestBuilder.Build());
            }
            else
            {
                Console.WriteLine("Rewarded Video already loaded");
            }
        }

        public void ShowRewardedVideo()
        {
            if (_rewardedAd != null && _rewardedAd.IsLoaded)
            {
                _rewardedAd.Show();
            }
            else
            {
                Console.WriteLine("Rewarded Video not loaded");
            }
        }

        private void RewardListener_OnRewardedAdLoadedEvent(object sender, EventArgs e)
        {
            OnRewardedAdLoaded?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedStartedEvent(object sender, EventArgs e)
        {
            OnRewardedStarted?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedAdLeftApplicationEvent(object sender, EventArgs e)
        {
            OnRewardedAdLeftApplication?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedAdFailedToLoadEvent(object sender, RewardedAdEventArgs e)
        {
            OnRewardedAdFailedToLoad?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedAdOpenedEvent(object sender, EventArgs e)
        {
            OnRewardedAdOpened?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedAdClosedEvent(object sender, EventArgs e)
        {
            OnRewardedAdClosed?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedEvent(object sender, RewardedAdEventArgs e)
        {
            OnRewarded?.Invoke(null, e);
        }

        private void RewardListener_OnRewardedCompletedEvent(object sender, EventArgs e)
        {
            OnRewardedAdCompleted?.Invoke(sender, e);
        }
    }
}
