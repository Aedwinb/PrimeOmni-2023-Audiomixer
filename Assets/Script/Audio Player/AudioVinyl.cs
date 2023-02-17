using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class AudioVinyl
{
    public Action<float> OnPitchEvent;
    [SerializeField][Range(0,10)]private float maxPitch;
    [SerializeField]private float defaultPitch;
    [SerializeField][Range(0.01f,1)]private float pitchChangeSpeed;
    private float _targetPitch;
    private float _currentPitch;
    public AudioVinyl()
    {
        _currentPitch = defaultPitch;
    }
    public void SetTargetPitch(float newValue)
    {
        _targetPitch = newValue;
    }
    public void SyncPitch()
    {
        if (_currentPitch != _targetPitch)
        {
            _currentPitch = Mathf.Lerp(_currentPitch, _targetPitch, pitchChangeSpeed);
            if(OnPitchEvent!=null)OnPitchEvent(_currentPitch);
        }
    }
}
