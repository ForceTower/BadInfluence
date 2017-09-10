using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboControlador : MonoBehaviour {
    public float velocidade = 5;
    public float forcaSalto = 20;
    public float distanciaRaio = 3;
    public int vida = 5;

    private new Rigidbody rigidbody;

    // Use this for initialization
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        switch(GameManager.Instance.GetEstadoDeJogo()) {
            case GameManager.EstadoDeJogo.Jogando:
                EntradaDoUsuario();
                break;
        }
    }

    private void EntradaDoUsuario() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector3.right * Time.deltaTime * velocidade);
            //GetComponent<Rigidbody>().AddForce(Vector3.right * velocidade);
            //GetComponent<Rigidbody>().MovePosition (transform.position + Vector3.right * Time.deltaTime * velocidade);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            //GetComponent<Rigidbody>().AddForce(Vector3.left * velocidade);
            transform.Translate(Vector3.left * Time.deltaTime * velocidade);
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(Vector3.forward * Time.deltaTime * velocidade);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(Vector3.back * Time.deltaTime * velocidade);
        }

        if (Input.GetKeyDown(KeyCode.Space) && NoChao()) {
            rigidbody.AddForce(Vector3.up * forcaSalto);
        }
    }

    private bool NoChao() {
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -distanciaRaio, 0), Color.black, 2f);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, distanciaRaio);
        foreach (RaycastHit hit in hits) {
            if (hit.collider != GetComponent<Collider>())
                return true;
        }

        return false;
    }

    public void TomarDano(int dano) {
        vida -= dano;
        print("Tomou " + dano + " de dano. Vida: " + vida);

        if (vida <= 0) {
            vida = 0;
            GameManager.Instance.Perder();
        }
    }
}
