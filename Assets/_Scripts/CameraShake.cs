using UnityEngine;

public class CameraShake : MonoBehaviour{

    public static CameraShake instance;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.2f;
    private float dampingSpeed = 1.0f;

    Vector3 initialPosition;

    void Awake(){
        if (instance == null) instance = this;
        initialPosition = transform.localPosition;
    }

    void OnEnable(){
        initialPosition = transform.localPosition;
    }

    void Update(){
        if (shakeDuration > 0){
            // Gera uma posição aleatória dentro de uma esfera pequena
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else{
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void Shake(float duration, float magnitude){
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

}