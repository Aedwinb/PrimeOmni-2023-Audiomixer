using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibraryHandler : MonoBehaviour
{
    [SerializeField] private AudioLibrary _audioLibrary;
    [SerializeField] private GameObject _audioButtonPrefab;
    [SerializeField] private Transform _audioSpawnParent;
    private List<GameObject> _spawnedObjects;
    private void Start()
    {
        Initiate();
    }
    private void Initiate()
    {
        GameObject cache;
        _spawnedObjects = new List<GameObject>();
        foreach (SongEntry song in _audioLibrary.songEntries)
        {
            cache = Instantiate(_audioButtonPrefab, _audioSpawnParent);
            _spawnedObjects.Add(cache);
            AudioTrackButton button = cache.GetComponent<AudioTrackButton>();
            button.Initiate(song);  
        }
    }

    public bool IsSpawnedButtons()
    {
        if (_spawnedObjects != null)
        {
            return _spawnedObjects.Count > 0;
        }
        else
        {
            return false;
        }
    }

    public List<GameObject> GetSpawnedEntries()
    {
        return _spawnedObjects;
    }
}
