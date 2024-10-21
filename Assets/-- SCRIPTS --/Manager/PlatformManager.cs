using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public List<GameObject> gameObjects; 
    public int initialPlatformCount = 3; 
    public int layer = 0;

    private List<GameObject> _platforms = new List<GameObject>();
    private Transform _playerTransform;
    private float _scrollSpeed = 5f; 

    private GameObject _lastPlatform;
    private GameObject _lastGivenPrefab;
    private float _lastPlatformWidth;
    Vector3 _spawnPosition;
    float _offset = 0;

    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        SpawnPlatform(true);
        for (int i = 0; i < initialPlatformCount-1; i++)
        {
            _spawnPosition = (i == 0) ? Vector3.zero : new Vector3(_lastPlatform.transform.position.x + (_lastPlatformWidth / 2), 0, 0);
            SpawnPlatform();
        }
    }

    void Update()
    {
        ScrollPlatforms();
        CheckAndSpawnPlatform();
    }

    void ScrollPlatforms()
    {
        foreach (GameObject platform in _platforms)
        {
            platform.transform.Translate(Vector3.left * _scrollSpeed * Time.deltaTime);
        }
    }

    void CheckAndSpawnPlatform() 
    {
        GameObject firstPlatform = _platforms[0];

        if (firstPlatform.transform.position.x + GetPlatformWidth(firstPlatform) / 2 < _playerTransform.position.x - GetPlatformWidth(firstPlatform))
        {
            Destroy(firstPlatform);
            _platforms.RemoveAt(0);

            SpawnPlatform();
        }
    }

    void SpawnPlatform(bool isFirstPlatform = false)
    {
        GameObject platformPrefab = SelectPrefab();

        if (!isFirstPlatform)
        {
            if (_offset != 0)
            {
                _spawnPosition = new Vector3(_lastPlatform.transform.position.x + (_lastPlatformWidth) - _offset, 0, 0);
                _offset = 0;
            }
            else
                _spawnPosition = new Vector3(_lastPlatform.transform.position.x + (_lastPlatformWidth), 0, 0);

        }




        GameObject platform = Instantiate(platformPrefab, _spawnPosition, Quaternion.identity);
        _platforms.Add(platform);

        _lastPlatform = platform;
        _lastPlatformWidth = GetPlatformWidth(platform);
    }

    float GetPlatformWidth(GameObject platform)
    {
        SpriteRenderer platformRenderer = platform.GetComponent<SpriteRenderer>();
        if (platformRenderer != null)
        {
            return platformRenderer.bounds.size.x; 
        }
        else
        {
            Debug.LogWarning("Platform prefab does not have a Renderer component!");
            return 1f; 
        }
    }

    GameObject SelectPrefab()
    {
        GameObject prefabToReturn = gameObjects[layer];
        if (_lastPlatform != null && prefabToReturn != _lastGivenPrefab)
        {
            _offset = (_lastPlatformWidth - prefabToReturn.GetComponent<SpriteRenderer>().bounds.size.x) / 2;

        }
        _lastGivenPrefab = prefabToReturn;
        return prefabToReturn;
    }
}
