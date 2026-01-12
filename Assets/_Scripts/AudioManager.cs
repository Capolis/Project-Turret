using UnityEngine;

public class AudioManager : MonoBehaviour{
    public static AudioManager instance;

    [Header("Fontes de Áudio")]
    public AudioSource sfxSource;   // Canal para Efeitos Sonoros
    public AudioSource musicSource; // Canal para Música de Fundo (Futuro)

    [Header("Clips de Áudio")]
    public AudioClip shootClip;
    public AudioClip explosionClip;
    public AudioClip levelUpClip;

    void Awake(){
        if (instance == null) instance = this;
        else Destroy(gameObject);
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