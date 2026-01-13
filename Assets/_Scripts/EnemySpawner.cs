using UnityEngine;
using System.Collections.Generic; // Necessário para usar Listas

public class EnemySpawner : MonoBehaviour{

    [System.Serializable]
    public class EnemyType{
        public string name;           // Apenas para organização (ex: "Satélite Tank")
        public GameObject prefab;     // O prefab do inimigo
        public int minPlayerLevel;    // Nível mínimo para começar a aparecer
        [Range(0, 100)] public int spawnChanceWeight; // Chance relativa de aparecer (Peso) INATIVO
    }

    public List<EnemyType> enemyTypes; // Lista que vamos preencher no Inspector

    public float spawnRate = 1f;
    public float spawnRadius = 18f;

    private float nextSpawnTime = 0f;
    private LevelSystem playerLevelSystem; // Referência para saber o nível atual

    void Start(){
        // Encontra o Player para ler o nível
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
            playerLevelSystem = player.GetComponent<LevelSystem>();
        }
    }

    void Update(){
        if (Time.time >= nextSpawnTime){
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy(){
        if (playerLevelSystem == null) return;

        // 1. Filtrar quais inimigos podem nascer no nível atual
        List<EnemyType> availableEnemies = new List<EnemyType>();
        int currentLevel = playerLevelSystem.level;

        foreach (EnemyType enemy in enemyTypes){
            if (currentLevel >= enemy.minPlayerLevel){
                availableEnemies.Add(enemy);
            }
        }

        if (availableEnemies.Count == 0) return;

        // 2. Escolher um aleatório da lista (Simples)
        EnemyType selectedEnemy = availableEnemies[Random.Range(0, availableEnemies.Count)];

        // 3. Criar o inimigo
        Vector2 randomPoint = Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(selectedEnemy.prefab, randomPoint, Quaternion.identity);
    }
}