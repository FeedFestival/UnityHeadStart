using Assets.HeadStart.Core.SFX;

namespace Assets.HeadStart.Core
{
    public interface ISFX
    {
        void PlayBackgroundMusic(MusicOpts opts);
        void PlaySound(MusicOpts opts);
        void PlaySFX(MusicOpts opts);
    }
}
