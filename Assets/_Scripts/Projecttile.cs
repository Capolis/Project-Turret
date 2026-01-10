using UnityEngine;

public class Projectile : MonoBehaviour{

    public float speed = 10f;
    public float lifetime = 3f; // Tempo até a bala sumir (performance)

    void Start(){

        // Destroi este objeto após X segundos para não travar o jogo com infinitas balas
        Destroy(gameObject, lifetime);

    }

    void Update(){

        // Move o objeto para "CIMA" (frente) em relação à própria rotação dele
        // Time.deltaTime garante que a velocidade seja por SEGUNDO, não por FRAME
        transform.Translate(Vector3.up * speed * Time.deltaTime);

    }
}