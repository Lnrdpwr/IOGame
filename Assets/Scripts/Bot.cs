using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    private NavMeshAgent _botAgent;
    private List<Transform> _destinations = new List<Transform>();

    private void Start()
    {
        _botAgent = GetComponent<NavMeshAgent>();
        GameObject[] destinationsObj = GameObject.FindGameObjectsWithTag("Destination");
        foreach(GameObject obj in destinationsObj)
        {
            _destinations.Add(obj.transform);
        }

        int addMeat = BotSpawner.Instance._addMeat;

        GetComponent<PlayerAnimator>().SetRunningAnimation(true);
        GetComponent<MeatEatingComponent>().AddMeat(Random.Range(5 + addMeat, 10 + addMeat));
        StartCoroutine(ChangeDestination(0));
    }

    public void LockMovement(Transform player)
    {
        StopAllCoroutines();
        _botAgent.SetDestination(transform.position);
        transform.LookAt(player);

        StartCoroutine(ChangeDestination(2.2f));
    }

    public void SelfDestroy()
    {
        Destroy(_canvas);
        Destroy(gameObject);
    }

    IEnumerator ChangeDestination(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            Vector3 chosenDestination = _destinations[Random.Range(0, _destinations.Count)].position;
            _botAgent.SetDestination(chosenDestination);
            yield return new WaitUntil(() => Vector3.Distance(chosenDestination, transform.position) < 3);
        }
    }
}
