using System;

namespace Clapn.Xamarin.Admob
{
    public static class ClapnAdmob
    {
        static readonly Lazy<IAdmob> _instance = new Lazy<IAdmob>(Create, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static bool IsSupported => _instance.Value != null;

        public static IAdmob Instance
        {
            get
            {
                IAdmob ret = _instance.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }

                return ret;
            }
        }

        static IAdmob Create()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#else
            return new Admob();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException(
                "This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
}
