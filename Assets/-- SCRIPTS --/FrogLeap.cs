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
        _gameManager.onRealBeat += PlayFrogAnim;
    }

    [Button("test anim")]
    private void PlayFrogAnim()
    {
        _anim.SetBool("isLeaping", true);
    }

    private IEnumerator StopLeapAnim()
    {
        yield return new WaitForSeconds(0.1f);
        _anim.SetBool("isLeaping", false);
    }
}
