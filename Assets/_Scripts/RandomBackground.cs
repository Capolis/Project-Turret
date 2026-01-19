using UnityEngine;

public class RandomBackground : MonoBehaviour{

    public Sprite[] backgroundImages; // Lista (Array) de imagens

    void Start(){
        // Se a lista não estiver vazia...
        if (backgroundImages.Length > 0){
            // 1. Sorteia um número entre 0 e o tamanho da lista
            int index = Random.Range(0, backgroundImages.Length);
            // 2. Troca a sprite do objeto atual pela sorteada
            GetComponent<SpriteRenderer>().sprite = backgroundImages[index];
        }
    }

}