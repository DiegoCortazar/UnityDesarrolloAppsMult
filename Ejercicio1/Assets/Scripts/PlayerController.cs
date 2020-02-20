using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    public float speed;
    private Vector3 position;
    private Vector3 posicion;

    public Transform particulas;
    private ParticleSystem sistemaParticulas;
    private AudioSource audioRecoleccion;

    private int cubes = 12;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sistemaParticulas = particulas.GetComponent<ParticleSystem>();
        sistemaParticulas.Stop();
        audioRecoleccion = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cubes == 0)
        {
            // this.transform.position = new Vector3(0,1,0);
            // cubes = 1;
            SceneManager.LoadScene(1);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
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
            audioRecoleccion.Play();
            Destroy(other.gameObject);
            Debug.Log(cubes);

        }
        else
        {

        }
    }
}
