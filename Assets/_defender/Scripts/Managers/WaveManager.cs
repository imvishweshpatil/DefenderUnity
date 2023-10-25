using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.Common;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class WaveManager : MonoBehaviour
{
    public int RemainingEnemies => _enemies.Count;

    public void MutateMobs()
    {
        Debug.Log($"Mutating mobs in {gameObject.name}");
        while (_enemies.Any(MutateMob)) { }
    }

    [SerializeField] private float _waveDelay = 0f;

    private GameManager _gameManager;

    private List<GameObject> _enemies;

    private IEnumerator Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.EntityDestroyed += OnEntityDestroyed;
        _enemies = new List<GameObject>();
        foreach (Transform enemy in transform)
        {
            _enemies.Add(enemy.gameObject);
        }

        yield return new WaitForSeconds(_waveDelay);
        SoundManager.Instance.PlayAudioClip(SoundManager.Instance.BeamInSound);
        //instantiate beam-in particle effect
        foreach (var enemy in _enemies)
        {
            Instantiate(
                EffectsManager.Instance.BeamInprefab, 
                enemy.transform.position, 
                Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        foreach (var enemy in _enemies)
        {
            enemy.SetActive(true);
        }
    }

    void OnEntityDestroyed(GameObject entity)
    {
        if (_enemies.Contains(entity)) return;
        Debug.Log($"{name} removing {entity.name} from enemies.");
        _enemies.Remove(entity);
    }

    private bool MutateMob(GameObject mob)
    {
        if (!mob.TryGetComponent<MutatableMob>(out var mutatableMob)) return false;
        Debug.Log($"Mutating {mutatableMob.name} to {mutatableMob.MutantMobPrefab.name}");
        var mobTransform = mutatableMob.transform;
        var mutant = Instantiate(mutatableMob.MutantMobPrefab, mobTransform.position, mobTransform.rotation);
        _enemies.Remove(mutatableMob.gameObject);
        _enemies.Add(mutant);
        return true;
    }
}