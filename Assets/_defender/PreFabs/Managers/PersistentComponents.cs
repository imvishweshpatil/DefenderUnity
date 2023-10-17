using UnityEngine;


public class PersistentComponents : MonoBehaviour
{
    private static PersistentComponents _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;   
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}