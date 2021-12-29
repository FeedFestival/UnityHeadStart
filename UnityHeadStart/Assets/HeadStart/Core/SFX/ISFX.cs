public interface ISFX
{
    void PlayBackgroundMusic(string musicName, bool loop = true, float? time = null);
    void PlaySound(string soundName, bool loop = false);
}
