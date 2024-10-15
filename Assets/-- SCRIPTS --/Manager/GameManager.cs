using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);

        Instance = this;
        StartCoroutine(GameLoop());
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
