using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public List<GameObject> gameObjects; 
    public int initialPlatformCount = 3; 
    private int _layer = 0;

    private List<GameObject> platforms = new List<GameObject>();
    private Transform playerTransform;
    private float scrollSpeed = 5f; 
    private float lastPlatformRightEdge; 

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < initialPlatformCount; i++)
        {
            Vector3 spawnPosition = (i == 0) ? Vector3.zero : new Vector3(lastPlatformRightEdge, 0, 0);
            SpawnPlatform(spawnPosition);
        }
    }

    void Update()
    {
        ScrollPlatforms();
        CheckAndSpawnPlatform();

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            _layer++;
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            _layer--;
        }
    }

    void ScrollPlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            platform.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
    }

    void CheckAndSpawnPlatform() // FAIRE LE CALCUL DE LA POS DE SPAWN AU MOMENT OU LE SPAWN EST APPELE 
    {
        GameObject firstPlatform = platforms[0];

        if (firstPlatform.transform.position.x + GetPlatformWidth(firstPlatform) / 2 < playerTransform.position.x - GetPlatformWidth(firstPlatform))
        {
            Destroy(firstPlatform);
            platforms.RemoveAt(0);

            Vector3 spawnPosition = new Vector3(lastPlatformRightEdge, 0, 0);
            SpawnPlatform(spawnPosition);
        }
    }

    void SpawnPlatform(Vector3 spawnPosition)
    {
        GameObject platformPrefab = SelectPrefab();

        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        platforms.Add(platform);

        float platformWidth = GetPlatformWidth(platform);
        lastPlatformRightEdge = platform.transform.position.x + (platformWidth / 2);
    }

    float GetPlatformWidth(GameObject platform)
    {
        SpriteRenderer platformRenderer = platform.GetComponent<SpriteRenderer>();
        if (platformRenderer != null)
        {
            return platformRenderer.bounds.size.x; // Use the x-axis as it's a 2D game
        }
        else
        {
            Debug.LogWarning("Platform prefab does not have a Renderer component!");
            return 1f; // Return a default width in case something goes wrong
        }
    }

    GameObject SelectPrefab()
    {
        return gameObjects[_layer];
    }
}
