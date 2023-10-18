using UnityEngine;

public class Music : MonoBehaviour
{
    private void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Music");

        if(objects.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
