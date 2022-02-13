using Assets.HeadStart.Core.SFX;

namespace Assets.HeadStart.Core
{
    public interface ISFX
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.7";
#pragma warning restore 0414 //
        void PlayBackgroundMusic(MusicOpts opts);
        void PlaySound(MusicOpts opts);
        void PlaySFX(MusicOpts opts);
    }
}
