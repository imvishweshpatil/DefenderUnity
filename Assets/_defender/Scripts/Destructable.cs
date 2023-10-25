using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private int _points = 150;
    
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    [ContextMenu("Destroy Me")]
    public void DestroyMe()
    {
        Instantiate(EffectsManager.Instance.ExplosionPrefab, transform.position, Quaternion.identity);
        _gameManager.Addpoints(_points);
        _gameManager.ComponentDestroyed(gameObject);
    }
}