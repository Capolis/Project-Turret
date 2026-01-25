using UnityEngine;

public class Projectile : MonoBehaviour{

    public float speed = 10f;
    public float lifetime = 3f; // Tempo até a bala sumir (performance)
    public int damage = 1; // Dano base do tiro

    [HideInInspector] // Esconde do Inspector pois quem define isso é a Torre
    public int pierceCount = 1;

    void Start(){
        // Destroi este objeto após X segundos para não travar o jogo com infinitas balas
        Destroy(gameObject, lifetime);

    }

    void Update(){
        // Move o objeto para "CIMA" (frente) em relação à própria rotação dele
        // Time.deltaTime garante que a velocidade seja por SEGUNDO, não por FRAME
        transform.Translate(Vector3.up * speed * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Enemy")){
            // Tenta achar um Inimigo Padrão
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null){
                enemy.TakeDamage(damage);
            }
            else{
                // SE NÃO FOR INIMIGO PADRÃO, TENTA ACHAR UM ASTEROIDE
                AsteroidController asteroid = other.GetComponent<AsteroidController>();
                if (asteroid != null){
                    asteroid.TakeDamage(damage);
                }
                // SE NÃO FOR ASTEROIDE, TENTA O SHOOTER
                ShooterEnemy shooter = other.GetComponent<ShooterEnemy>();
                if (shooter != null){
                    shooter.TakeDamage(damage);
                }
            }
            // Reduz 1 do contador de perfuração
            pierceCount--;
            // Se não sobrou poder de perfuração, destrói a bala.
            // Se sobrou (pierceCount > 0), ela continua
            if (pierceCount <= 0){
                Destroy(gameObject);
            }
        }
    }
}