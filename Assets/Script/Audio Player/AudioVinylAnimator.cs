using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVinylAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _vinylRecord;
    [SerializeField] private Animator _pinAnimator;
    private Coroutine _rotateRoutine;
    public void Initiate()
    {
        if (_rotateRoutine != null) StopCoroutine(_rotateRoutine);
        _rotateRoutine = StartCoroutine(IE_Rotate());
        _pinAnimator.SetBool("isPlaying",true);
    }
    public void Terminate()
    {
        if(_rotateRoutine!=null)StopCoroutine(_rotateRoutine);
        _pinAnimator.SetBool("isPlaying", false);
    }
    private IEnumerator IE_Rotate()
    {
        while (true)
        {
            _vinylRecord.transform.Rotate(Vector3.forward);
            yield return null;
        }
    }
}
