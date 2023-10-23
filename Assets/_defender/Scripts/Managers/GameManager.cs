using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerShip PlayerShip { get; private set; }
    public int MapWidth = _mapWidth;
    public int Score { get; private set; }
    public int Lives { get; private set; }
    public int SmartBombs { get; private set; }
    
    public event Action<int> ScoreChanged;
    public event Action<int> playerLivesChanged;
    public event Action<int> SmartBombsChanged;
    
    [SerializeField] private GameObject _playerShipPrefab;
    [SerializeField] private AudioClip _startSound;
    [SerializeField] private static int _mapWidth = 50;
   
    private IUserInput _userInput;
    private float _smartBombDelay;

    private void OnEnable()
    {
        StartGame();
    }

    private void Start()
    {
        _userInput = UserInput.Instance;
        _userInput.OnSmartBombPressed += HandleSmartBombPressed;
    }

    public void StartGame()
    {
        SoundManager.Instance.PlayAudioClip(_startSound);
        Score = 0;
        Lives = 3;
        SmartBombs = 3;
        SpawnPlayerShip();
    }

    private void SpawnPlayerShip()
    {
        PlayerShip = Instantiate(_playerShipPrefab).GetComponent<PlayerShip>();
        PlayerShip.name = "Player Ship";
    }

    public void Addpoints(int points)
    {
        Score += points;
        ScoreChanged?.Invoke(Score);
    }

    public void ComponentDestroyed(GameObject component)
    {
        if (component.TryGetComponent<PlayerShip>(out var player))
        {
            playerLivesChanged?.Invoke(--Lives);
            if (Lives > 0)
            {
                Invoke(nameof(SpawnPlayerShip), 3f);
            }
        }
        
        Destroy(component);
    }

    private void HandleSmartBombPressed()
    {
        if (SmartBombs < 1 || Time.time < _smartBombDelay) return;
        _smartBombDelay = Time.time + 0.25f;
        SmartBombsChanged?.Invoke(--SmartBombs);
    }
}

