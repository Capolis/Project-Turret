using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar cenas
using TMPro;

public class MainMenuController : MonoBehaviour{

    public string gameSceneName = "SampleScene"; // Nome exato da sua cena de jogo
    public TextMeshProUGUI highScoreText;

    void Start(){
        // Recupera o valor salvo na memória
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null){
            highScoreText.text = $"HIGH SCORE: {bestScore}";
        }
    }

    public void PlayGame(){
        // Carrega a cena do jogo
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }

}