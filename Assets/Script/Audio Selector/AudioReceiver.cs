using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioReceiver : MonoBehaviour
{
    [SerializeField] private AudioPlayerHandler _audioPlayer;
    [SerializeField] private AudioSelector _audioSelector;
    [SerializeField] private Button _receiveButton;
    private void OnEnable()
    {
        _receiveButton.onClick.AddListener(ReceiveSelection);
    }
    private void OnDisable()
    {
        _receiveButton.onClick.RemoveListener(ReceiveSelection);
    }
    private void ReceiveSelection()
    {
        if (_audioSelector.HasSelection())
        {
            _audioPlayer.SetAudioEntry(_audioSelector.GetSelection());
            _audioSelector.ResetSelection();
        }
    }
}
