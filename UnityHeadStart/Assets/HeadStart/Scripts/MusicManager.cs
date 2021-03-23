using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "1.0.3";
#pragma warning restore 0414 //
    private static MusicManager _instance;
    public static MusicManager _ { get { return _instance; } }

    void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }

    private Dictionary<string, MAudio> Sounds;
    public AudioClip MainMenuMusic;
    public AudioClip ClickSound;

    public void Init()
    {
        Sounds = new Dictionary<string, MAudio>()
        {
            { "MainMenuMusic", new MAudio() { AudioClip = MainMenuMusic } },
            { "Click", new MAudio() { AudioClip = ClickSound }
            }
        };
    }

    private int? _backgroundMusicId;
    private string _backgroundMusic;
    private int? _ambientMusicId;
    private string _ambientMusic;

    private int? _soundId;
    private string _sound;

    public void PlayBackgroundMusic(string musicName, bool loop = true, float? time = null)
    {
        Audio audio;
        if (_backgroundMusicId.HasValue)
        {
            audio = EazySoundManager.GetAudio(_backgroundMusicId.Value);
            if (audio == null)
            {
                Debug.LogError("audio has a problem");
                return;
            }
            audio.Stop();
        }
        _backgroundMusicId = EazySoundManager.PrepareMusic(Sounds[musicName].AudioClip, 0.7f, loop, false);
        _backgroundMusic = musicName;
        audio = EazySoundManager.GetAudio(_backgroundMusicId.Value);
        audio.Play();
        if (time.HasValue)
        {
            audio.AudioSource.time = time.Value;
        }
    }

    public void PlayAmbient(string musicName, bool loop = true)
    {
        ClearAmbientQueue();
        _ambientMusic = musicName;
        if (Sounds[_ambientMusic].AudioClip != null)
        {
            PlayAmbientAudio(Sounds[musicName], loop);
        }
        else
        {
            StartCoroutine(PlayAllAmbientAudio(Sounds[musicName], loop));
        }
    }

    private void ClearAmbientQueue()
    {
        if (_ambientMusicId.HasValue)
        {
            Audio audio = null;
            audio = EazySoundManager.GetAudio(_ambientMusicId.Value);
            if (audio == null)
            {
                Debug.LogError("audio has a problem");
                return;
            }
            audio.Stop();
            audio = null;
        }
    }

    private Audio PlayAmbientAudio(MAudio mAudio, bool loop, bool loadMany = false)
    {
        ClearAmbientQueue();
        _ambientMusicId = EazySoundManager.PrepareMusic(
            loadMany ? mAudio.AudioClips[mAudio.GetRandomIndex()] : mAudio.AudioClip,
            0.2f,
            loop,
            false
            );
        Audio audio = EazySoundManager.GetAudio(_ambientMusicId.Value);
        audio.Play();
        return audio;
    }

    private IEnumerator PlayAllAmbientAudio(MAudio mAudio, bool loop)
    {
        var audio = PlayAmbientAudio(mAudio, loop, true);
        var time = audio.AudioSource.clip.length;

        yield return new WaitForSeconds(time);

        StartCoroutine(PlayAllAmbientAudio(mAudio, loop));
    }

    public void PlayRequiredBackgroundMusic(string musicName, bool loop = true, float? time = null)
    {
        if (_backgroundMusic == musicName)
        {
            return;
        }

        PlayBackgroundMusic(musicName, loop, time);
    }

    public void PlayRequiredAmbient(string musicName, bool loop = false)
    {
        if (_ambientMusic == musicName)
        {
            return;
        }

        PlayAmbient(musicName, loop);
    }

    public void PlaySound(string soundName, bool loop = false)
    {
        _sound = soundName;
        if (Sounds[_sound].AudioClip != null)
        {
            PlayAmbientAudio(Sounds[soundName], loop);
        }
    }

    private void PlaySoundAudio(MAudio mAudio, bool loop)
    {
        _soundId = EazySoundManager.PrepareMusic(
            mAudio.AudioClip,
            0.2f,
            loop,
            false
            );
        Audio audio = EazySoundManager.GetAudio(_soundId.Value);
        audio.Play();
    }
}

public class MAudio
{
    public AudioClip AudioClip;
    public AudioClip[] AudioClips;

    public int GetRandomIndex()
    {
        if (AudioClips != null && AudioClips.Length > 0)
        {
            return (int)Mathf.Ceil(Random.Range(0, AudioClips.Length));
        }
        return 0;
    }
}
