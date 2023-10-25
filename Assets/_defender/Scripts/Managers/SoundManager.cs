using UnityEngine;
 
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _fireSound, _startSound, _beamInSound;

    private AudioSource _audioSource; 
    public AudioClip FireSound => _fireSound;
    public AudioClip StartSount => _startSound;
    public AudioClip BeamInSound => _beamInSound;
    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip clip, float volume = 1f)
    {
        _audioSource.PlayOneShot(clip, volume);
    }
}