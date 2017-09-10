using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerDoMal : MonoBehaviour {
    private Color playerColor;
    public float influencia = 1;
    public int danoInfluencia = 2;
    public int danoBase = 1;
    public float tempoAndar = 4;

    private float tempo;
    private bool influenciado = false;
    private float tempoAndando;

    private List<Vector3> posicoes;
    private int posicaoAtual;
    private Vector3 inicio;

    void Start () {
        posicoes = new List<Vector3>();
        tempo = 0.0f;
        tempoAndando = 0.0f;
        posicaoAtual = 0;
        
        for (int i = 0; i < transform.childCount; i++) {
            Transform filho = transform.GetChild(i);
            posicoes.Add(filho.position);
        }

        posicoes.Add(transform.position);
        inicio = transform.position;
    }
	
    void Update () {
        switch (GameManager.Instance.GetEstadoDeJogo()) {
            case GameManager.EstadoDeJogo.Jogando:
                EmJogo();
                break;
        }
    }

    private void EmJogo() {
        Influenciar();
        AndarEmPosicoes();
    }

    private void AndarEmPosicoes() {
        if (posicoes.Count > 1) {
            Vector3 destino = posicoes[posicaoAtual];

            tempoAndando += Time.deltaTime / tempoAndar;

            if (tempoAndando > 1.0f)
                tempoAndando = 1.0f;

            transform.position = Vector3.Lerp(inicio, destino, tempoAndando);

            if (tempoAndando == 1.0f) {
                posicaoAtual++;
                tempoAndando = 0.0f;
                inicio = transform.position;
                if (posicaoAtual >= posicoes.Count) {
                    posicaoAtual = 0;
                }
            }
        }
    }

    private void Influenciar() {
        if (influenciado) {
            tempo += Time.deltaTime;
            if (tempo >= influencia) {
                tempo = 0.0f;
                FindObjectOfType<CuboControlador>().TomarDano(danoInfluencia);
            }

            float tempoLerp = tempo / influencia;
            Color cor = Color.Lerp(playerColor, GetComponent<MeshRenderer>().material.color, tempoLerp);
            GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>().material.color = cor;
        }
        else {
            tempo = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player" && GameManager.Instance.GetEstadoDeJogo() == GameManager.EstadoDeJogo.Jogando) {
            collision.gameObject.GetComponent<CuboControlador>().TomarDano(danoBase);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && GameManager.Instance.GetEstadoDeJogo() == GameManager.EstadoDeJogo.Jogando) {
            playerColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            influenciado = true;
            //other.gameObject.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && GameManager.Instance.GetEstadoDeJogo() == GameManager.EstadoDeJogo.Jogando) {
            influenciado = false;
            other.gameObject.GetComponent<MeshRenderer>().material.color = playerColor;
        }
    }
}
