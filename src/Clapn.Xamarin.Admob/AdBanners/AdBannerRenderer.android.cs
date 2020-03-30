using System;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Widget;
using Clapn.Xamarin.Admob.AdBanners;
using Google.Ads.Mediation.Admob;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

    [assembly: ExportRenderer(typeof(AdBannerView), typeof(AdViewRenderer))]

namespace Clapn.Xamarin.Admob.AdBanners
{
    public class AdViewRenderer : ViewRenderer<AdBannerView, AdView>
    {
        string _adUnitId = string.Empty;
        readonly AdSize _adSize = AdSize.SmartBanner;
        AdView _adView;

        public AdViewRenderer(Context context) : base(context)
        {
        }

        private void CreateNativeControl(AdBannerView myMtAdView, string adsId, bool? personalizedAds)
        {
            if (_adView != null)
                return;

            _adUnitId = !string.IsNullOrEmpty(adsId) ? adsId : ClapnAdmob.Instance.AdsId;

            if (string.IsNullOrEmpty(_adUnitId))
            {
                Console.WriteLine("You must set the adsID before using it");
            }

            var listener = new MyAdBannerListener();

            listener.AdClicked += myMtAdView.AdClicked;
            listener.AdClosed += myMtAdView.AdClosed;
            listener.AdImpression += myMtAdView.AdImpression;
            listener.AdOpened += myMtAdView.AdOpened;

            _adView = new AdView(Context)
            {
                AdSize = _adSize,
                AdUnitId = _adUnitId,
                AdListener = listener,
                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
            };

            var requestBuilder = new AdRequest.Builder();
            if (ClapnAdmob.Instance.TestDevices != null)
            {
                foreach (var testDevice in ClapnAdmob.Instance.TestDevices)
                {
                    requestBuilder.AddTestDevice(testDevice);
                }
            }

            if ((personalizedAds.HasValue && personalizedAds.Value) || ClapnAdmob.Instance.UserPersonalizedAds)
            {
                _adView.LoadAd(requestBuilder.Build());
            }
            else
            {
                Bundle bundleExtra = new Bundle();
                bundleExtra.PutString("npa", "1");

                _adView.LoadAd(requestBuilder
                    .AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), bundleExtra)
                    .Build());
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdBannerView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeControl(e.NewElement, e.NewElement.AdsId, e.NewElement.PersonalizedAds);
                SetNativeControl(_adView);
            }
        }
    }
}
