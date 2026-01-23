using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour{

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    void Awake(){
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit = false){
        textMesh.text = damageAmount.ToString();
        if (isCriticalHit){
            textMesh.fontSize = 45;
            textMesh.color = Color.red;
        }
        else{
            textMesh.fontSize = 36;
            textMesh.color = Color.yellow;
        }
        textColor = textMesh.color;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        // Movimento rápido para cima e aleatório para os lados
        moveVector = new Vector3(Random.Range(-1f, 1f), 2f) * 5f;
    }

    void Update(){
        // Move o texto
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime; // Desacelera o movimento
        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f){
            // Primeira metade do tempo: Aumenta e diminui (efeito "pop")
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else{
            // Segunda metade: Diminui escala
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0){
            // Inicia o Fade Out (Transparência)
            float fadeSpeed = 3f;
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0){
                Destroy(gameObject);
            }
        }
    }

}