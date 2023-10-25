using UnityEditor.PackageManager;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    [SerializeField] LayerMask _layersToIgnore;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ShouldIgnoreThisCollision(other)) return;
        if (other.TryGetComponent<Destructable>(out var target))
        {
            target.DestroyMe();
        } 
    }

    private bool ShouldIgnoreThisCollision(Collider2D other)
    {
        return (_layersToIgnore & (1 << other.gameObject.layer)) > 0;
    }
}