using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FT_GameClips
{
    Timer,
    ClickSound,
    Exit,
    Loading,
    Win,
    Loose,
    Take,
    SpeenWheelRotate,
    WheelStart,
    WheelEnd,
}

[System.Serializable]
public class FT_GameAudioClipsInfo
{
    public FT_GameClips fT_GameClips;
    public AudioClip audioClip;
}

public class FT_SoundManager : MonoBehaviour
{
    public static FT_SoundManager instance;

    public AudioSource ft_AudioSorce;
    public AudioSource timerAudio;
    
    public List<FT_GameAudioClipsInfo> fT_GameAudioClipsInfo = new();

    private void Awake()
    {
        instance = this;
    }

    public AudioClip GetAudioClip(FT_GameClips clip)
    {
        for (int i = 0; i < fT_GameAudioClipsInfo.Count; i++)
        {
            if (fT_GameAudioClipsInfo[i].fT_GameClips == clip) return fT_GameAudioClipsInfo[i].audioClip;
        }

        return null;
    }

    public void PlayAudioClip(FT_GameClips clip, bool loop = false)
    {
        ft_AudioSorce.Stop();
        AudioClip audioClip = GetAudioClip(clip);
        ft_AudioSorce.clip = audioClip;
        ft_AudioSorce.loop = loop;
        ft_AudioSorce.PlayOneShot(audioClip);
    }
}
