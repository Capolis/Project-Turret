using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour{

    [Header("UI References")]
    public GameObject shopPanel;
    public GameObject mainMenuContainer; // O grupo que contém os botões do menu
    public TextMeshProUGUI balanceText;

    [Header("Buttons Text")]
    public TextMeshProUGUI healthUpgradeText;
    public TextMeshProUGUI fireRateUpgradeText;
    public TextMeshProUGUI pierceUpgradeText;

    [Header("Prices")]
    public int healthCost = 10;
    public int fireRateCost = 20;
    public int pierceCost = 50;

    // Keys para salvar
    const string HEALTH_LVL_KEY = "Shop_HealthLvl";
    const string FIRERATE_LVL_KEY = "Shop_FireRateLvl";
    const string PIERCE_LVL_KEY = "Shop_PierceLvl";

    void Start(){
        UpdateUI();
        shopPanel.SetActive(false);
        mainMenuContainer.SetActive(true); // Garante que o menu começa visível
    }

    public void ToggleShop(){
        bool isOpening = !shopPanel.activeSelf;

        shopPanel.SetActive(isOpening);
        mainMenuContainer.SetActive(!isOpening); // Se abrir a loja, esconde o menu (e vice-versa)

        if (isOpening) UpdateUI();
    }

    public void UpdateUI(){
        // Atualiza moedas (Inglês)
        if (EconomyManager.instance != null){
            balanceText.text = "Coins: " + EconomyManager.instance.currentCoins.ToString();
        }
        // Atualiza Botão de Vida (Armor)
        int hpLvl = PlayerPrefs.GetInt(HEALTH_LVL_KEY, 0);
        healthUpgradeText.text = $"Armor (+1 HP)\nCost: {healthCost}\n(Lvl {hpLvl})";
        // Atualiza Botão de Fire Rate
        int frLvl = PlayerPrefs.GetInt(FIRERATE_LVL_KEY, 0);
        if (fireRateUpgradeText != null){
            fireRateUpgradeText.text = $"Fire Rate (+10%)\nCost: {fireRateCost}\n(Lvl {frLvl})";
        }
        // Atualiza Botão de Pierce
        int pierceLvl = PlayerPrefs.GetInt(PIERCE_LVL_KEY, 0);
        if (pierceUpgradeText != null){
            // Mostra quantos inimigos a bala atravessa no total (Base 1 + Nível)
            int totalPierce = 1 + pierceLvl;
            pierceUpgradeText.text = $"Piercing (Hits: {totalPierce})\nCost: {pierceCost}\n(Lvl {pierceLvl})";
        }
    }
    // --- AÇÕES DE COMPRA ---
    // Função de compra de Health
    public void BuyHealthUpgrade(){
        if (EconomyManager.instance.SpendCoins(healthCost)){
            int currentLvl = PlayerPrefs.GetInt(HEALTH_LVL_KEY, 0);
            PlayerPrefs.SetInt(HEALTH_LVL_KEY, currentLvl + 1);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }
    // Função de compra de Fire Rate
    public void BuyFireRateUpgrade(){
        if (EconomyManager.instance.SpendCoins(fireRateCost)){
            int currentLvl = PlayerPrefs.GetInt(FIRERATE_LVL_KEY, 0);
            PlayerPrefs.SetInt(FIRERATE_LVL_KEY, currentLvl + 1);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }
    // Adicione a nova função de compra no final do script:
    public void BuyPierceUpgrade(){
        if (EconomyManager.instance.SpendCoins(pierceCost)){
            int currentLvl = PlayerPrefs.GetInt(PIERCE_LVL_KEY, 0);
            PlayerPrefs.SetInt(PIERCE_LVL_KEY, currentLvl + 1);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

}