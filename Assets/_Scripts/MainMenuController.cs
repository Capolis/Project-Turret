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
        if (AudioManager.instance != null){
            AudioManager.instance.PlayMenuMusic();
        }
    }

    public void PlayGame(){
        // Carrega a cena do jogo
        SceneManager.LoadScene(gameSceneName);
        if (AudioManager.instance != null){
            AudioManager.instance.PlayGameMusic();
        }
    }

    public void QuitGame(){
        Application.Quit();
    }

}