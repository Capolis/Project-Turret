using UnityEngine;

public class BackgroundRotator : MonoBehaviour{
    
    [Header("Configuração")]
    public float rotationSpeed = 2f; // Bem devagar

    void Update(){
        // Gira no eixo Z
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

}