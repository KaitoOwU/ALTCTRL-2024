using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using NaughtyAttributes;

public class FrogLeap : MonoBehaviour
{
    [SerializeField] Animator _anim;
    GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _anim = GetComponent<Animator>();
    }

    [Button("test anim")]
    private void PlayFrogAnim()
    {
        //_anim["FrogLeap"].wrapMode = WrapMode.Once;
        _anim.Play("FrogLeap");
    }

    private IEnumerator StopLeapAnim()
    {
        yield return new WaitForSeconds(0.1f);
        _anim.SetBool("isLeap", false);
    }
}
