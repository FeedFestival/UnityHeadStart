namespace Assets.HeadStart.Core.SFX
{
    public class MusicOpts
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
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
        }
        public MusicOpts(string musicName, float volume = 0.7f)
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