using UnityEngine;

public class MutatableMob : MonoBehaviour
{
    [SerializeField] GameObject _mutantMobPrefab;
    
    public GameObject MutantMobPrefab => _mutantMobPrefab;
}