using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para reiniciar a cena

public class PlayerHealth : MonoBehaviour{

    public int maxHealth = 5;
    public int currentHealth;

    void Start(){

        currentHealth = maxHealth;
    
    }

    public void TakeDamage(int damageAmount){

        currentHealth -= damageAmount;
        if (currentHealth <= 0){
            Die();
        }

    }

    void Die(){

        // Por enquanto, apenas recarregamos a cena para reiniciar o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}