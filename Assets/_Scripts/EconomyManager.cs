using UnityEngine;
using TMPro; // Vamos precisar mostrar o dinheiro na tela depois

public class EconomyManager : MonoBehaviour{

    public static EconomyManager instance;

    public int currentCoins = 0;

    // Chaves para salvar no PlayerPrefs (Evita errar digitação depois)
    public const string COIN_KEY = "TotalCoins";

    void Awake(){
        // Singleton que sobrevive entre cenas (Importante para a Loja)
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject); // Não destrói ao mudar de cena
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start(){
        // Carrega o dinheiro salvo. Se não tiver, começa com 0.
        currentCoins = PlayerPrefs.GetInt(COIN_KEY, 0);
    }

    public void AddCoins(int amount){
        currentCoins += amount;
        PlayerPrefs.SetInt(COIN_KEY, currentCoins); // Salva imediatamente
        PlayerPrefs.Save(); // Força a gravação no disco
    }

    public bool SpendCoins(int amount){
        if (currentCoins >= amount){
            currentCoins -= amount;
            PlayerPrefs.SetInt(COIN_KEY, currentCoins);
            PlayerPrefs.Save();
            return true; // Compra aprovada
        }
        else{
            return false; // Compra negada
        }
    }
}