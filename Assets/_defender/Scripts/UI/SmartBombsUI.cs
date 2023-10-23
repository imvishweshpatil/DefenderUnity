using UnityEngine;

public class SmartBombsUI : MonoBehaviour
{
    [SerializeField] private GameObject _smartBombPrefab;
    private Transform _transform;
    private GameManager _gameManager;

    private void Awake()
    {
        _transform = transform;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _gameManager.SmartBombsChanged += OnSmartBombsChanged;
    }

    private void OnSmartBombsChanged(int bombs)
    {
        while (_transform.childCount > bombs)
        {
            Transform bomb = _transform.GetChild(0);
            bomb.SetParent(null);
            Destroy(bomb.gameObject);
        }

        while (_transform.childCount < bombs)
        {
            Instantiate(_smartBombPrefab, _transform);
        }
    }
}