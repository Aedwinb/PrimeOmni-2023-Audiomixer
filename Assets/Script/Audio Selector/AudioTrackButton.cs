using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class AudioTrackButton : MonoBehaviour
{
    [SerializeField] private Image _albumCover;
    [SerializeField] private TextMeshProUGUI _songArtist;
    [SerializeField] private TextMeshProUGUI _songTitle;
    [SerializeField] private Button _button;
    [SerializeField] private Color _toggleOn, _toggleOff;
    [SerializeField] private Image _backgroundImage;
    private SongEntry _songEntry;
    public Action<AudioTrackButton> onToggleOn;
    public Action<AudioTrackButton> onToggleOff;
    private bool _toggle;
    public void Initiate(SongEntry entry)
    {
        _songEntry = entry;
        _albumCover.sprite = entry.songImage;
        _songArtist.text = entry.songArtist;
        _songTitle.text = entry.songTitle;
        _backgroundImage.color = _toggleOff;
        _button.onClick.AddListener(ToggleButton);
    }
    public void Terminate()
    {
        _button.onClick.RemoveListener(ToggleButton);
    }
    public void ToggleButton()
    {
        if (_toggle)
        {
            ToggleOff();
        }
        else
        {
            ToggleOn();
        }
    }
    public void ToggleOff()
    {
        _toggle = false;
        _backgroundImage.color = _toggleOff;
        if (onToggleOff != null) onToggleOff(this);
    }
    public void ToggleOn()
    {
        _toggle = true;
        _backgroundImage.color = _toggleOn;
        if (onToggleOn != null) onToggleOn(this);
    }
    public SongEntry GetSongEntry()
    {
        return _songEntry;
    }
}
