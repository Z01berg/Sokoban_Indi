using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraProperties : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private float _zoomSpeed = 2f;
    private float _minZoom = 0f;
    private float _maxZoom = 20f;

    public void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(zoomInput);
    }

    void ZoomCamera(float increment)
    {
        float currentZoom = _virtualCamera.m_Lens.OrthographicSize;
        float newZoom = Mathf.Clamp(currentZoom - increment * _zoomSpeed, _minZoom, _maxZoom);
        _virtualCamera.m_Lens.OrthographicSize = newZoom;
    }
}