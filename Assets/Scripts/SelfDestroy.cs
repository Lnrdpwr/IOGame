using System.Collections;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float _time;

    private void Start()
    {
        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}
