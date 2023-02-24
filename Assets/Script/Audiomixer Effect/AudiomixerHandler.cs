using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudiomixerHandler : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot _targetSnapshot;
    [SerializeField] private Button _triggerButton;
    private void OnEnable()
    {
        _triggerButton.onClick.AddListener(ApplySnapshot);
    }
    private void OnDisable()
    {
        _triggerButton.onClick.RemoveListener(ApplySnapshot);
    }
    private void ApplySnapshot()
    {
        Debug.Log("Applying snapshot: "+_targetSnapshot.name);
        _targetSnapshot.TransitionTo(0.5f);
    }
}
