using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour{

    [Header("UI References")]
    public GameObject shopPanel;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI healthUpgradeText;

    [Header("Configuração de Preços")]
    public int healthCost = 10;

    // Keys para salvar no PlayerPrefs
    const string HEALTH_LVL_KEY = "Shop_HealthLvl";

    void Start(){
        UpdateUI();
        shopPanel.SetActive(false); // Esconde a loja ao iniciar
    }

    public void ToggleShop(){
        // Abre ou fecha o painel
        shopPanel.SetActive(!shopPanel.activeSelf);
        UpdateUI();
    }

    public void UpdateUI(){
        // Atualiza o texto das moedas
        if (EconomyManager.instance != null){
            balanceText.text = "Moedas: " + EconomyManager.instance.currentCoins.ToString();
        }
        // Atualiza o texto do botão (Mostra o nível atual)
        int currentLvl = PlayerPrefs.GetInt(HEALTH_LVL_KEY, 0);
        healthUpgradeText.text = $"Blindagem (Lvl {currentLvl})\nPreço: {healthCost}";
    }

    public void BuyHealthUpgrade(){
        // Verifica se tem dinheiro
        if (EconomyManager.instance.SpendCoins(healthCost)){
            // Aumenta o nível salvo
            int currentLvl = PlayerPrefs.GetInt(HEALTH_LVL_KEY, 0);
            currentLvl++;
            PlayerPrefs.SetInt(HEALTH_LVL_KEY, currentLvl);
            PlayerPrefs.Save();
            // Aumenta o preço para a próxima compra (Opcional: Inflação)
            // healthCost += 10;
            UpdateUI();
        }
        else{
            // Aqui você poderia tocar um som de "Erro"
        }
    }
}