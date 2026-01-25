using UnityEngine;

// HERANÇA: ShooterEnemy É UM EnemyController
public class ShooterEnemy : EnemyController{

    [Header("Configuração de Atirador")]
    public float stopDistance = 6f; // Distância segura para parar
    public float retreatDistance = 4f; // Se o player chegar muito perto, ele recua

    [Header("Arma")]
    public GameObject enemyBulletPrefab;
    public float fireRate = 2f;
    private float nextFireTime;

    // Sobrescrevemos o Start para manter a lógica do pai (vida) e adicionar a nossa
    protected override void Start(){
        base.Start(); // IMPORTANTE: Chama o Start do pai para calcular HP e achar o Player
    }
    // Sobrescrevemos o Update porque o movimento é diferente
    protected override void Update(){
        if (player == null) return;
        float distance = Vector2.Distance(transform.position, player.position);
        // --- 1. MOVIMENTAÇÃO INTELIGENTE (Kiting) ---
        if (distance > stopDistance){
            // Longe demais: Aproxima
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distance < retreatDistance){
            // Perto demais: Foge (Recua)
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
        else{
            // Distância ideal: Fica parado
            transform.position = this.transform.position;
        }
        // --- 2. ROTAÇÃO ---
        Vector2 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        // --- 3. TIRO ---
        if (Time.time >= nextFireTime){
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot(){
        if (enemyBulletPrefab != null){
            Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
        }
    }

}