using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class FrogLeap : MonoBehaviour
{
    public Sprite[] sprites; 
    SpriteRenderer spriteRenderer; 

    public float[] timings;  

    private int currentIndex = 0;
    private bool isReversing = false; 
    private GameManager _gameManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = FindObjectOfType<GameManager>();

        _gameManager.onRealBeat += StartLooping;

    }

    [Button("Start Looping")]
    public void StartLooping()
    {
        if (sprites.Length == 3 && timings.Length == 3)
        {
            currentIndex = 0;  
            isReversing = false; 
            LoopOnce();
            Debug.Log("Looping started.");
        }
        else
        {
            Debug.LogError("Please assign 3 sprites and 3 timing values.");
        }
    }

    private void LoopOnce()
    {
        SetSprite(currentIndex);

        DOVirtual.DelayedCall(timings[currentIndex], () =>
        {
            if (!isReversing)
            {
                currentIndex++;
                if (currentIndex == sprites.Length)
                {
                    isReversing = true;
                }
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    return;
                }
            }

            LoopOnce();
            Debug.Log("Current Index: " + currentIndex);    
        });
    }

    private void SetSprite(int index)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[index];
        }
        else
        {
            Debug.LogError("No Image or SpriteRenderer assigned.");
        }
    }
}
