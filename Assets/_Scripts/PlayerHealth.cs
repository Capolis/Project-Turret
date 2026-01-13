using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para reiniciar a cena

public class PlayerHealth : MonoBehaviour{

    public int maxHealth = 5;
    public int currentHealth;

    void Start(){
        // Lê o nível comprado. Se não tiver comprado nada, retorna 0.
        int bonusHealth = PlayerPrefs.GetInt("Shop_HealthLvl", 0);
        // Adiciona ao máximo base
        maxHealth = maxHealth + bonusHealth;
        currentHealth = maxHealth;    
    }

    public void TakeDamage(int damageAmount){
        currentHealth -= damageAmount;

        if (UIManager.instance != null){
            UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        }
        if (currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        SceneManager.LoadScene("MainMenu");
    }
}