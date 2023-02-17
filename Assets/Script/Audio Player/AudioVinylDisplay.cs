using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVinylDisplay : MonoBehaviour
{
    [SerializeField] private AudioVinylAnimator _vinylAnimator;
    [SerializeField] private AudioVinylHandler _audioVinyl;
    [SerializeField] private Image _vinylCover;
    [SerializeField] private Transform _vinylGroup;
    [SerializeField] private AudioVinylInput _vinylInput;
    [SerializeField] private AudioPlayerHandler _audioPlayer; 
    private static Color EmptyCover = new Color(1,1,1,0);
    private float _initialRotation;
    private bool _audioWasPlaying;
    private void OnEnable()
    {
        _vinylInput.OnRotate += EventRotation;
        _vinylInput.OnInputStart += EventInitialRotation;
        _vinylInput.OnInputStart += _vinylAnimator.Terminate;
        _vinylInput.OnInputStart += SetPreviousState;
        _vinylInput.OnInputEnd += ReturnToPreviousState;
        _audioPlayer.onAudioChange +=StatusCheck;
    }
    private void OnDisable()
    {
        _vinylInput.OnRotate -= EventRotation;
        _vinylInput.OnInputStart -= EventInitialRotation;
        _vinylInput.OnInputStart -= _vinylAnimator.Terminate;
        _vinylInput.OnInputStart -= SetPreviousState;
        _vinylInput.OnInputEnd -= ReturnToPreviousState;
        _audioPlayer.onAudioChange -=StatusCheck;
    }
    public void Pause()
    {
        _vinylAnimator.Terminate();
    }
    public void Play()
    {
        _vinylAnimator.Initiate();
    }
    public void Stop()
    {
        _vinylAnimator.Terminate();
        _vinylGroup.rotation = Quaternion.identity;
    }
    private void SetPreviousState()
    {
        _audioWasPlaying = _audioPlayer.IsPlaying();
    }
    private void ReturnToPreviousState()
    {
        if (_audioWasPlaying)
        {
            _vinylAnimator.Initiate();
        }
        else
        {
            _vinylAnimator.Terminate();
        }
    }
    private void EventRotation(float newRotation)
    {
        SetVinylRotation(_initialRotation + newRotation);
    }
    private void EventInitialRotation()
    {
        _initialRotation = GetVinylRotation();
    }
    private void StatusCheck(AudioPlayer audioPlayer)
    {
        if (audioPlayer.GetSongEntry() != null)
        {
            Initiate();
        }
        else
        {
            Terminate();
        }
    }
    private void Initiate()
    {
        _vinylAnimator.Initiate();
    }
    private void Terminate()
    {
        _vinylAnimator.Terminate();
    }
    public void SetVinylCover(Sprite newSprite)
    {
        if (newSprite == null)
        {
            _vinylCover.sprite = null;
            _vinylCover.color = EmptyCover;
        }
        else
        {
            _vinylCover.sprite = newSprite;
            _vinylCover.color = Color.white;
        }
    }
    public Image GetVinylCover()
    {
        return _vinylCover;
    }

    internal void SetVinylRotation(float newRotation)
    {
        Vector3 currRotation = _vinylGroup.transform.rotation.eulerAngles;
        _vinylGroup.rotation = Quaternion.Euler(currRotation.x,currRotation.y,newRotation);
    }

    internal float GetVinylRotation()
    {
        return _vinylGroup.rotation.eulerAngles.z;
    }
}
