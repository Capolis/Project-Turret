using UnityEngine;
using UnityEngine.UI;

public class UltimateAbility : MonoBehaviour{

    [Header("Configuração")]
    public float cooldownTime = 15f; // Tempo para carregar (segundos)
    public int damageAmount = 20;     // Dano em área
    public KeyCode triggerKey = KeyCode.Space;

    [Header("UI")]
    public Image cooldownFillImage; // A imagem colorida que enche

    private float currentCooldown = 0f;
    private bool isReady = false;

    void Start(){
        currentCooldown = cooldownTime; // Começa sem carga (opcional)
    }

    void Update(){
        // 1. Gerencia o Cooldown
        if (!isReady){
            currentCooldown -= Time.deltaTime;

            // Atualiza a UI (Inverte o valor porque 1 = cheio, 0 = vazio)
            if (cooldownFillImage != null)
                cooldownFillImage.fillAmount = 1 - (currentCooldown / cooldownTime);

            if (currentCooldown <= 0){
                isReady = true;
                currentCooldown = 0;
            }
        }

        // 2. Detona a bomba
        if (isReady && Input.GetKeyDown(triggerKey)){
            ActivateUltimate();
        }
    }

    void ActivateUltimate(){
        // Encontra TODOS os inimigos na cena
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject target in targets){
            // 1. Tenta achar Inimigo Comum
            EnemyController enemy = target.GetComponent<EnemyController>();
            if (enemy != null){
                enemy.TakeDamage(damageAmount);
                continue; // Já achou, vai para o próximo alvo
            }

            // 2. Se não achou, tenta achar Asteroide
            AsteroidController asteroid = target.GetComponent<AsteroidController>();
            if (asteroid != null){
                asteroid.TakeDamage(damageAmount);
                continue;
            }

            /* 3. Se não achou, tenta achar Inimigo Atirador (Shooter)
            ShooterEnemy shooter = target.GetComponent<ShooterEnemy>();
            if (shooter != null){
                shooter.TakeDamage(damageAmount);
            } */
        }

        // Reinicia o cooldown
        isReady = false;
        currentCooldown = cooldownTime;

        // (Opcional) Tocar som de explosão aqui
        if (AudioManager.instance != null) AudioManager.instance.PlayExplosion();
    }

}