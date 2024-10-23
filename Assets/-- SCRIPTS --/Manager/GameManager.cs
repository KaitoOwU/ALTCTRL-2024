using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    [field:SerializeField] public Player Player { get; private set; }
    [field:SerializeField] public float PosY { get; private set; }
    [field:SerializeField] public float PropsSpeed { get; private set; }
    [field:SerializeField] public Transform SpawnPoint { get; private set; } //TEMP
    public GameData GameData { get; private set; }

    public Action onBeat;
    [SerializeField] private SignalReceiver _signals; 
    
    public EInputPrecision InputPrecision
    {
        get
        {
            /*if (InputValue >= GameData.perfectTolerance)
                return EInputPrecision.PERFECT;
            if (InputValue >= GameData.niceTolerance)
                return EInputPrecision.NICE;
            if (InputValue >= GameData.okTolerance)
                return EInputPrecision.OK;*/
            return EInputPrecision.MISSED;
        }
    }
    
    private float _score;

    private int _currentCurve = 0;
    private void Update()
    {
        if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.NOTE_KEY) || Input.GetKeyDown(KeyCode.Space))
        {
            switch (InputPrecision)
            {
                case EInputPrecision.MISSED:
                    Debug.Log($"<b><color=#{Color.red.ToHexString()}> {InputPrecision}</color></b>");
                    break;
                case EInputPrecision.OK:
                    Debug.Log($"<b><color=#{Color.yellow.ToHexString()}> {InputPrecision}</color></b>");
                    _score += 50;
                    break;
                case EInputPrecision.NICE:
                    Debug.Log($"<b><color=#{Color.cyan.ToHexString()}> {InputPrecision}</color></b>");
                    _score += 70;
                    break;
                case EInputPrecision.PERFECT:
                    Debug.Log($"<b><color=#{Color.green.ToHexString()}> {InputPrecision}</color></b>");
                    _score += 100;
                    break;
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        GameData = Resources.Load<GameData>("GameData");

        Instance = this;
        //StartCoroutine(GameLoop());
        StartCoroutine(BeatChecker());
    }

    private IEnumerator BeatChecker()
    {
        while (true)
        {
            
        }
    }

    private void OnValidate()
    {
        foreach (var fixedPose in Editor.FindObjectsByType<GameObject>(FindObjectsSortMode.InstanceID)) if(fixedPose.GetComponent<IFixedPos>() != null)
        {
            fixedPose.transform.position = new Vector3(fixedPose.transform.position.x, PosY, fixedPose.transform.position.z);
        }
    }
}

public interface IFixedPos{}

public enum EInputPrecision
{
    MISSED,
    OK,
    NICE,
    PERFECT
}
