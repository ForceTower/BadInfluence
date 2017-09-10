using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour {
    public int pontos = 5;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameManager.Instance.AumentarPontuacao(pontos);
            GameObject[] coletaveis = GameObject.FindGameObjectsWithTag("Coletavel");
            Destroy(gameObject);
            if (coletaveis.Length == 1) {
                GameManager.Instance.Vencer();
            }
        }
    }
}
