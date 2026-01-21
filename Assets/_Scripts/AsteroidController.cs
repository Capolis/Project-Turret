using UnityEngine;

public class AsteroidController : MonoBehaviour{

    [Header("Configuração Base")]
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float baseSpeed = 2f;
    public int baseHealth = 2;
    public int damageToPlayer = 2;

    [Header("Loot")]
    public GameObject xpGemPrefab;
    public GameObject explosionPrefab;

    private int currentHealth;
    private float speed;

    void Start(){
        // --- LÓGICA DE TAMANHO ALEATÓRIO ---
        float randomSize = Random.Range(minSize, maxSize);

        // Aplica o tamanho (Escala)
        transform.localScale = Vector3.one * randomSize;

        // --- VIDA PROPORCIONAL AO TAMANHO ---
        // Ex: Se tamanho for 2.0 (Dobro), vida será maior.
        // Mathf.RoundToInt arredonda para número inteiro.
        currentHealth = Mathf.RoundToInt(baseHealth * randomSize);

        // --- VELOCIDADE INVERSA ---
        // Asteroides grandes são mais lentos, pequenos são mais rápidos
        speed = baseSpeed / randomSize;

        // Rotação aleatória inicial para dar estilo
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    void Update(){
        // Move para frente (na direção que ele nasceu apontando)
        // Se usar o EnemySpawner atual, ele aponta pro player.
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Destrói se for muito longe (Limpeza de memória)
        if (Vector3.Distance(transform.position, Vector3.zero) > 20f){
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        if (currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Asteroides podem não dar XP ou dar menos
        if (Random.value > 0.5f) // 50% chance de dropar XP
            Instantiate(xpGemPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other){
        // Dano no Player
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null) player.TakeDamage(damageToPlayer);

            // O asteroide se quebra no player
            Die();
        }
    }

}