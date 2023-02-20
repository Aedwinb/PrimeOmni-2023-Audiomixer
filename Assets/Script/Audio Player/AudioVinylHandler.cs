using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVinylHandler : MonoBehaviour
{
    [SerializeField] AudioPlayerHandler _audioPlayer;
    [SerializeField]private AudioVinyl _audioVinyl;
    private void OnEnable()
    {
        _audioVinyl.onPitchEvent += _audioPlayer.SetPitch;
    }
    private void OnDisable()
    {
        _audioVinyl.onPitchEvent -= _audioPlayer.SetPitch;
    }
    private void Update()
    {
        if (_audioVinyl != null) _audioVinyl.SyncPitch();
    }
    public void ChangeTargetPitch(float newPitch)
    {
        _audioVinyl.SetTargetPitch(newPitch);
    }
}
