using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Destructable>(out var target))
        {
            target.DestroyMe();
        } 
    }
}