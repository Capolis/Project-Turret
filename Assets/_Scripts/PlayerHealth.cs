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
        // Atualiza a UI da barra de vida
        if (UIManager.instance != null){
            UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        }
        // Efeito de câmera ao levar dano
        if (CameraShake.instance != null)
            CameraShake.instance.Shake(0.2f, 0.2f); // Tremidinha rápida
        if (currentHealth <= 0) Die(); // Chama a função de morrer
    }

    void Die(){
        SceneManager.LoadScene("MainMenu");
    }
}