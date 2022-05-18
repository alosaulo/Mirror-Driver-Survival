using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Camera myCamera;
    public Transform alvo;
    public float velocidade;

    Vector3 posInicial;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        posInicial = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = -Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float posY = transform.localPosition.y;

        if (posY >= 0) { 
   
            transform.LookAt(alvo.position);

            transform.RotateAround(alvo.position, 
                new Vector3(mouseY,mouseX), 
                velocidade * Time.deltaTime);

        }

        if (posY < 0)
        {
            transform.localPosition = new Vector3
                (transform.localPosition.x,
                0,
                transform.localPosition.z);
        }

        float distancia = Vector3.Distance(transform.position, alvo.position);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && distancia > 2) { 
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, Time.deltaTime * velocidade);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && distancia < 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, Time.deltaTime * -velocidade);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            transform.localPosition = posInicial;
        }

    }
}
