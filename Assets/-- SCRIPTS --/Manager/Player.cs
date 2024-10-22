using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFixedPos
{
    [SerializeField] private Material _colorAura;
    
    private void Update()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 playerScreenPos = new Vector2(screenPos.x / Camera.main.pixelWidth, screenPos.y / Camera.main.pixelHeight);
        
        _colorAura.SetVector("_PlayerPos", playerScreenPos);
    }
}
