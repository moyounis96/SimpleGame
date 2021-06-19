using System;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;
    
    [HideInInspector] public AudioSource musicSource;
    [HideInInspector] public AudioSource sfxSource;
    private void Awake()
    {
        Instance = this;
        Array.Sort(sounds);
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        sfxSource = transform.GetChild(1).GetComponent<AudioSource>();
        musicSource.volume = PlayerPrefs.GetFloat(Constants.MUSIC_VOLUME_PREFS, 0.5f);
        sfxSource.volume = PlayerPrefs.GetFloat(Constants.SFX_VOLUME_PREFS, 0.5f);
        PlayMusic("LoginMusic");
    }
    public void PlayMusic(string clipName, float volume = -1)
    {
        if (volume == -1) volume = musicSource.volume;
        AudioClip clip = GetClip(clipName);
        if (clip != null) 
            PlayMusic(clip, volume);
    }
    public void PlayMusic(AudioClip clip, float volume)
    {
        if(musicSource.volume  > volume)
            musicSource.volume = volume;
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void PlayLoopedEffect(string clipName)
    {
        AudioClip clip = GetClip(clipName);
        if(clip!= null)
            PlayLoopedEffect(GetClip(clipName));
    }
    public void PlayLoopedEffect(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }
    public void StopLoopedEffect()
    {
        sfxSource.Stop();
    }
    public void PlayEffect(string clipName, float pitch = 1, float volume = 1)
    {
        AudioClip clip = GetClip(clipName);
        if(clip != null)
            PlayEffect(clip, pitch, volume);
    }
    public void PlayEffect(AudioClip clip, float pitch = 1, float volume = 1)
    {
        sfxSource.pitch = pitch;
        sfxSource.PlayOneShot(clip, volume);
    }
    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(Constants.SFX_VOLUME_PREFS, volume);
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(Constants.MUSIC_VOLUME_PREFS, volume);
    }
    AudioClip GetClip(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;
        int index = Array.BinarySearch<Sound>(sounds, new Sound(name));
        return index == -1 ? null : sounds[index].clip;
    }
}
[Serializable]
public class Sound : IComparable
{
    public string name;
    public AudioClip clip;
    public Sound(string name, AudioClip clip = null)
    {
        this.name = name;
        this.clip = clip;
    }
    public int CompareTo(object obj)
    {
        Sound other = obj as Sound;
        if (other == null) return 1;
        return string.Compare(name, other.name);
    }
}