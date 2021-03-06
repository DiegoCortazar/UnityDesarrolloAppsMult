﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    public float speed;
    private Vector3 position;
    private Vector3 posicion;

    public GameObject cuboMovible;

    public Transform particulas;
    private ParticleSystem sistemaParticulas;
    private AudioSource audioRecoleccion;

    public Text textoContador;
    public Text textoGanaste;

    private int cubes = 12;
    private int contador = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sistemaParticulas = particulas.GetComponent<ParticleSystem>();
        sistemaParticulas.Stop();
        audioRecoleccion = GetComponent<AudioSource>();
        textoContador.text = "Contador: " + contador.ToString();
        textoGanaste.text = "";
        StartCoroutine("Movimiento");
    }

    // Update is called once per frame
    void Update()
    {
        if (cubes == 0)
        {
            //SceneManager.LoadScene (1);
            textoGanaste.text = "GANASTE";
        }
    }

    public IEnumerator Movimiento()
    {
        for (; ; )
        {
            if (Vector3.Distance(transform.position, cuboMovible.transform.position) < 6f)
            {
                cuboMovible.transform.position = Vector3.Lerp(cuboMovible.transform.position,
                new Vector3(Random.Range(-10.0f, 10.0f), 0.97f, Random.Range(-10.0f, 10.0f)),
                10f * Time.deltaTime);
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

   // void FixedUpdate()
    // {
    //     float moveHorizontal = Input.GetAxis("Horizontal");
    //     float moveVertical = Input.GetAxis("Vertical");
    //     Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //     rb.AddForce(movement * speed);
    // }

    public IEnumerator DetenerParticulas(ParticleSystem part)
    {
        yield return new WaitForSecondsRealtime(2);
        part.Stop();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Recolectable"))
        {
            //other.gameObject.SetActive(false);
            posicion = other.gameObject.transform.position;
            particulas.position = posicion;
            sistemaParticulas.Play();
            cubes -= 1;
            contador = contador + 1;
            textoContador.text = "Contador: " + contador.ToString();
            audioRecoleccion.Play();
            Destroy(other.gameObject);
            StartCoroutine(DetenerParticulas(sistemaParticulas));

            Debug.Log(contador);

        }
        else
        {

        }
    }
}