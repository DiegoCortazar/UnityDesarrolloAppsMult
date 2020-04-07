using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDisparo : MonoBehaviour
{

    public GameObject Player;
    public float TiempoEntreDisparos = 1f;
    public float rango = 100f;
    private AudioSource audioDisparo;
    public Transform particulas;
    private Vector3 posicion;

    private ParticleSystem sistemaParticulas;


    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    Light gunLight;
    float effectDisplayTime = 1.2f;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioDisparo = GetComponent<AudioSource>();
        sistemaParticulas = particulas.GetComponent<ParticleSystem>();
        sistemaParticulas.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= TiempoEntreDisparos * effectDisplayTime)
        {
            DissableEffects();
        }
    }

    void Shoot()
    {
        audioDisparo.Play();
        Vector3 ubicacion = new Vector3(Player.transform.position.x,
            Player.transform.position.y + 0.3f,
            Player.transform.position.z);

        timer = 0f;
        gunLine.enabled = true;
        gunLight.enabled = true;
        shootRay.origin = ubicacion;
        shootRay.direction = transform.forward;
        gunLine.SetPosition(0, ubicacion);

        if (Physics.Raycast(shootRay, out shootHit, rango, shootableMask))
        {
            Destroy(shootHit.collider.gameObject);
            posicion = shootHit.point;
            particulas.position = posicion;
            sistemaParticulas.Play();

            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            Debug.Log("No se impacto con ningún objeto");
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * rango);
        }
    }

    public void DissableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
