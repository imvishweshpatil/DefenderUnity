using System.Runtime.Versioning;
using UnityEngine;

public class BootStraper : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Initialize()
    {
        var bootstrapPrefab = Resources.Load("--- Persistent Components ---");
        if (bootstrapPrefab == null) return;
        var bootstrapper = GameObject.Instantiate(bootstrapPrefab);
        bootstrapper.name = "--- Persistent Components ---";
        DontDestroyOnLoad(bootstrapper);
    }
}

