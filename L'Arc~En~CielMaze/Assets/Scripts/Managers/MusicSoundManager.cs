using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoundManager : MonoBehaviour
{
    public AudioClip[] sfxClips, musicClips;
    public AudioSource sfxSource, musicSource;

    private void Awake()
    {
        CodeControl.Message.AddListener<PlaySFXRequestEvent>(OnPlaySFXRequested);
        CodeControl.Message.AddListener<PlayMusicRequestEvent>(OnPlayMusicRequested);
    }

    private void OnPlayMusicRequested(PlayMusicRequestEvent obj)
    {
        musicSource.clip = FindSongByName(obj.songName);
        musicSource.loop = true;
        musicSource.Play();
    }

    private void OnPlaySFXRequested(PlaySFXRequestEvent obj)
    {
        sfxSource.PlayOneShot(FindSFXClipByName(obj.sfxName), obj.volume);
    }

    private AudioClip FindSFXClipByName(string name)
    {
        AudioClip clip = null;
        foreach (AudioClip audioClip in sfxClips)
        {
            if (audioClip.name.ToLower().Contains(name.ToLower()))
            {
                clip = audioClip;
                break;
            }
        }
        return clip;
    }

    private AudioClip FindSongByName(string name)
    {
        AudioClip clip = null;
        foreach (AudioClip audioClip in musicClips)
        {
            if (audioClip.name.ToLower().Contains(name.ToLower()))
            {
                clip = audioClip;
                break;
            }
        }
        return clip;
    }
}
