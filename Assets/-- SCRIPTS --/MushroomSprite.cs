using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class MushroomSprite : MonoBehaviour
{
    public List<Sprite> mushroomSprites = new List<Sprite>();

    SpriteRenderer _sr;
    float _moveUpOffset;
    float _moveDownOffset;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = mushroomSprites[Random.Range(0, mushroomSprites.Count - 1)];
        _moveUpOffset = _sr.bounds.size.y * -0.1f;
    }

    [Button("warp")]
    public void StartWarp()
    {
        _sr.transform.DOScaleX(1.3f, 0.1f).OnComplete(MidWarp);
        _sr.transform.DOScaleY(0.7f, 0.1f);
        _sr.transform.DOMoveY(transform.position.y + _moveUpOffset, 0.1f);
    }

    public void MidWarp()
    {
        _sr.transform.DOScaleX(0.7f, 0.1f).OnComplete(EndWarp);
        _sr.transform.DOScaleY(1.3f, 0.1f);
        _sr.transform.DOMoveY(transform.position.y - _moveUpOffset, 0.1f);
    }

    private void EndWarp()
    {
        _sr.transform.DOScale(Vector3.one, 0.1f);
    }
}
