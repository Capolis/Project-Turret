using UnityEngine;

public class TurretController : MonoBehaviour{

    [Header("Configurações de Tiro")]
    public GameObject bulletPrefab; // O modelo da bala
    public Transform firePoint;     // De onde a bala sai
    public float fireRate = 0.5f;   // Tempo entre tiros (segundos)

    [Header("Upgrades de Arma")]
    public int projectileCount = 1;     // Quantas balas saem por tiro (1 = normal, 3 = triplo)
    public float spreadAngle = 20f;     // Abertura do leque (em graus) se tiver mais de 1 bala

    private float nextFireTime = 0f; // Variável de controle interno

    void Update(){
        if (Time.timeScale == 0) return;

        RotateTowardsMouse();
        HandleShooting();
    }

    void RotateTowardsMouse(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void HandleShooting(){
        // Lógica simples de Cooldown: Se o tempo atual do jogo for maior que o tempo permitido...
        if (Time.time >= nextFireTime)
        {
            Shoot();
            // Define o próximo momento permitido para atirar
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot(){
        // Se for só 1 bala, comportamento padrão (econômico)
        if (projectileCount == 1){
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        }
        else{
            // Lógica do Leque (Shotgun)
            // Calcula o ângulo inicial (o mais à esquerda do leque)
            float startRotation = -spreadAngle / 2f;
            // Calcula o passo entre cada bala
            float angleStep = spreadAngle / (projectileCount - 1);

            for (int i = 0; i < projectileCount; i++){
                // Calcula o ângulo desta bala específica
                float currentAngle = startRotation + (angleStep * i);

                // Cria uma rotação combinada: Rotação da Torre + Ajuste do Leque
                Quaternion rotation = transform.rotation * Quaternion.Euler(0, 0, currentAngle);

                Instantiate(bulletPrefab, firePoint.position, rotation);
            }
        }
    }

}