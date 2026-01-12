using UnityEngine;

public class EnemyController : MonoBehaviour{

    [Header("Atributos")]
    public float speed = 3f;
    public int maxHealth = 1; // Vida do inimigo (1 = Hit Kill)
    public int damageToPlayer = 1;
    public int xpReward = 10;

    [Header("Visual")]
    public float rotationSpeed = 200f;
    public GameObject xpGemPrefab;

    private int currentHealth;

    void Start(){
        currentHealth = maxHealth;
        // Variação visual (opcional): Gira para lados aleatórios
        if (Random.value > 0.5f) rotationSpeed *= -1;
    }

    void Update(){
        // Move o inimigo em direção ao centro (0,0,0) onde está o player
        // Vector2.MoveTowards(OndeEstou, ParaOndeVou, QuantoAndo)
        transform.position = Vector2.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Gira no eixo Z (frente/trás da tela 2D)

    }

    // Função pública para levar dano
    public void TakeDamage(int damage){
        currentHealth -= damage;

        // Efeito visual simples: Piscar ou diminuir tamanho (opcional para polimento futuro)

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die(){
        // 1. Cria a gema e guarda a referência dela numa variável temporária 'gem'
        GameObject gem = Instantiate(xpGemPrefab, transform.position, Quaternion.identity);

        // 2. Acessa o script da gema recém-criada
        ExperienceGem gemScript = gem.GetComponent<ExperienceGem>();

        // 3. Sobrescreve o valor padrão (10) pelo valor deste inimigo específico
        if (gemScript != null)
        {
            gemScript.xpAmount = xpReward;
        }

        // Toca som e destrói
        if (AudioManager.instance != null)
            AudioManager.instance.PlayExplosion();

        Destroy(gameObject);
    }

    // Função automática da Unity que detecta quando algo ENTRA no colisor
    void OnTriggerEnter2D(Collider2D other){
        // Se bater no Player
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null){
                playerHealth.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject);
        }
    }

}