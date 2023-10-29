using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerShip PlayerShip { get; private set; }
    public bool PlayerAlive { get; private set; }
    public int MapWidth => _mapWidth;
    public int Score { get; private set; }
    public int Lives { get; private set; }
    public int SmartBombs { get; private set; }

    public event Action PlayerShipSpawned = delegate {  };
    public event Action<int> ScoreChanged;
    public event Action<int> PlayerLivesChanged;
    public event Action<int> SmartBombsChanged;
    public event Action<GameObject> EntityDestroyed;
    
    [SerializeField] GameObject _playerShipPrefab;
    [SerializeField] int _mapWidth = 50;

    IUserInput _userInput;
    float _smartBombDelay;
    MobManager _mobManager;


    void OnEnable()
    {
        StartGame();
    }

    void Awake()
    {
        _mobManager = FindObjectOfType<MobManager>();
    }

    void Start()
    {
        _userInput = UserInput.Instance;
        _userInput.OnSmartBombPressed += HandleSmartBombPressed;
    }

    public void StartGame()
    {
        SoundManager.Instance.PlayAudioClip(SoundManager.Instance.StartSound);
        Score = 0;
        Lives = 3;
        SmartBombs = 3;
        SpawnPlayerShip();
    }

    void SpawnPlayerShip()
    {
        PlayerShip = Instantiate(_playerShipPrefab).GetComponent<PlayerShip>();
        PlayerShip.name = "Player Ship";
        PlayerAlive = true;
        PlayerShipSpawned();
    }

    public void AddPoints(int points)
    {
        Score += points;
        ScoreChanged?.Invoke(Score);
    }

    public void ComponentDestroyed(GameObject component)
    {
        if (component.TryGetComponent<PlayerShip>(out var player))
        {
            PlayerAlive = false;
            PlayerLivesChanged?.Invoke(--Lives);
            if (Lives > 0)
            {
                Invoke(nameof(SpawnPlayerShip), 3f);
            }
        }

        component.DropHumanPassenger(_mobManager.HumansContainer);
        Destroy(component);
        EntityDestroyed?.Invoke(component);

        if (Lives < 1)
        {
            GameOver();
            return;
        }

        if (_mobManager.RemainingEnemies < 1)
        {
            WaveComplete();
        }
    }

    void WaveComplete()
    {
        Debug.Log("Wave complete!");
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
    }

    void HandleSmartBombPressed()
    {
        if (SmartBombs < 1 || Time.time < _smartBombDelay) return;
        _smartBombDelay = Time.time + 0.25f;
        SmartBombsChanged?.Invoke(--SmartBombs);
    }
}
