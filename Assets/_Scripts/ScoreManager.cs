using UnityEngine;
using TMPro; // Para atualizar a UI

public class ScoreManager : MonoBehaviour{

    public static ScoreManager instance;

    public int currentScore = 0;
    public int highScore = 0;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;      // Texto na tela de jogo (ex: "Score: 150")
    public TextMeshProUGUI highScoreText;  // Texto de recorde

    void Awake(){
        if (instance == null) instance = this;
    }

    void Start(){
        // Carrega o recorde salvo. Se não existir, retorna 0.
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddScore(int amount){
        currentScore += amount;

        // Verifica se bateu o recorde
        if (currentScore > highScore)
        {
            highScore = currentScore;
            // Salva IMEDIATAMENTE no disco
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateUI();
    }

    void UpdateUI(){
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore.ToString();

        // Se quiser mostrar o recorde durante o jogo
        if (highScoreText != null) highScoreText.text = "Best: " + highScore.ToString();
    }

}