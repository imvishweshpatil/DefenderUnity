using System;
using System.Net.Sockets;
using UnityEngine;


public class MobScroller : MonoBehaviour
{
    private Transform _transform;
    private GameManager _gameManager;

    private PlayerShip PlayerShip => _gameManager.PlayerShip;
    private bool ShouldScrollMob => UserInput.Instance.IsThrusting;
    private float ScrollAmount => PlayerShip.Speed * PlayerShip.Direction * -1f;
    
    private void Awake()
    {
        _transform = transform;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (!_gameManager || !PlayerShip) return;
        if (ShouldScrollMob)
        {
            ScrollMob();
        }
    }

    private void ScrollMob()
    {
        _transform.position += Vector3.right * ScrollAmount * Time.deltaTime;
    }
}

