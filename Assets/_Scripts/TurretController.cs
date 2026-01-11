using UnityEngine;

public class TurretController : MonoBehaviour{

    [Header("Configurações de Tiro")]
    public GameObject bulletPrefab; // O modelo da bala
    public Transform firePoint;     // De onde a bala sai
    public float fireRate = 0.5f;   // Tempo entre tiros (segundos)

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
        // Cria uma cópia do Prefab, na posição do FirePoint, com a rotação da Torre
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }

}