namespace Assets.HeadStart.Core.SFX
{
    public class MusicOpts
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.1.1";
#pragma warning restore 0414 //
        public string MusicName;
        public float Volume;
        public bool Loop = true;
        public float? Time;
        public float FadeInSeconds = 0.1f;
        public float FadeOutSeconds;
        public MusicOpts(string musicName)
        {
            MusicName = musicName;
            Volume = 0.7F;
        }
        public MusicOpts(string musicName, float volume)
        {
            MusicName = musicName;
            Volume = volume;
        }
        public MusicOpts(string musicName, float volume = 0.7f, bool loop = true, float? time = null)
        {
            MusicName = musicName;
            Volume = volume;
            Loop = loop;
            Time = time;
        }
        public MusicOpts(string musicName, bool loop = true, float? time = null)
        {
            MusicName = musicName;
            Loop = loop;
            Time = time;
        }
    }
}