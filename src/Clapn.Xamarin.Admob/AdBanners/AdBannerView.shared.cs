using System;
using Xamarin.Forms;

namespace Clapn.Xamarin.Admob.AdBanners
{
    public class AdBannerView : View
    {
        public event EventHandler AdsClicked;
        public event EventHandler AdsClosed;
        public event EventHandler AdsImpression;
        public event EventHandler AdsOpened;

        public static readonly BindableProperty AdsIdProperty = BindableProperty.Create("AdsId", typeof(string), typeof(AdBannerView));

        public string AdsId
        {
            get => (string)GetValue(AdsIdProperty);
            set => SetValue(AdsIdProperty, value);
        }

        public static readonly BindableProperty PersonalizedAdsProperty = BindableProperty.Create("PersonalizedAds", typeof(bool), typeof(AdBannerView));

        public bool? PersonalizedAds
        {
            get => (bool?)GetValue(PersonalizedAdsProperty);
            set => SetValue(PersonalizedAdsProperty, value);
        }

        internal void AdClicked(object sender, EventArgs e)
        {
            AdsClicked?.Invoke(sender, e);
        }

        internal void AdClosed(object sender, EventArgs e)
        {
            AdsClosed?.Invoke(sender, e);
        }

        internal void AdImpression(object sender, EventArgs e)
        {
            AdsImpression?.Invoke(sender, e);
        }

        internal void AdOpened(object sender, EventArgs e)
        {
            AdsOpened?.Invoke(sender, e);
        }
    }
}
