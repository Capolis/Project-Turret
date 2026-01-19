using UnityEngine;

public class CoinPickup : MonoBehaviour{
    public int coinValue = 1;
    public float speed = 12f; // Moedas são atraídas mais rápido que XP!

    void Update(){
        // Voa para o player (0,0)
        transform.position = Vector2.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, Vector3.zero) < 0.2f){
            Collect();
        }
    }

    void Collect(){
        if (EconomyManager.instance != null){
            EconomyManager.instance.AddCoins(coinValue);
        }
        Destroy(gameObject);
    }
}