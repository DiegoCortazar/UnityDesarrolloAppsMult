using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour {
    // Start is called before the first frame update

    Material material;
    void Start () {
        material = GetComponent<Renderer> ().material;
        material.color = Color.black;
    }

    // Update is called once per frame
    void Update () { }

    public void rotar () {
        transform.Rotate (new Vector3 (45, 45, 45));
    }

    public void EscalarCubo (float value) {
        transform.localScale = new Vector3 (value, value, value);
    }

    public void CambiarColor (int opcion) {
        Debug.Log ("Parametro" + opcion);
        switch (opcion) {
            case 0:
                Debug.Log ("Opcion 1");
                material.color = Color.black;
                break;
            case 1:
                Debug.Log ("Opcion 2");
                material.color = Color.red;
                break;
            case 2:
                Debug.Log ("Opcion 3");
                material.color = Color.yellow;
                break;
        }
    }

}