using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerTaller : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    public float speed;
    public GameObject poder;
    private Vector3 position;
    private Vector3 posicion;
    Material material;
    Animator anim;

    public GameObject cuboMovible;

    public GameObject cuboDesaparece10;
    public GameObject cuboDesaparece5y3;

    public Transform particulas;
    private ParticleSystem sistemaParticulas;
    private AudioSource audioRecoleccion;

    public Text textoContador;
    public Text textoGanaste;

    private int cubes = 12;
    private int contador = 0;

    public Transform particulasQuiz;
    private ParticleSystem sistemaParticulasQuiz;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sistemaParticulas = particulas.GetComponent<ParticleSystem>();
        sistemaParticulas.Stop();
        sistemaParticulasQuiz = particulasQuiz.GetComponent<ParticleSystem>();
        sistemaParticulasQuiz.Stop();
        audioRecoleccion = GetComponent<AudioSource>();
        textoContador.text = "Contador: " + contador.ToString();
        textoGanaste.text = "";
        StartCoroutine("Movimiento");
        StartCoroutine("DesaparecerCubo10");
        StartCoroutine("DesaparecerCubo5y3");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cubes == 0)
        {
            //SceneManager.LoadScene (1);
            textoGanaste.text = "GANASTE";
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Animar();
        }
    }

    public void Animar()
    {
        // anim.SetBool("isSendingMagic",true);
        // StartCoroutine(Reiniciar());
        sistemaParticulasQuiz.Play();
        StartCoroutine(Reiniciar());
    }

    public IEnumerator Reiniciar()
    {
        anim.SetBool("isAnimatorQuiz", true);
        StartCoroutine(DetenerParticulas(sistemaParticulasQuiz));
        yield return new WaitForSecondsRealtime(0.5f);

        //particulas.position = posicion;
        //sistemaParticulasQuiz.Play();
        yield return new WaitForSecondsRealtime(1.0f);
        poder.transform.position = transform.position;
        poder.SendMessage("Shoot");
        anim.SetBool("isAnimatorQuiz", false);
        // yield return new WaitForSecondsRealtime(1.5f);
        // anim.SetBool("isSendingMagic",false);
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

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    public IEnumerator DetenerParticulas(ParticleSystem part)
    {
        yield return new WaitForSecondsRealtime(7);
        part.Stop();
    }

    public IEnumerator DesaparecerCubo10()
    {
        yield return new WaitForSecondsRealtime(10);
        Destroy(this.cuboDesaparece10);
    }
    private Color[] colors = new Color[] { Color.green, Color.black, Color.red, Color.white, Color.blue };
    public IEnumerator DesaparecerCubo5y3()
    {

        int i = 0;
        for (; ; )
        {

            cuboDesaparece5y3.GetComponent<Renderer>().material.color = colors[i];
            i++;

            if (i == colors.Length)
            {
                i = 0;
            }

            yield return new WaitForSecondsRealtime(5);
            this.cuboDesaparece5y3.SetActive(false);

            yield return new WaitForSecondsRealtime(3);
            this.cuboDesaparece5y3.SetActive(true);

        }
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