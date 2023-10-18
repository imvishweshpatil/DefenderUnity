
using UnityEngine;


public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private AudioClip _fireSound;
    [SerializeField] private float _fireSoundVolume = 0.33f;
    [SerializeField] private float _fireDelay = 0.25f;

    private float _coolDownTime;
    private Transform _transform;
    private bool CanFire => Time.time >= _coolDownTime;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _coolDownTime = Time.time;
    }

    public void FireGun()
    {
        if (!CanFire) return;
        SoundManager.Instance.PlayAudioClip(_fireSound, _fireSoundVolume);
        var projectile = Instantiate(_projectilePrefab, _transform.position, _transform.rotation);
        projectile.gameObject.SetActive(true);
        _coolDownTime = Time.time + _fireDelay;
    }
}