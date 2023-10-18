using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private GameObject[] _bots;
    [SerializeField] private Transform _player;
    [SerializeField] private int _enemiesToSpawn = 9;

    public int _addMeat;

    internal static BotSpawner Instance;

    private void Awake()
    {
        _addMeat += PlayerPrefs.GetInt("AddMeat", 0) / 2;

        Instance = this;
        for(int i = 0; i < _enemiesToSpawn; i++)
            Spawn();
    }

    public void Spawn()
    {
        Transform chosenSpawn = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        while(Vector3.Distance(chosenSpawn.position, _player.position) < 35)
            chosenSpawn = _spawnPositions[Random.Range(0, _spawnPositions.Length)];

        GameObject enemy = Instantiate(_bots[Random.Range(0, _bots.Length)], chosenSpawn);
    }

    public void AutoLevel(int meat)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            if(Vector3.Distance(enemy.transform.position, _player.position) > 35)
            {
                enemy.GetComponent<MeatEatingComponent>().AddMeat(meat);
            }
        }
    }
}
