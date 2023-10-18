using UnityEngine;


public class Destructable : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;

    [ContextMenu("Destroy Me")]
    public void DestroyMe()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}