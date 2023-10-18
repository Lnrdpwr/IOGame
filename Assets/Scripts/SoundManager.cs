using UnityEngine;

public class SoundManager : MonoBehaviour
{
    internal static AudioSource instance;

    void Start()
    {
        instance = gameObject.GetComponent<AudioSource>();
    }
}
