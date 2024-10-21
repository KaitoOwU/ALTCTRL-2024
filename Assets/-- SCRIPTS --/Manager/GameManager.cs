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

    [SerializeField] private GameObject _npcPrefab;
    [SerializeField] private GameObject _wallPrefab;

    [SerializeField] private TMP_Text _valueOfMusicYippie;
    [SerializeField] private Soundtracker _soundtracker;
    [SerializeField] private AudioSource _audioSource;
    private void Update()
    {
        float f = _soundtracker.curves[0].curve.Evaluate(_audioSource.time);
        _valueOfMusicYippie.text = f.ToString();
        

        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            string debug = _valueOfMusicYippie.text;
            switch (f)
            {
                case < 0.25f:
                    debug += $"<b><color=#{new Color(0.7f, 0.7f, 0.7f).ToHexString()}> MISSED</color></b>";
                    break;
                case < 0.5f:
                    debug += $"<b><color=#{new Color(0.7f, 0.2f, 0.2f).ToHexString()}> BAD</color></b>";
                    break;
                case < 0.75f:
                    debug += $"<b><color=#{Color.blue.ToHexString()}> OK</color></b>";
                    break;
                case < 0.9f:
                    debug += $"<b><color=#{Color.yellow.ToHexString()}> NICE</color></b>";
                    break;
                case >= 0.9f:
                    debug += $"<b><color=#{Color.green.ToHexString()}> PERFECT</color></b>";
                    break;
            }
            
            Debug.Log(debug);
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
