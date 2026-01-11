using UnityEngine;

public class LevelSystem : MonoBehaviour{

    public int level = 1;
    public int currentXp = 0;
    public int xpToNextLevel = 100;

    void Start(){
        if (UIManager.instance != null){
            // Força a barra a ir para 0 (ou o valor salvo) assim que o jogo abre
            UIManager.instance.UpdateXPBar(currentXp, xpToNextLevel);

            // É bom fazer o mesmo com o Nível para garantir
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

        currentXp -= xpToNextLevel; // O que sobrou conta para o próximo
        level++;
        xpToNextLevel += 50; // Curva de dificuldade simples (cada nível exige +50 XP)

        if (UIManager.instance != null){
            UIManager.instance.UpdateLevelText(level);
            UIManager.instance.UpdateXPBar(currentXp, xpToNextLevel);
        }

    }

}