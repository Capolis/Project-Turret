using UnityEngine;

public class EnemyProjectile : MonoBehaviour{

    public float speed = 10f;
    public int damage = 1;

    void Update(){
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null) player.TakeDamage(damage);
            Destroy(gameObject); // Destrói bala ao acertar player
        }
    }

}