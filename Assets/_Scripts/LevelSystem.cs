using UnityEngine;

public class LevelSystem : MonoBehaviour{

    public int level = 1;
    public int currentXp = 0;
    public int xpToNextLevel = 100;

    public void AddExperience(int amount){

        currentXp += amount;
        if (currentXp >= xpToNextLevel){
            LevelUp();
        }

    }

    void LevelUp(){

        currentXp -= xpToNextLevel; // O que sobrou conta para o próximo
        level++;
        xpToNextLevel += 50; // Curva de dificuldade simples (cada nível exige +50 XP)

        // Futuro: Aqui chamaremos o Menu de Upgrades
    }
}