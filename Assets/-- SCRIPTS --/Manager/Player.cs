using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFixedPos
{
    [SerializeField] private Material _colorAura;
    [SerializeField] private ParticleSystem _musicParticles;
    [SerializeField] private ParticleSystem _perfectParticles;
    private Animator _anim;

    GameManager _gameManager;
    EInputPrecision _inputPrecision;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _gameManager = GetComponent<GameManager>();


    }

    private void Update()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 playerScreenPos = new Vector2(screenPos.x / Camera.main.pixelWidth, screenPos.y / Camera.main.pixelHeight);
        
        _colorAura.SetVector("_PlayerPos", playerScreenPos);
    }

    [Button("jumpanimtest")]
    public void TryJumpAnim()
    {
        _anim.SetBool("isJumping", true);
        StartCoroutine(StopJumpAnim());
    }

    private IEnumerator StopJumpAnim()
    {
        yield return new WaitForSeconds(0.1f);
        _anim.SetBool("isJumping", false);
    }

    private void PlayMusicParticles()
    {
        //_inputPrecision = _gameManager.InputPrecision;

        //switch (_inputPrecision)
        //{
        //    // CHANGE PARTICLE DENSITY
        //    case EInputPrecision.PERFECT:
        //        break;
        //    case EInputPrecision.NICE:
        //        break;
        //    case EInputPrecision.OK:
        //        break;
        //    case EInputPrecision.MISSED:
        //        break;
        //    default:
        //        break;
        //}

        // PLAY SYSTEM ANYWAYS
        _musicParticles.Play();
        StartCoroutine(StopParticleSystem(_musicParticles));

    }

    private IEnumerator StopParticleSystem(ParticleSystem system) 
    {
        yield return new WaitForSeconds(0.1f);
        system.Stop();
    }
}
