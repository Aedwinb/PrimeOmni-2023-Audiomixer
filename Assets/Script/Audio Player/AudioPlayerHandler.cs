using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioPlayerHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private AudioPlayer _audioPlayer;
    public Action<AudioPlayer> onAudioChange;
    public void SetAudioEntry(SongEntry entry)
    {
        if (_audioPlayer != null) _audioPlayer.Terminate();
        _audioPlayer = new AudioPlayer(entry, _audioSource);
        onAudioChange(_audioPlayer);
    }
    public void ChangeVolume(float newVolume)
    {
        if (_audioPlayer!=null) { _audioPlayer.ChangeVolume(newVolume); }
    }
    public void SetPitch(float newValue)
    {
        if (_audioPlayer != null) _audioPlayer.ChangePitch(newValue);
    }

    public bool IsPlaying()
    {
        if (_audioPlayer != null)
        {
            return _audioPlayer.IsPlaying();
        }
        else
        {
            return false;
        }
    }

    public float GetPlaybackTime()
    {
        return _audioPlayer.GetPlaybackTime();
    }
}
