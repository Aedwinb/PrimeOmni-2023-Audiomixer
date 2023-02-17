using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer
{
    private AudioSource _audioSource;
    private SongEntry _selectedSong;
    public Action<float> onVolumeChange;
    public AudioPlayer(SongEntry entry, AudioSource source)
    {
        _selectedSong = entry;
        _audioSource = source;
        _audioSource.clip = entry.songAudioclip;
        _audioSource.loop = true;
        ChangeVolume(0);
        _audioSource.Play();
    }
    public void ChangeVolume(float newVolume)
    {
        _audioSource.volume = newVolume;
        if(onVolumeChange!=null)onVolumeChange(newVolume);
    }
    public void ChangePitch(float newPitch)
    {
        //default pitch is 1
        _audioSource.pitch = newPitch;
    }
    public void ResetPitch()
    {
        _audioSource.pitch = 1;
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public void Play()
    {
        if (_audioSource.time > 0)
        {
            _audioSource.UnPause();
        }
        else
        {
            _audioSource.Play();
        }
    }

    internal float GetPlaybackTime()
    {
        return _audioSource.time;
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    public bool IsPlaying()
    {
        return _audioSource.isPlaying;
    }

    public void Terminate()
    {
        _audioSource.Stop();
    }
    public SongEntry GetSongEntry()
    {
        return _selectedSong;
    }

    public float GetVolume()
    {
        return _audioSource.volume;
    }
}
