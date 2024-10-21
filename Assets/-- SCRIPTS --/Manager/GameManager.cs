using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    [field:SerializeField] public Player Player { get; private set; }
    [field:SerializeField] public float PosY { get; private set; }
    [field:SerializeField] public float PropsSpeed { get; private set; }
    [field:SerializeField] public Transform SpawnPoint { get; private set; } //TEMP
    public GameData GameData { get; private set; }

    public EInputPrecision InputPrecision
    {
        get
        {
            float f = _soundtracker.curves[_currentCurve].curve.Evaluate(_audioSource.time);
            if (f >= GameData.perfectTolerance)
                return EInputPrecision.PERFECT;
            if (f >= GameData.niceTolerance)
                return EInputPrecision.NICE;
            if (f >= GameData.okTolerance)
                return EInputPrecision.OK;
            return EInputPrecision.MISSED;
        }
    }
    
    [SerializeField] private GameObject _npcPrefab;
    [SerializeField] private GameObject _wallPrefab;

    [SerializeField] private TMP_Text _valueOfMusicYippie;
    [SerializeField] private Soundtracker _soundtracker;
    [SerializeField] private AudioSource _audioSource;

    private int _currentCurve = 0;
    private void Update()
    {
        float f = _soundtracker.curves[_currentCurve].curve.Evaluate(_audioSource.time);
        _valueOfMusicYippie.text = f.ToString();

        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (InputPrecision)
            {
                case EInputPrecision.MISSED:
                    Debug.Log($"<b><color=#{Color.red.ToHexString()}> {InputPrecision}</color></b>");
                    break;
                case EInputPrecision.OK:
                    Debug.Log($"<b><color=#{Color.yellow.ToHexString()}> {InputPrecision}</color></b>");
                    break;
                case EInputPrecision.NICE:
                    Debug.Log($"<b><color=#{Color.cyan.ToHexString()}> {InputPrecision}</color></b>");
                    break;
                case EInputPrecision.PERFECT:
                    Debug.Log($"<b><color=#{Color.green.ToHexString()}> {InputPrecision}</color></b>");
                    break;
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);

        Instance = this;
        StartCoroutine(GameLoop());
        
        _audioSource.clip = _soundtracker.audioClip;
        _audioSource.Play();
        
        GameData = Resources.Load<GameData>("GameData");
    }

    public IEnumerator GameLoop()
    {
        while (true)
        {
            bool isNPC = Random.Range(0, 2) == 0;
            GameObject go;

            if (isNPC)
            {
                go = Instantiate(_npcPrefab, SpawnPoint.position, Quaternion.identity);
            }
            else
            {
                go = Instantiate(_wallPrefab, SpawnPoint.position, Quaternion.identity);
            }
            
            yield return new WaitUntil(() => !go);
            yield return new WaitForSeconds(Random.Range(0f, 3f));
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
