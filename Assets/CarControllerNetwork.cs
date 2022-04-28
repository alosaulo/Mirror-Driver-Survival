using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CarControllerNetwork : NetworkBehaviour
{

    [SyncVar(hook = "ColorMe")] public Color32 color;

    public GameObject myCamera;

    public float torque;

    public float direcao;

    public Vector3 centroDeGravidade;

    Rigidbody myBody;

    public WheelCollider[] Rodas;

    public Renderer myRender;


    public override void OnStartServer()
    {
        base.OnStartServer();
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myBody.centerOfMass = centroDeGravidade;
        if (isLocalPlayer) {
            myCamera.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer) { 
            Acelerar();
            Virar();
        }
        //Debug.Log(myBody.velocity.magnitude * 3.6);
    }

    void Acelerar() {
        float vAxis = Input.GetAxis("Vertical");
        Rodas[0].motorTorque = vAxis * torque;
        Rodas[1].motorTorque = vAxis * torque;
        Rodas[2].motorTorque = vAxis * torque;
        Rodas[3].motorTorque = vAxis * torque;
    }

    void Virar() {
        float hAxis = Input.GetAxis("Horizontal");
        Rodas[0].steerAngle = hAxis * direcao;
        Rodas[1].steerAngle = hAxis * direcao;
    }

    private void ColorMe(Color32 oldColor, Color32 newColor)
    {
        /*if (myRender == null)
            myRender = GetComponent<Renderer>();*/
        myRender.material.color = newColor;
    }

    void OnDestroy()
    {
        Destroy(myRender);
    }

}
