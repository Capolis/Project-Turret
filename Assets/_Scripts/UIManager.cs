using UnityEngine;
using UnityEngine.UI; // Necessário para mexer em Image
using TMPro;          // Necessário para mexer em TextMeshPro

public class UIManager : MonoBehaviour{

    // Singleton: Um jeito fácil de acessar este script de qualquer lugar
    public static UIManager instance;

    [Header("UI Elements")]
    public Image healthBarFill;
    public Image xpBarFill;
    public TextMeshProUGUI levelText;

    void Awake(){

        // Configura o Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);

    }

    // Chamado pelo PlayerHealth
    public void UpdateHealthBar(int currentHealth, int maxHealth){

        // Converte para float (0.0 a 1.0) pois a barra precisa de decimal
        float amount = (float)currentHealth / (float)maxHealth;
        healthBarFill.fillAmount = amount;

    }

    // Chamado pelo LevelSystem
    public void UpdateXPBar(int currentXp, int requiredXp){

        float amount = (float)currentXp / (float)requiredXp;
        xpBarFill.fillAmount = amount;

    }

    // Chamado pelo LevelSystem
    public void UpdateLevelText(int level){

        levelText.text = "Lvl: " + level.ToString();

    }

}