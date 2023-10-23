using System;
using TMPro;
using UnityEngine;

public class PlayerLivesUI : MonoBehaviour
{
    [SerializeField] private GameObject _playerLifePrefab;
    private Transform _transform;
    private GameManager _gameManager;

    private void Awake()
    {
        _transform = transform;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _gameManager.playerLivesChanged += OnPlayerLiveschanged;
    }

    private void OnPlayerLiveschanged(int lives)
    {
        while (_transform.childCount > lives)
        {
            Transform playerLife = _transform.GetChild(0);
            playerLife.SetParent(null);
            Destroy(playerLife.gameObject);
        }

        while (_transform.childCount < lives)
        { 
            Instantiate(_playerLifePrefab, _transform);
        }
    }
}