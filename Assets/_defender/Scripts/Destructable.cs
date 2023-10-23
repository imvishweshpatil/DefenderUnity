using System;
using UnityEngine;


public class Destructable : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private int _points = 150;
    
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    [ContextMenu("Destroy Me")]
    public void DestroyMe()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        _gameManager.Addpoints(_points);
        _gameManager.ComponentDestroyed(gameObject);
    }
}