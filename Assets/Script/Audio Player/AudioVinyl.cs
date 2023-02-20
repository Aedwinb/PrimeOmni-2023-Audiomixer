using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class AudioVinyl
{
    public Action<float> onPitchEvent;
    [SerializeField]private float _defaultPitch;
    [SerializeField][Range(0.01f,1)]private float _pitchChangeSpeed;
    private float _targetPitch;
    private float _currentPitch;
    public AudioVinyl()
    {
        _currentPitch = _defaultPitch;
    }
    public void SetTargetPitch(float newValue)
    {
        _targetPitch = newValue;
    }
    public void SyncPitch()
    {
        if (_currentPitch != _targetPitch)
        {
            _currentPitch = Mathf.Lerp(_currentPitch, _targetPitch, _pitchChangeSpeed);
            if(onPitchEvent!=null)onPitchEvent(_currentPitch);
        }
    }
}
