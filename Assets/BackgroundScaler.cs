using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
   [SerializeField] Camera cam;
   [SerializeField] SpriteRenderer spriteRenderer;
    private void Update()
    {
        spriteRenderer.transform.localScale = new Vector3(cam.orthographicSize/5, cam.orthographicSize / 5, cam.orthographicSize / 5);
    }
}
