using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _duration = 0.25f;

    private float _destroyTime;

    private bool OutOfFuel => Time.time >= _destroyTime;

    private void OnEnable()
    {
        _destroyTime = Time.time + _duration;
        _rigidbody.velocity = (transform.right * _speed);
    }

    private void Update()
    {
        if (OutOfFuel)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Destructable>(out var target))
        {
            target.DestroyMe();
        } 
    }
}