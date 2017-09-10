using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour {
    public Text nivel;
    public Text pontos;
    public Text vidas;

    public Canvas vencer;
    public Canvas perder;
    public Canvas espera;

    // Use this for initialization
    void Start () {
        vencer.enabled = false;
        perder.enabled = false;
        espera.enabled = false;
    }
	
    // Update is called once per frame
    void Update () {
        nivel.text = "Nivel\n" + GameManager.nivel;
        pontos.text = "Pontos\n" + GameManager.pontuacao;
        vidas.text = "Vidas\n" + FindObjectOfType<CuboControlador>().vida;

        switch(GameManager.Instance.GetEstadoDeJogo()) {
            case GameManager.EstadoDeJogo.Iniciar:
                MostrarMensagemEspera();
                break;
            case GameManager.EstadoDeJogo.Jogando:
                RetirarMensagemEspera();
                break;
        }
    }

    public void MostrarInterfaceVencer() {
        vencer.enabled = true;
    }

    public void MostrarInterfacePerder() {
        perder.enabled = true;
    }

    public void MostrarMensagemEspera() {
        espera.enabled = true;
    }

    public void RetirarMensagemEspera() {
        espera.enabled = false;
    }
}
