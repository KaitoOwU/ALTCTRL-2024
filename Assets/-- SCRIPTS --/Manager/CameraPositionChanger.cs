using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraPositionChanger : MonoBehaviour
{
    Camera _cam ;
    Dictionary<CameraPosition, Vector3> _posDict;
    Dictionary<CameraPosition, float> _zoomDict;

    [Header("Camera Positions")]
    [SerializeField] private Vector3 _nearPosition;
    [SerializeField] private Vector3 _normalPosition;
    [SerializeField] private Vector3 _farPosition;

    [Header("Camera Zooms")]
    [SerializeField] private float _nearZoom;
    [SerializeField] private float _normalZoom;
    [SerializeField] private float _farZoom;

    private void Start()
    {
        _cam = Camera.main;

        _posDict = new()
        {
            { CameraPosition.NEAR, _nearPosition },
            { CameraPosition.NORMAL, _normalPosition },
            { CameraPosition.FAR, _farPosition }
        };

        _zoomDict = new()
        {
            { CameraPosition.NEAR, _nearZoom },
            { CameraPosition.NORMAL, _normalZoom },
            { CameraPosition.FAR, _farZoom }
        };
    }

    [Serializable]
    public enum CameraPosition
    {
        NEAR,
        NORMAL,
        FAR
    }

    public void ChangeCameraPosition(CameraPosition position)
    {
        switch (position)
        {
            case CameraPosition.NEAR:
                _cam.gameObject.transform.DOMove(_posDict[CameraPosition.NEAR], 0.5f);
                _cam.DOOrthoSize(_zoomDict[CameraPosition.NEAR], 0.5f);
                break;
            case CameraPosition.NORMAL:
                _cam.gameObject.transform.DOMove(_posDict[CameraPosition.NORMAL], 0.5f);
                _cam.DOOrthoSize(_zoomDict[CameraPosition.NORMAL], 0.5f);
                break;
            case CameraPosition.FAR:
                _cam.gameObject.transform.DOMove(_posDict[CameraPosition.FAR], 0.5f);
                _cam.DOOrthoSize(_zoomDict[CameraPosition.FAR], 0.5f);
                break;
        }
    }

    private void Update()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeCameraPosition(CameraPosition.NEAR);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeCameraPosition(CameraPosition.NORMAL);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeCameraPosition(CameraPosition.FAR);
        }
    }
#endif
}
