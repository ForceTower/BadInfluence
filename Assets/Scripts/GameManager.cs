using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    public enum EstadoDeJogo {
        Iniciar, Jogando, Venceu, Perdeu
    }

    public EstadoDeJogo estado;
    public static int pontuacao;
    public static int nivel;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this);
        } else {
            if (_instance != this) {
                Destroy(gameObject);
            }
        }
    }

    void Start () {
        estado = EstadoDeJogo.Iniciar;
        pontuacao = 0;
    }

    void Update () {
		switch(estado) {
            case EstadoDeJogo.Iniciar:
                IniciarEmQualquerTecla();
                break;
        }
    }

    private void IniciarEmQualquerTecla() {
        if (Input.anyKey)
            estado = EstadoDeJogo.Jogando;
    }

    public void Vencer() {
        print("Você ganhou! Pontuacao: " + pontuacao);
        estado = EstadoDeJogo.Venceu;
        FindObjectOfType<InterfaceController>().MostrarInterfaceVencer();
    }

    public void Perder() {
        print("Você Perdeu... Pontuacao: " + pontuacao);
        estado = EstadoDeJogo.Perdeu;
        FindObjectOfType<InterfaceController>().MostrarInterfacePerder();
    }

    public void AumentarPontuacao(int valor) {
        pontuacao += valor;
    }

    public EstadoDeJogo GetEstadoDeJogo() {
        return estado;
    }
}
