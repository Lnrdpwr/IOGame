using System.Collections;
using UnityEngine;

public class MeatSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private GameObject _meat;
    [SerializeField] private float _meatToSpawn = 1;

    private void Start()
    {
        for (int i = 0; i < 30; i++)
            Spawn();

        StartCoroutine(SpawnMeat());
    }

    IEnumerator SpawnMeat()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(_meatToSpawn);
        }
    }

    private void Spawn()
    {
        Vector3 chosenPosition = _spawnPositions[Random.Range(0, _spawnPositions.Length)].position + new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-4f, 4f));
        Instantiate(_meat, chosenPosition, Quaternion.identity);
    }
}
