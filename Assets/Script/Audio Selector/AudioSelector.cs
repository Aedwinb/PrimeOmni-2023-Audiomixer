using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelector : MonoBehaviour
{
    private SongEntry _selectedEntry;
    [SerializeField] private AudioLibraryHandler _audioLibrary;
    private Coroutine _subscribeRoutine;
    public System.Action<SongEntry> onSelected;
    public System.Action onDeselected;
    private void OnEnable()
    {
        if (_subscribeRoutine != null) StopCoroutine(_subscribeRoutine);
        _subscribeRoutine = StartCoroutine(IE_Subscriber());
    }
    private void OnDisable()
    {
        Unsubscriber();
    }

    public bool HasSelection()
    {
        return _selectedEntry != null;
    }

    public SongEntry GetSelection()
    {
        return _selectedEntry;
    }

    public void ResetSelection()
    {
        ClearSelection();
    }

    private IEnumerator IE_Subscriber()
    {
        yield return new WaitUntil(()=>_audioLibrary.IsSpawnedButtons());
        List<GameObject> buttons = _audioLibrary.GetSpawnedEntries();
        for (int i = 0; i < buttons.Count; i++)
        {
            AudioTrackButton cache = buttons[i].GetComponent<AudioTrackButton>();
            cache.onToggleOn += SelectEntry;
            cache.onToggleOff += DeselectEntry;
        }
    }
    private void Unsubscriber()
    {
        List<GameObject> buttons = _audioLibrary.GetSpawnedEntries();
        for (int i = 0; i < buttons.Count; i++)
        {
            AudioTrackButton cache = buttons[i].GetComponent<AudioTrackButton>();
            cache.onToggleOn -= SelectEntry;
            cache.onToggleOff -= DeselectEntry;
        }
    }
    private void SelectEntry(AudioTrackButton button)
    {
        _selectedEntry = button.GetSongEntry();
        List<GameObject> buttons = _audioLibrary.GetSpawnedEntries();
        foreach (GameObject go in buttons)
        {
            if (go != button.gameObject)
                go.GetComponent<AudioTrackButton>().ToggleOff();
        }
        if(onSelected!=null)onSelected(_selectedEntry);
    }
    private void DeselectEntry(AudioTrackButton button)
    {
        if (_selectedEntry == button.GetSongEntry())
        {
            _selectedEntry = null;
            if (onDeselected != null) onDeselected();
        }
    }
    private void ClearSelection()
    {
        _selectedEntry = null;
        List<GameObject> buttons = _audioLibrary.GetSpawnedEntries();
        foreach (GameObject go in buttons)
        {
            go.GetComponent<AudioTrackButton>().ToggleOff();
        }
    }
}
