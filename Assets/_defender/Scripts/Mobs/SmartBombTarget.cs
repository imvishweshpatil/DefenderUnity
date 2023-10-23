using System;
using UnityEngine;

public class SmartBombTarget : MonoBehaviour
{
    private Camera _mainCamera;
    private GameManager _gameManager;

    private void Start()
    {
        _mainCamera = Camera.main;
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.SmartBombsChanged += OnSmartBombsChanged;
    }

    private void OnDestroy()
    {
        _gameManager.SmartBombsChanged -= OnSmartBombsChanged;     
    }

    private void OnSmartBombsChanged(int bombs)
    {
        if (!IsWithinBombRange() || !TryGetComponent<Destructable>(out var target)) return;
        target.DestroyMe();
    }

    bool IsWithinBombRange()
    {
        var viewportPosition = _mainCamera.WorldToViewportPoint(transform.position);
        return viewportPosition.x is > 0 and < 1 && viewportPosition.y is > 0 and < 1;
    }
}