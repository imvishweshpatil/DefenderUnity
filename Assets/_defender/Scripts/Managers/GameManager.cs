using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerShip PlayerShip { get; private set; }

    [SerializeField] private GameObject _playerShipPrefab;
    [SerializeField] private AudioClip _startSound;
    [SerializeField] private int _mapWidth = 50;

    public int MapWidth => _mapWidth;
    private void OnEnable()
    {
        StartGame();
    }

    public void StartGame()
    {
        PlayerShip = Instantiate(_playerShipPrefab).GetComponent<PlayerShip>();
        PlayerShip.name = "Player Ship";
        SoundManager.Instance.PlayAudioClip(_startSound);
    }
}

