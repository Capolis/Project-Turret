using UnityEngine;
using System.Collections; // <--- 1. IMPORTANTE: Necessário para o "IEnumerator" funcionar

public class LevelSystem : MonoBehaviour{
    // Singleton para facilitar o acesso (Opcional, mas ajuda muito os botões acharem esse script)
    public static LevelSystem instance;

    public int level = 1;
    public int currentXp = 0;
    public int xpToNextLevel = 100;

    void Awake(){
        if (instance == null) instance = this;
    }

    void Start(){
        if (UIManager.instance != null){
            UIManager.instance.UpdateXPBar(currentXp, xpToNextLevel);
            UIManager.instance.UpdateLevelText(level);
        }
    }

    public void AddExperience(int amount){
        currentXp += amount;
        if (UIManager.instance != null){
            UIManager.instance.UpdateXPBar(currentXp, xpToNextLevel);
        }
        if (currentXp >= xpToNextLevel) LevelUp();
    }

    void LevelUp(){
        if (AudioManager.instance != null)
            AudioManager.instance.PlayLevelUp();

        currentXp -= xpToNextLevel;
        level++;
        xpToNextLevel += 50;
        if (UIManager.instance != null){
            UIManager.instance.UpdateLevelText(level);
            UIManager.instance.UpdateXPBar(currentXp, xpToNextLevel);
        }
        // Abre o menu (Isso pausa o jogo lá no UpgradeManager)
        if (UpgradeManager.instance != null){
            UpgradeManager.instance.OpenUpgradeMenu();
        }
    }
    // --- NOVO: A Lógica de Desaceleração ---
    // 2. Esta é a função que os BOTÕES DE UPGRADE devem chamar ao serem clicados
    public void FinishLevelUp(){
        // Fecha o painel de upgrade visualmente
        if (UpgradeManager.instance != null){
            UpgradeManager.instance.CloseUpgradeMenu();
        }
        // Inicia a câmera lenta
        StartCoroutine(ResumeGameRoutine());
    }

    // 3. A mágica da Câmera Lenta
    IEnumerator ResumeGameRoutine(){
        float targetScale = 1f; // Velocidade normal
        float duration = 1.0f;  // Quanto tempo demora para voltar (1 segundo)
        float currentTime = 0f;

        // Começa bem lento (10% da velocidade)
        Time.timeScale = 0.1f;

        while (currentTime < duration){
            // Usa unscaledDeltaTime porque o Time.deltaTime está afetado pelo timeScale lento
            currentTime += Time.unscaledDeltaTime;
            // Faz a transição suave de 0.1 até 1.0
            Time.timeScale = Mathf.Lerp(0.1f, targetScale, currentTime / duration);

            yield return null; // Espera o próximo frame
        }
        Time.timeScale = 1f; // Garante que cravou em 100% no final
    }

}