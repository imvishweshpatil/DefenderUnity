using System;
using UnityEngine;


public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _frameDelay = 0.25f;

    private float _nextFrameTime;
    private int _frame = 0;
    
    private void OnEnable()
    {
        _nextFrameTime = Time.time + _frameDelay;
        _frame = 0;
    }

    private void LateUpdate()
    {
        if (Time.time >= _nextFrameTime)
        {
            AdvanceToNextFrame();
            _nextFrameTime = Time.time + _frameDelay;
        }
    }

    private void AdvanceToNextFrame()
    {
        _frame = ++_frame % _sprites.Length;
        _renderer.sprite = _sprites[_frame];
    }
}

