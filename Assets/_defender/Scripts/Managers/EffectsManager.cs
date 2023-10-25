using System;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; private set; }

    [SerializeField] private GameObject _explosionPrefab, _beamInPrefab;

    public GameObject ExplosionPrefab => _explosionPrefab;
    public GameObject BeamInprefab => _beamInPrefab;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}