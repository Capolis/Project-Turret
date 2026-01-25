using UnityEngine;

public class EnemyProjectile : MonoBehaviour{

    public float speed = 10f;
    public int damage = 1;

    void Update(){
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        // Destrói depois de 5s para não sobrar lixo na memória
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null) player.TakeDamage(damage);
            Destroy(gameObject); // Destrói bala ao acertar player
        }
    }

}