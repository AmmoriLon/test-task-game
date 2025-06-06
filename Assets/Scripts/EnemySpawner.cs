using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 3;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}