using UnityEngine;

public class EnemyController : MonoBehaviour{

    public float speed = 3f;
    public GameObject xpGemPrefab;

    void Update(){

        // Move o inimigo em direção ao centro (0,0,0) onde está o player
        // Vector2.MoveTowards(OndeEstou, ParaOndeVou, QuantoAndo)
        transform.position = Vector2.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
    
    }

    // Função automática da Unity que detecta quando algo ENTRA no colisor
    void OnTriggerEnter2D(Collider2D other){

        // Se batermos em algo que tenha o script "Projectile" (nossa bala)...
        // Nota: Precisaremos garantir que a Bala tenha um Collider e seja "Is Trigger"
        if (other.GetComponent<Projectile>() != null){
            Instantiate(xpGemPrefab, transform.position, Quaternion.identity); // Cria a gema no lugar onde o inimigo morreu
            Destroy(other.gameObject); // Destroi a bala
            Destroy(gameObject);       // Destroi o inimigo
        }
        else if (other.CompareTag("Player")){
            // Tenta pegar o script de vida do objeto que batemos
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null) {
                playerHealth.TakeDamage(1); // Tira 1 de vida
            }            
            Destroy(gameObject); // O inimigo morre ao colidir
        }

    }

}