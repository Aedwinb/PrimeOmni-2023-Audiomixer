using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AudioPlayerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _songTitle;
    [SerializeField] private TextMeshProUGUI _songArtist;
    [SerializeField] private AudioVinylDisplay _vinylDisplay;
    [SerializeField] private Slider _songProgress;
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonPause;
    [SerializeField] private Button _buttonStop;
    [SerializeField] private AudioPlayerHandler _audioPlayer;
    [SerializeField] private Slider _songVolume;
    private void OnEnable()
    {
        _audioPlayer.onAudioChange += SetDisplay;
    }
    private void OnDisable()
    {
        _audioPlayer.onAudioChange -= SetDisplay;
    }

    public void SetDisplay(AudioPlayer player)
    {
        ResetValues();
        SongEntry entryCache = player.GetSongEntry();
        _songArtist.text = entryCache.songArtist;
        _songTitle.text = entryCache.songTitle;
        _vinylDisplay.SetVinylCover(entryCache.songImage);
        _songProgress.maxValue = entryCache.songAudioclip.length;
        _songProgress.value = 0;
        _buttonPause.onClick.AddListener(player.Pause);
        _buttonPause.onClick.AddListener(_vinylDisplay.Pause);
        _buttonPlay.onClick.AddListener(player.Play);
        _buttonPlay.onClick.AddListener(_vinylDisplay.Play);
        _buttonStop.onClick.AddListener(player.Stop);
        _buttonStop.onClick.AddListener(_vinylDisplay.Stop);
        _songVolume.onValueChanged.AddListener(player.ChangeVolume);
        _songVolume.value = player.GetVolume();
    }
    private void Update()
    {
        if (_audioPlayer.IsPlaying()) _songProgress.value = _audioPlayer.GetPlaybackTime();
    }
    private void ResetValues()
    {
        _buttonPause.onClick.RemoveAllListeners();
        _buttonPlay.onClick.RemoveAllListeners();
        _buttonStop.onClick.RemoveAllListeners();
        _songVolume.onValueChanged.RemoveAllListeners();
    }
}
