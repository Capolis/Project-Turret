using UnityEngine;
using UnityEngine.UI; // Para o Slider

public class AudioManager : MonoBehaviour{
    public static AudioManager instance;

    [Header("Fontes de Áudio")]
    public AudioSource sfxSource;   // Canal para Efeitos Sonoros
    public AudioSource musicSource; // Canal para Música de Fundo

    [Header("Clips de Áudio")]
    public AudioClip shootClip;
    public AudioClip explosionClip;
    public AudioClip levelUpClip;

    [Header("Clips de Música")]
    public AudioClip menuTheme;
    public AudioClip gameTheme;

    // Chave para salvar o volume
    private const string VOLUME_KEY = "MasterVolume";

    void Awake(){
        if (instance == null) { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else Destroy(gameObject);
    }

    void Start(){
        // Carrega volume salvo (Padrão 1.0 = 100%)
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
    }
    // --- Sistema de Música ---
    public void PlayMenuMusic(){
        if (musicSource.clip == menuTheme) return; // Já está tocando
        musicSource.clip = menuTheme;
        musicSource.Play();
    }

    public void PlayGameMusic(){
        if (musicSource.clip == gameTheme) return;
        musicSource.clip = gameTheme;
        musicSource.Play();
    }

    // --- Sistema de Volume (0.0 a 1.0) ---
    public void SetVolume(float volume){
        // AudioListener controla o volume GLOBAL do jogo (SFX + Música)
        AudioListener.volume = volume;
        // Salva para a próxima vez
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    // --- Métodos de Disparo ---
    public void PlayShoot(){
        // PlayOneShot é vital aqui: ele permite que sons se sobreponham.
        // Se usar apenas .Play(), um tiro cortaria o som do anterior.
        // O "0.7f" é o volume (70%) para o tiro não ensurdecer.
        sfxSource.PlayOneShot(shootClip, 0.7f);
    }

    public void PlayExplosion(){
        sfxSource.PlayOneShot(explosionClip, 0.8f);
    }

    public void PlayLevelUp(){
        sfxSource.PlayOneShot(levelUpClip, 1.0f);
    }
}