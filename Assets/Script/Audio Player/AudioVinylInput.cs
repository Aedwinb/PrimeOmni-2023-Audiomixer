using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.EventSystems;
public class AudioVinylInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerExitHandler, IPointerUpHandler, IEndDragHandler
{
    //[SerializeField] private AudioVinylDisplay _vinylDisplay;
    [SerializeField] private AudioVinylHandler _audioVinyl;
    [SerializeField] private Camera _eventCamera;
    [SerializeField] private float _angleToPitch;
    private float _initialRotation;
    private Vector2 _initialVector;
    private Vector2 _lastVector;
    private Vector2 _currentVector;
    private bool _isTouching;
    public Action OnInputStart;
    public Action<float> OnRotate;
    public Action OnInputEnd;
    public void OnDrag(PointerEventData eventData)
    {
        DragCheck(eventData);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _isTouching = false;
        OnInputEnd();
    }
    private void DragCheck(PointerEventData eventData)
    {
        
        _currentVector = eventData.position - GetOriginPoint();
    }
    private void Update()
    {
        if (_isTouching)
        {
            float angle = Vector2.SignedAngle(_initialVector, _currentVector);
            OnRotate(angle);
            _audioVinyl.ChangeTargetPitch(_angleToPitch * (Vector2.SignedAngle(_lastVector, _currentVector)));
            _lastVector = _currentVector;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnInputStart();
        _audioVinyl.ChangeTargetPitch(0);
        _initialVector =  eventData.position - GetOriginPoint();
        _lastVector = _initialVector;
        _currentVector = _initialVector;
        _isTouching = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _audioVinyl.ChangeTargetPitch(1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _audioVinyl.ChangeTargetPitch(1);
        if (_isTouching)
        {
            _isTouching = false;
            OnInputEnd();
        }
    }
    private Vector2 Vector2ize(Vector3 data)
    {
        return new Vector2(data.x,data.y);
    }
    private Vector2 GetOriginPoint()
    {
        return Vector2ize(_eventCamera.WorldToScreenPoint(transform.position));
    }

}
