using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour{

    private Slider slider;

    void Start(){
        slider = GetComponent<Slider>();
        // Recupera o valor atual para a barra visual ficar certa
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        slider.value = savedVolume;
        // Adiciona o "ouvinte" para quando mexer na barra
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck(){
        if (AudioManager.instance != null)
            AudioManager.instance.SetVolume(slider.value);
    }

}