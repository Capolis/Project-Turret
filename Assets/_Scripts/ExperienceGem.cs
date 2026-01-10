using UnityEngine;

public class ExperienceGem : MonoBehaviour{

    public int xpAmount = 10;
    public float speed = 8f;

    void Update(){

        // Move a gema em direção ao centro (0,0)
        transform.position = Vector2.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);

        // Se estiver muito perto do centro, entrega a XP e some
        if (Vector2.Distance(transform.position, Vector3.zero) < 0.2f){
            Collect();
        }

    }

    void Collect(){

        // Procura o Player para entregar a XP
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
            player.GetComponent<LevelSystem>().AddExperience(xpAmount);
        }

        Destroy(gameObject); // Destroi a gema

    }

}