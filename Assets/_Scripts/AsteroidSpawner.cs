using UnityEngine;

public class AsteroidSpawner : MonoBehaviour{

    public GameObject asteroidPrefab;
    public Transform player;
    public float spawnRate = 5f; // Menos frequente que inimigos
    public float spawnDistance = 18f; // Bem longe (fora da tela)

    private float nextSpawnTime;

    void Update(){
        if (player == null) return;
        if (Time.time >= nextSpawnTime){
            SpawnAsteroid();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnAsteroid(){
        // Gera posição aleatória num círculo grande ao redor do player
        Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnDistance;
        Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
    }

}