using System;

namespace Clapn.Xamarin.Admob
{
    public interface IInterstitialAd
    {
        event EventHandler OnInterstitialAdLoaded;
        event EventHandler OnInterstitialAdOpened;
        event EventHandler OnInterstitialAdClosed;
    }
}
