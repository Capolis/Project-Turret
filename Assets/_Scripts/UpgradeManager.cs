using UnityEngine;

public class UpgradeManager : MonoBehaviour{

    public static UpgradeManager instance;
    public GameObject levelUpPanel; // O painel

    [Header("Referências")]
    public TurretController playerWeapon;
    public PlayerHealth playerHealth;

    void Awake(){
        if (instance == null) instance = this;
    }

    public void OpenUpgradeMenu(){
        levelUpPanel.SetActive(true); // Mostra o menu
        Time.timeScale = 0f;          // PAUSA O JOGO (Congela tudo)
    }

    public void CloseUpgradeMenu(){
        levelUpPanel.SetActive(false); // Esconde o menu
        Time.timeScale = 1f;           // DESPAUSA O JOGO
    }

    // --- OPÇÕES DE UPGRADE ---

    public void UpgradeFireRate(){
        // Diminui o tempo entre tiros em 10% (Tiro mais rápido)
        playerWeapon.fireRate *= 0.9f;
        CloseUpgradeMenu();
    }

    public void UpgradeHeal(){
        // Cura 2 de vida (sem passar do máximo)
        playerHealth.currentHealth += 2;
        if (playerHealth.currentHealth > playerHealth.maxHealth)
            playerHealth.currentHealth = playerHealth.maxHealth;

        // Atualiza a barra de vida visualmente
        UIManager.instance.UpdateHealthBar(playerHealth.currentHealth, playerHealth.maxHealth);

        CloseUpgradeMenu();
    }

    public void UpgradeMultishot(){
        playerWeapon.projectileCount++; // Adiciona +1 bala ao leque
        playerWeapon.fireRate *= 1.1f;  // (Balanceamento) Atirar mais balas deixa o tiro levemente mais lento

        Debug.Log("Upgrade: Multishot Adicionado!");
        CloseUpgradeMenu();
    }

}