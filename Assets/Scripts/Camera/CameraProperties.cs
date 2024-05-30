using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraProperties : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private float _zoomSpeed = 2f;
    private float _minZoom = 1f;
    private float _maxZoom = 20f;
    private float _currentZoom;
    private float _newZoom;
    private float _zoomInput;

    public void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()//TODO: Optimaze
    {
        _zoomInput = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(_zoomInput);
    }

    void ZoomCamera(float increment)
    {
        _currentZoom = _virtualCamera.m_Lens.OrthographicSize;
        _newZoom = Mathf.Clamp(_currentZoom - increment * _zoomSpeed, _minZoom, _maxZoom);
        _virtualCamera.m_Lens.OrthographicSize = _newZoom;
    }
}