using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraPositionChanger : MonoBehaviour
{
    public static CameraPositionChanger instance;

    Camera _cam ;
    public Dictionary<CameraPosition, Vector3> _posDict;
    public Dictionary<CameraPosition, float> _zoomDict;

    [Header("Camera Positions")]
    [SerializeField] private Vector3 _posOne;
    [SerializeField] private Vector3 _posTwo;
    [SerializeField] private Vector3 _posThree;
    [SerializeField] private Vector3 _posFour;
    [SerializeField] private Vector3 _posFive;
    [SerializeField] private Vector3 _posWin;

    [Header("Camera Zooms")]
    [SerializeField] private float _zoomOne;
    [SerializeField] private float _zoomTwo;
    [SerializeField] private float _zoomThree;
    [SerializeField] private float _zoomFour;
    [SerializeField] private float _zoomFive;
    [SerializeField] private float _zoomWin;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _cam = Camera.main;

        _posDict = new()
        {
            { CameraPosition.FIRST, _posOne },
            { CameraPosition.SECOND, _posTwo },
            { CameraPosition.THIRD, _posThree },
            { CameraPosition.FOURTH, _posFour },
            { CameraPosition.FIFTH, _posFive },
            { CameraPosition.WIN, _posWin },
        };

        _zoomDict = new()
        {
            { CameraPosition.FIRST, _zoomOne },
            { CameraPosition.SECOND, _zoomTwo },
            { CameraPosition.THIRD, _zoomThree },
            { CameraPosition.FOURTH, _zoomFour },
            { CameraPosition.FIFTH, _zoomFive },
            { CameraPosition.WIN, _zoomWin }
        };
    }

    [Serializable]
    public enum CameraPosition
    {
        FIRST = 0,
        SECOND = 1,
        THIRD = 2,
        FOURTH = 3,
        FIFTH = 4,
        WIN = 5
    }

    public void ChangeCameraPosition(CameraPosition position)
    {
        _cam.gameObject.transform.DOMove(_posDict[position], 0.4f).SetEase(Ease.OutElastic, 2f, 0.8f);
        _cam.DOOrthoSize(_zoomDict[position], 0.5f).SetEase(Ease.OutElastic, 1.6f, 0.8f);
    }

    private void Update()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeCameraPosition(CameraPosition.FIRST);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeCameraPosition(CameraPosition.SECOND);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeCameraPosition(CameraPosition.THIRD);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeCameraPosition(CameraPosition.FOURTH);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeCameraPosition(CameraPosition.FIFTH);
        }
    }
#endif
}
