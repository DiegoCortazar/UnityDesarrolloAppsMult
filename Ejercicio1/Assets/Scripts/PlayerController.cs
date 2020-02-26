using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update

    private Rigidbody rb;
    public float speed;
    private Vector3 position;
    private Vector3 posicion;

    public Transform particulas;
    private ParticleSystem sistemaParticulas;
    private AudioSource audioRecoleccion;

    public Text textoContador;
    public Text textoGanaste;

    private int cubes = 12;
    private int contador = 0;

    void Start () {
        rb = GetComponent<Rigidbody> ();
        sistemaParticulas = particulas.GetComponent<ParticleSystem> ();
        sistemaParticulas.Stop ();
        audioRecoleccion = GetComponent<AudioSource> ();
        textoContador.text = "Contador: " + contador.ToString ();
        textoGanaste.text = "";
    }

    // Update is called once per frame
    void Update () {
        if (cubes == 0) {
            //SceneManager.LoadScene (1);
            textoGanaste.text = "GANASTE";

        }
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce (movement * speed);
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag ("Recolectable")) {
            //other.gameObject.SetActive(false);
            posicion = other.gameObject.transform.position;
            particulas.position = posicion;
            sistemaParticulas.Play ();
            cubes -= 1;
            contador = contador + 1;
            textoContador.text = "Contador: " + contador.ToString ();
            audioRecoleccion.Play ();
            Destroy (other.gameObject);
            Debug.Log (contador);

        } else {

        }
    }
}