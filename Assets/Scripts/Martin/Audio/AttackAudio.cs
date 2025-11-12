using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _StartSource;
    [SerializeField] private AudioSource _LoopSource;

    [SerializeField] private InGameCanvas _menu;

    [SerializeField] private float _volume;

    private bool _isAttacking;

    private void Start()
    {
        _StartSource.enabled = false;
        _LoopSource.enabled = false;
    }


    private void Update()
    {
        if (!_menu._IsPaused)
        {
            if (Input.GetMouseButton(0))
            {
                _isAttacking = true;

                _StartSource.volume = _volume;
                _LoopSource.volume = _volume;

                _StartSource.enabled = true;
                if (!_StartSource.isPlaying)
                {
                    _LoopSource.enabled = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isAttacking = false;
            }
        }
        
        if (!_isAttacking)
        {
            _StartSource.volume -= 0.03f;
            _LoopSource.volume -= 0.03f;

            if (_LoopSource.volume <= 0f || _StartSource.volume <= 0f)
            {
                _LoopSource.enabled = false;
                _StartSource.enabled = false;
            }
        }
    }
}
