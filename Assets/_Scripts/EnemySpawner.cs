using UnityEngine;

public class EnemySpawner : MonoBehaviour{

    public GameObject enemyPrefab;
    public float spawnRate = 1f;   // Cria 1 inimigo por segundo
    public float spawnRadius = 10f; // Distância do centro onde eles nascem

    private float nextSpawnTime = 0f;

    void Update(){

        if (Time.time >= nextSpawnTime){

            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;

        }
    }

    void SpawnEnemy(){

        // Gera uma posição aleatória num círculo de raio 1
        Vector2 randomPoint = Random.insideUnitCircle.normalized;

        // Multiplica pelo raio desejado (ex: 10 metros do centro)
        Vector2 spawnPos = randomPoint * spawnRadius;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

    }

}