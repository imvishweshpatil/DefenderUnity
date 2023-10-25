using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public int RemainingHumans => _humans.Count;
    public int RemainingEnemies => _waves.Sum(w => w.RemainingEnemies);
    
    [SerializeField] private WaveManager[] _waves;
    [SerializeField] private Transform _humansContainer;

    private GameManager _gameManager;
    private List<GameObject> _humans;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.EntityDestroyed += OnEntityDestroyed;
        _humans = new List<GameObject>();
        foreach (Transform human in _humansContainer)
        {
            _humans.Add(human.gameObject);
        }
        Debug.Log($"Mob manager found {_humans.Count} humans.");
    }

    private void OnEntityDestroyed(GameObject entity)
    {
        if (!_humans.Contains(entity)) return;
        _humans.Remove(entity);
        Debug.Log($"{name} removing {entity.name}. {_humans.Count} humans remain.");
    }
}