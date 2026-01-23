using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator

public class EnemyController : MonoBehaviour{

    [Header("Atributos")]
    public float speed = 3f;
    public int maxHealth = 1; // Vida do inimigo (1 = Hit Kill)
    public int damageToPlayer = 1;
    public int xpReward = 10;

    [Header("Visual")]
    public float rotationSpeed = 200f;
    public GameObject xpGemPrefab;

    [Header("Loot")]
    public GameObject coinPrefab;
    [Range(0, 100)] public int coinDropChance = 50; // 50% de chance

    [Header("Visual")]
    public GameObject explosionPrefab;

    [Header("UI World")]
    public GameObject damagePopupPrefab;

    protected int currentHealth;
    protected SpriteRenderer spriteRenderer; // Referência para mudar a cor
    protected Color originalColor;           // Para lembrar a cor original
    protected Transform player;

    protected virtual void Start(){
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Pega o componente visual do próprio inimigo
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color; // Guarda a cor
        // Variação visual (opcional): Gira para lados aleatórios
        if (Random.value > 0.5f) rotationSpeed *= -1;
    }

    protected virtual void Update(){
        // Move o inimigo em direção ao centro (0,0,0) onde está o player
        // Vector2.MoveTowards(OndeEstou, ParaOndeVou, QuantoAndo)
        transform.position = Vector2.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Gira no eixo Z (frente/trás da tela 2D)

    }

    // Função pública para levar dano
    public void TakeDamage(int damage){
        currentHealth -= damage;
        // Mostra o popup de dano
        if (damagePopupPrefab != null){
            // Cria o texto na posição do inimigo
            GameObject popup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
            // Configura o valor. (Opcional: Se damage > 5 considere Crítico 'true')
            popup.GetComponent<DamagePopup>().Setup(damage, damage >= 3);
        }
        // Efeito visual simples: Piscar ou diminuir tamanho (opcional para polimento futuro)
        if (spriteRenderer != null && gameObject.activeInHierarchy)
            StartCoroutine(FlashEffect());
        if (currentHealth <= 0) Die();
    }

    IEnumerator FlashEffect(){
        // 1. Muda para Branco (Flash)
        spriteRenderer.color = Color.white;
        // Caso o sprite for branco, use Color.red para feedback de dano
        // 2. Espera uma fração de segundo (0.1s)
        yield return new WaitForSeconds(0.1f);
        // 3. Volta para a cor normal
        spriteRenderer.color = originalColor;
    }

    void Die(){
        // 1. Cria a explosão
        if (explosionPrefab != null){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        // 2. Cria a gema e guarda a referência dela numa variável temporária 'gem'
        GameObject gem = Instantiate(xpGemPrefab, transform.position, Quaternion.identity);
        // 3. Acessa o script da gema recém-criada
        ExperienceGem gemScript = gem.GetComponent<ExperienceGem>();
        // 4. Sobrescreve o valor padrão (10) pelo valor deste inimigo específico
        if (gemScript != null){
            gemScript.xpAmount = xpReward;
        }
        // 5. Adiciona a pontuação ao ScoreManager
        if (ScoreManager.instance != null){
            // Usa o mesmo valor da XP ou cria uma variável nova 'scoreReward'
            ScoreManager.instance.AddScore(xpReward);
        }
        // 6. Dropa MOEDA
        // Rola um dado de 0 a 100. Se for menor que a chance, dropa.
        if (Random.Range(0, 100) < coinDropChance && coinPrefab != null)
        {
            // Instancia a moeda um pouco ao lado para não ficar encavalada na XP
            Vector3 offset = new Vector3(0.2f, 0.2f, 0);
            Instantiate(coinPrefab, transform.position + offset, Quaternion.identity);
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