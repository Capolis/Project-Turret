using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para reiniciar a cena

public class PlayerHealth : MonoBehaviour{

    public int maxHealth = 5;
    public int currentHealth;

    [Header("Audio")]
    public AudioClip damageSound;

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
        if (UIManager.instance != null)
            UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        // TREMOR LEVE E RÁPIDO
        if (CameraShake.instance != null)
            CameraShake.instance.Shake(0.15f, 0.15f); // Metade da força da Ultimate
        // SOM DE DANO
        if (damageSound != null) { }
            AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position);
        if (currentHealth <= 0) 
            Die(); // Chama a função de morrer
    }

    void Die(){
        SceneManager.LoadScene("MainMenu");
    }
}